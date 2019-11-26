#ifndef __itable__
#define __itable__

#include <string>
#include <iostream>
#include "lua.hpp"
#include "common.h"


/*
 对外的API（比如说 main(), c# etc.）
 
 外部需要传lua的搜索路径、表格bytes所在的路径
*/


void add_search_path(lua_State *L, std::string path);

int inner_load(lua_State* L, const char* search_path, const char* table_path);

extern "C" 
{

	LUA_API int luaE_table(lua_State* L, const char* search_path, const char* table_path);

}

#endif
