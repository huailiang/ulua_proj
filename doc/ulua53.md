<p align="center">
    <a href="https://www.lua.org/">
	    <img src="http://www.runoob.com/manual/lua53doc/logo.gif" width="110" height="100">
	</a>
	<a href="https://unity3d.com/cn/">
	    <img src="https://huailiang.github.io/img/unity.jpeg" width="200" height="100">
	</a>
    	<a href="https://huailiang.github.io/">
    	<img src="https://huailiang.github.io/img/avatar-Alex.jpg" width="120" height="100">
   	</a>
</p>


此项目c#侧脚本在unity-plugin插件ulua_v1.25的升级变动而来，由于lua53相较于lua51的改动，我们做了如下的修改：



### 1. LUA_REGISTRYINDEX  
	
	lua51对应的值是LUA_REGISTRYINDEX 在lua53的定义如下：

``` c++
#if LUAI_BITSINT >= 32
#define LUAI_MAXSTACK		1000000
#else
#define LUAI_MAXSTACK		15000
#endif
#define LUA_REGISTRYINDEX	(-LUAI_MAXSTACK - 1000)
```

	所以lua53中对应的值是-1001000  这个值需要在LuaDLL.cs中定义修改过来

### 2. LUA_GLOBALSINDEX

	从 Lua 5.2开始, LUA_GLOBALSINDEX 就不存在了. 代替的是, 使用lua_setglobal() and lua_getglobal(). 正确使用方式应该用注册表的方式：

```c++
lua_pushstring(L, "getmetatable");
lua_getglobal(L, "getmetatable"); 
```

	类似下面lua_settable中通过LUA_GLOBALSINDEX 伪索引的方式设置table是错误的
	
```c++
lua_settable(L, LUA_GLOBALSINDEX);
```

### 3. lua_replace
	这个导出给外部使用的接口也取消了，在Lua53中实现如下：

```c++
#define lua_replace(L,idx)	(lua_copy(L, -1, (idx)), lua_pop(L, 1))
```

c#端实现如下：

我们加入了lua_copy的外部接口, 封装lua_replace 实现类似c++的效果

```csharp
[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
public static extern void lua_copy(IntPtr luaState, int fromidx,int toidx);
public static void lua_replace(IntPtr luaState, int index)
{
    lua_copy(luaState, -1, index);
    lua_pop(luaState, 1);
}
```

### 4. lua_pcall 
	
	外部使用的接口取消 替代的是lua_pcallk

```c++
#define lua_pcall(L,n,r,f)	lua_pcallk(L, (n), (r), (f), 0, NULL)
```

	我们采取了同xlua同样的实现方式解决，对lua_pcallk包装一下
```c++ 
LUA_API int lua_pcall (lua_State *L, int nargs, int nresults, int errfunc) {
	return lua_pcallk(L, nargs, nresults, errfunc, 0, NULL);
}
```

### 5. 关于require的实现

  lua51的用法：
 	a. 查找package.loaded 确认目标模块是否加载过
	b. 没加载过，则通过package.loaders 获取loader
	c. 通过loader去加载目标模块
  lua53中package中剔除了loaders表，相对应替代的是searchers表

  所以我们在LuaState.cs中, 做如下修改(算法参考了xlua的实现方式)：
  
```csharp
LuaDLL.lua_getglobal(L, "package");
LuaDLL.lua_getfield(L, -1, "searchers");
LuaDLL.lua_remove(L, -2); //remv table package
int len = LuaDLL.lua_rawlen(L, -1);
for (int e = len + 1; e > 1; e--)
{
    LuaDLL.lua_rawgeti(L, -1, e - 1);
    LuaDLL.lua_rawseti(L, -2, e);
}
LuaDLL.lua_pushstdcallcfunction(L, loaderFunction);
LuaDLL.lua_rawseti(L, -2, 1);
```

### 6. luaL_typerror
	luaL_typerror取消了， 在lua51中luaL_typerror实现如下：

```c++
LUALIB_API int luaL_typerror (lua_State *L, int narg, const char *tname) {
  const char *msg = lua_pushfstring(L, "%s expected, got %s",
                                    tname, luaL_typename(L, narg));
  return luaL_argerror(L, narg, msg);
}

```

因此在c#中实现如下：

```csharp
public static int luaL_typerror(IntPtr luaState, int narg, string tname)
{
    lua_pushstring(luaState, tname + " expected, got " + luaL_typename(luaState, narg));
    string msg = lua_tostring(luaState, -1);
    return luaL_argerror(luaState, narg, msg);
}
```

### 7. lua_rawgeti，lua_rawseti

	这两个方法法的第三个参数在lua53中定义为lua_Integer，其在32位、64位机子对应到c#不同的整形数据， 所以我们采取和xlua一样的处理方式


	在c#中第三个参数直接定义成long类型：

```csharp
[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
public static extern void xlua_rawgeti(IntPtr luaState, int tableIndex, long index);
[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
public static extern void xlua_rawseti(IntPtr luaState, int tableIndex, long index);
```

	然后在c++中再做类型转换：

```c++
LUA_API void xlua_rawgeti (lua_State *L, int idx, int64_t n) {
	lua_rawgeti(L, idx, (lua_Integer)n);
}

LUA_API void xlua_rawseti (lua_State *L, int idx, int64_t n) {
	lua_rawseti(L, idx, (lua_Integer)n);
}
```

	如果不这样做的话，android平台下lua require其他lua文件，访问package.seachers表的时候，找不到对应的loader, 这个问题困扰了我好久。


###8. library变更

	根据我们项目的需要 移除了luasocket的库， 因为我们项目中所有的收发消息都是通过c#来，网络消息过来的时候，
	根据注册表分别向c++(战斗使用的库GameCore), lua(补丁使用的库)，c#(系统逻辑)转发， 不同平台使用对应的protobuf
	来反序列化出相应的对象。移除不必要的库，可以减少代码量，ios提交app store审核时，会有代码量的限制。 
	读者可以根据自己项目的需要来定制自己的lua库。

