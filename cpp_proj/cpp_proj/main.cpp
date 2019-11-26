#include "xtable.h"
#include "xtype.h"
#include "itable.h"

using namespace std;

lua_State* L;


void init()
{
	L = luaL_newstate();
	luaL_openlibs(L);
}


void dispose()
{
	lua_close(L);
	L = nullptr;
}


void info(const char* path)
{
	string t_path = path;
	t_path += "table.lua";
	luaL_dofile(L, t_path.c_str());
	lua_getglobal(L, "prt_behit");
	lua_pcall(L, 0, 0, 0);
	lua_getglobal(L, "prt_actor");
	lua_pcall(L, 0, 0, 0);
}


int main()
{
	init();
	const char* search = "lua/";
	const char* table = "table/";
	inner_load(L, search, table);
	info(search);
	system("pause");
	dispose();
	return 0;
}