#include "itable.h"
#include "xtable.h"


void add_search_path(lua_State * L, std::string path)
{
	string v;
	lua_getglobal(L, "package");
	lua_getfield(L, -1, "path");
	v.append(lua_tostring(L, -1));
	v.append(";" + path + "?.lua.txt");
	lua_pushstring(L, v.c_str());
	lua_setfield(L, -3, "path");
	lua_pop(L, 2);
}

int inner_load(lua_State * L, const char* search_path, const char* table_path, unsigned char flag)
{
	int result = 0;
	reader_flag = flag;
	string r_path = search_path;
	add_search_path(L, search_path);
	r_path += "regist.lua.txt";
	result = luaL_dofile(L, r_path.c_str());
	lua_getglobal(L, "Tables");
	if (!lua_istable(L, -1))
	{
#if _DEBUG
		std::cerr << "regist.lua error" << std::endl;
#endif
	}
	else
	{
		size_t count = lua_rawlen(L, -1);
		std::string* str = new std::string[count];
		loop(count)
		{
			lua_rawgeti(L, -1, i + 1);
			*(str + i) = lua_tostring(L, -1);
			lua_pop(L, 1);
		}
		loop(count)
		{
			lua_getglobal(L, (str[i] + "_headers").c_str());
			size_t cnt = lua_rawlen(L, -1);
			std::string* headers = new std::string[cnt];
			int* types = new int[cnt];
			loop(cnt)
			{
				lua_rawgeti(L, -1, i + 1);
				*(headers + i) = lua_tostring(L, -1);
				lua_pop(L, 1);
			}
			lua_getglobal(L, (str[i] + "_types").c_str());
			loop(cnt)
			{
				lua_rawgeti(L, -1, i + 1);
				*(types + i) = lua_tointeger(L, -1);
				lua_pop(L, 1);
			}
			XTable* pTab = new XTable(str[i], table_path, headers, types, (char)cnt);
			pTab->Read(L);
			delete[] headers;
			delete[] types;
			delete pTab;
		}
		delete[] str;
	}
	return result;
}


extern "C"
{
	
	int luaE_table(lua_State* L, const char* search_path, const char* table_path, unsigned char flag)
	{
		return	inner_load(L, search_path, table_path, flag);
	}
}