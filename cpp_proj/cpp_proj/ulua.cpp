#include "ulua.h"

ulua::ulua()
{
	tag = "ulua";
	L = luaL_newstate();
	luaL_openlibs(L);
}

ulua::~ulua()
{
	lua_close(L);
	L = NULL;
}

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
	lua_setglobal(L, "luanet");
	lua_pushvalue(L, LUA_GLOBALSINDEX);  //压入了_G表
	lua_getglobal(L, "luanet");
	LUAPRINT("ulua3");
	lua_pushstring(L, "getmetatable");
	lua_getglobal(L, "getmetatable");
	lua_settable(L, -3);
	LUAPRINT("ulua4");
	lua_pushstring(L, "rawget");
	lua_getglobal(L, "rawget");
	lua_settable(L, -3);
	LUAPRINT("ulua5");
	lua_pushstring(L, "rawset");
	lua_getglobal(L, "rawset");
	lua_settable(L, -3);
	LUAPRINT("ulua6");

	// Set luanet as global for object translator                          
	lua_replace(L, LUA_GLOBALSINDEX); //用luanet替换_G表           
	//translator = new ObjectTranslator(this, L);
	lua_replace(L, LUA_GLOBALSINDEX); //恢复_G表     
}