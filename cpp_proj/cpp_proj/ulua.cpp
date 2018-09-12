#include "ulua.h"

ulua::ulua()
{
	tag = "ulua";
	LUA_GLOBALSINDEX = -1027;
	L = luaL_newstate();
	luaL_openlibs(L);
}

ulua::~ulua()
{
	lua_close(L);
	L = NULL;
}

/*
 测试ulua LuaState的构造函数里的api
*/
void ulua::exec()
{
	cout << "LUA_REGISTRYINDEX " << LUA_REGISTRYINDEX << endl;
	luaL_openlibs(L);
	lua_pushstring(L, "LUAINTERFACE LOADED");
	lua_pushboolean(L, true);
	LUAPRINT("ulua1");
	lua_settable(L, LUA_REGISTRYINDEX);
	LUAPRINT("ulua2");

	lua_newtable(L);
	LUAPRINT("ulua3");
	lua_setglobal(L, "luanet");
	

	lua_getglobal(L, "luanet");
	LUAPRINT("ulua4");
	lua_pushstring(L, "getmetatable");
	LUAPRINT("ulua5");
	lua_getglobal(L, "getmetatable");
	LUAPRINT("ulua6");
	lua_settable(L, -3);
	LUAPRINT("ulua7");

	lua_pushstring(L, "rawget");
	lua_getglobal(L, "rawget");
	lua_settable(L, -3);

	lua_pushstring(L, "rawset");
	lua_getglobal(L, "rawset");
	lua_settable(L, -3);

	//把luanet里的element压入栈
	lua_getglobal(L, "luanet");
	lua_getfield(L, -1,"getmetatable");
	lua_getfield(L, -2, "rawget");
	lua_getfield(L, -3, "rawset");
	LUAPRINT("ulua7");
	lua_pop(L,4);
	LUAPRINT("ulua8");

}