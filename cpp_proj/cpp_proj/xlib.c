#include "lua.hpp"


static int GetSin(lua_State* luaState)
{
	float f;
	f = (float)luaL_checknumber(luaState, 1);
	// f = (float)lua_tonumber(luaState,1);
	float v = sinf(f);
	lua_pushnumber(luaState, v);
	return 1;
}

static const luaL_Reg testlib[] =
{
	{"GetSin", GetSin },
	{NULL, NULL}
};

int luaopenGetLib(lua_State* L)
{
	luaL_newlib(L, testlib);
	return 1; //return one value
}

int exec(lua_State *L) 
{
	lua_State *L = luaL_newstate();
	luaL_requiref(L, "sinlib", luaopenGetLib, 1);

	int status = luaL_loadfile(L, "cfg.lua"); //load the cfg.lua
	lua_pcall(L, 0, 0, 0); //execute the loaded cfg.lua

	int top = lua_gettop(L);

	double x = 10;
	double y = 20;
	double z = 0;

	lua_getglobal(L, "foo");
	bool isf = lua_isfunction(L, -1);
	lua_pushnumber(L, x);
	lua_pushnumber(L, y);
	//execute the lua function (pass two parameters, return one result
	if (lua_pcall(L, 2, 1, 0) != 0)
		luaL_error(L, "error running function 'f': %s",
			lua_tostring(L, -1));
	if (!lua_isnumber(L, -1))
		luaL_error(L, "function 'foo' must return a number");
	z = lua_tonumber(L, -1);
	lua_pop(L, 1);
	lua_close(L);
	return 1;
}