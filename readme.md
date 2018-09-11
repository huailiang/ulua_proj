<p align="center">
    <a href="https://www.lua.org/">
	    <img src="https://www.lua.org/images/lua25.gif" width="120" height="100">
	</a>
	<a href="https://unity3d.com/cn/">
	    <img src="https://huailiang.github.io/img/unity.jpeg" width="200" height="100">
	</a>
    	<a href="https://huailiang.github.io/">
    	<img src="https://huailiang.github.io/img/avatar-Alex.jpg" width="120" height="100">
   	</a>
</p>

此项目旨在提供一套通用的方式生成lua的库，工程使用的unity版本是2018.2.7


### 项目结构：
```
1.  Assets/ 目录 Unity的资源目录
2.  build/  提供luajit lua53等lua原码库，当然你可以去官网下载 以及不同平台的编译脚本
3.  cpp_proj/  c++工程，用来直接在c++环境里调试lua代码特性 
4.  Shell/ 用来lua原码编译对应到的bytecode，为了加密和优化
```


### git工程一共有个分支：
```
1. master 使用的是lua5.3  lua53好处是最新的版本提供了对64int( long long)等新特性的支持，坏处是效率没有jit高
2. jit    使用的是lua5.1 对应的是luajit, 对应lua版本是lua5.1 好处是jit的运行效率高 坏处是jit作者早就不维护了
```


作者在windows上编译win x64 lua dll, 在mac上编译Android(.so)，osx(.bundle), ios(.a)平台的库

windows 上编译需要安装cmake, vs2013(当然也可以是其他版本，只不过作者在build/make_win64_luajit.bat make_win64_luajit.bat脚本使用的是vs2013, 读者可以根据自己本地环境修改里面定义的变量)


项目c#代码借鉴了很多ulua部分，在ulua升级lua库的时候，因为lua升级特性的原因，做了如下的修改：
1. LUA_REGISTRYINDEX  lua51对应的值是LUA_REGISTRYINDEX 在lua53的定义如下：
``` c++
#if LUAI_BITSINT >= 32
#define LUAI_MAXSTACK		1000000
#else
#define LUAI_MAXSTACK		15000
#endif
#define LUA_REGISTRYINDEX	(-LUAI_MAXSTACK - 1000)
```
所以lua53中对应的值是-1001000  这个值需要在LuaDLL.cs中定义修改过来

2. LUA_ENVIRONINDEX

从 Lua 5.2开始, LUA_GLOBALSINDEX 就不存在了. 代替的是, 使用lua_setglobal() and lua_getglobal(). 正确使用方式如下：


```
lua_pushstring(L, T::className);
lua_pushvalue(L, methods);
lua_settable(L, LUA_GLOBALSINDEX);
```


3. lua_replace这个导出给外部使用的接口也取消了，在Lua53中实现如下：

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

4. lua_pcall 给外部使用的接口取消 替代的是lua_pcallk

```c++
#define lua_pcall(L,n,r,f)	lua_pcallk(L, (n), (r), (f), 0, NULL)
```

我们采取了同xlua同样的实现方式解决，对lua_pcallk包装一下
```c++ 
LUA_API int lua_pcall (lua_State *L, int nargs, int nresults, int errfunc) {
	return lua_pcallk(L, nargs, nresults, errfunc, 0, NULL);
}
```

5. 根据我们项目的需要 移除了luasocket的库， 因为我们项目中所有的收发消息都是通过c#来，网络消息过来的时候，根据注册表分别向c++(战斗使用的库GameCore), lua(补丁使用的库)，c#(系统逻辑)转发， 不同平台使用对应的protobuf来反序列化出相应的对象。移除不必要的库，可以减少代码量，ios提交app store审核时，会有代码量的限制。 读者可以根据自己项目的需要来定制自己的lua库。


更多关于lua51升级后的更变 请参考[这里](/doc/luachanges.md)
### QA:

1. 为什么自己编译lua库？

```
自己编译lua库，可以为相当于为游戏定制需求。
1，可以移除不必要的库，减少代码量 
2，有些公用的库，比如说cjson 可以同时在lua和GameCore中使用
3，如int64等问题可以根据项目需要决定取舍
```

