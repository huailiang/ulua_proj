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

static int io_loader(lua_State *L)
{
	return 0;
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
	lua_settable(L, LUA_REGISTRYINDEX);

	lua_newtable(L);
	lua_pushstring(L, "getmetatable");
	lua_getglobal(L, "getmetatable");
	lua_settable(L, -3);
	lua_pushstring(L, "rawget");
	lua_getglobal(L, "rawget");
	lua_settable(L, -3);
	lua_pushstring(L, "rawset");
	lua_getglobal(L, "rawset");
	lua_settable(L, -3);
	lua_setglobal(L, "luanet");

	//把luanet里的element压入栈
	lua_getglobal(L, "luanet");
	lua_getfield(L, -1, "getmetatable");
	lua_getfield(L, -2, "rawget");
	lua_getfield(L, -3, "rawset");
	LUAPRINT("ulua7");
	lua_pop(L, 3);
	global();
}



void ulua::global()
{
	lua_getglobal(L, "package");
	lua_getfield(L, -1, "loaded");
	lua_getfield(L, -2, "config");
	lua_getfield(L, -3, "preload");
	lua_getfield(L, -4, "searchers");
	LUAPRINT("ulua9");
}