## lua升级变动

1.	第三方cmodule，如使用luaL_register需要改为 luaL_newlib。如lfs库luaL_register (L, "lfs", fslib) 改为luaL_newlib(L,fslib);。
这里本来第二个参数是表明，非nil是把所有接口放到一个全局变量table中，nil就是所有接口都是全局函数。现在是强制取消全局接口了。

2.	所有第三方库没有gloable的函数了，所以使用时都要赋给一个表。如lfs， local lfs = require"lfs"。

3.	以前thread， function， userdata可以有env。现在没有了。

4.	getfenv、setfenv没了，只能使用_ENV，不能完全替代。比如给不同func加env不行了，以前可以传入不同函数，用setfenv给他们加上相同env的

```lua
setfenv = setfenv or function(f, t)
     f = (type(f) == 'function' and f or debug.getinfo(f + 1, 'f').func)
     local name
     local up = 0
     repeat
         up = up + 1
         name = debug.getupvalue(f, up)
     until name == '_ENV' or name == nil
     if name then
         debug.upvaluejoin(f, up, function() return name end, 1)  -- use unique upvalue
         debug.setupvalue(f, up, t)
     end
end
```

5.	userdata用lua_getuservalue代替lua_setfenv。

6.	local ss = "aa/bb/cc" ss:gsub('/', '%.')  5.1能运行，5.2必须把%去掉。

7.	table.maxn下个版本要去掉了

8.	lua_objlen ->lua_rawlen

9.	module (name [, •••]) deprecated 
    坏处：
        1.使用package.seeall会破坏模块内聚性，有可能随意访问或改变全局变量。
        2.直接把包以指定名字（可以AA.BB.CC的名字）加到了全局表。
    原来的module("mymodule")等同于：

```lua
local modname = "mymodule"     -- 定义模块名 
local M = {}                   -- 定义用于返回的模块表  
_G[modname] = M                -- 将模块表加入到全局变量中  
package.loaded[modname] = M    -- 将模块表加入到package.loaded中，防止多次加载  
setfenv(1,M)                   -- 将模块表设置为函数的环境表，这使得模块中的所有操作是以在模块表中的，这样定义函数就直接定义在模块表中 
```

新的方式：

```lua
local base = _ENV
local modname = {}
local _ENV = modname
return modname
```


10.	local socket = require("socket.core")
module("socket") 如luasocket这样的定义在5.1中，socket是 socket.core返回的table加上本module内定义的接口。5.2中设置了兼容，能用module，但是socket.core中的table直接被空table覆盖，不会暴露。

11.	

```c++
#define LUA_GLOBALSINDEX    LUA_RIDX_GLOBALS
#define luaL_reg            luaL_Reg
#define luaL_putchar(B,c)   luaL_addchar(B,c)
#define lua_open            luaL_newstate
```

12.	luaL_typerror没了，改为

```lua
LUALIB_API int luaL_typerror (lua_State *L, int narg, const char *tname) {
  const char *msg = lua_pushfstring(L, "%s expected, got %s",tname, luaL_typename(L, narg));
  return luaL_argerror(L, narg, msg);
}
```

13.	luaL_openlib(L, NULL, func, 0);   => luaL_setfuncs(L, func, 0);

14. unpack
unpack 这个函数而需要修改为 table.unpack 。这个函数从 lua 5.2 开始就从全局函数移到了 table 下，只是因为部分同学的习惯问题而一直没有改过来。这次 lua 5.3 把它从全局变量里删掉了
在例子07_LuaArray中 我们使用了table.unpack， 而不是unpack

15. __ipairs
__ipairs 在 Lua 5.3 里去掉了，现在 ipairs 的行为改为从 1 开始迭代，直到第一个为 nil 的项停止

16. unsigned
Lua 5.3 去掉了关于 unsigned 等的 api ，现在全部用 lua_Integer 类型了。这些只需要换掉 api ，加上强制转换即可