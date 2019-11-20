#include "cvs.h"


cvs::cvs(string table, int row, int col, string* title)
{
	this->table = table;
	this->col = col;
	this->row = row;
	this->title = title;
}


cvs::~cvs() {}

void cvs::begin(lua_State* L)
{
	this->L = L;
	lua_newtable(L); // 最外层的大表
}

void cvs::begin_row()
{
	lua_newtable(L); //每一行里的小表
}


void cvs::fill(int i_row, int i_col, string v)
{
	push_k_v(title[i_col].c_str(), v.c_str());
}


void cvs::fill(int i_row, int i_col, string *p, size_t len)
{
	push_array(title[i_col].c_str(), p, len);
}

void cvs::end_row(int i_row)
{
	lua_rawseti(L, -2, i_row);
}

void cvs::end()
{
	lua_setglobal(L, table.c_str());
}

lua_State*  cvs::GetLuaL()
{
	return L;
}

void cvs::push_array(const char * key, string * p, size_t len)
{
	lua_pushstring(L, key);
	lua_newtable(L);
	for (int i = 0; i < len; i++)
	{
		string str = *(p + i);
		lua_pushstring(L, str.c_str());
		lua_rawseti(L, -2, i);
	}
	lua_settable(L, -3);
}

void cvs::push_k_v(const char* key, const char* v)
{
	lua_pushstring(L, key);
	lua_pushstring(L, v);
	lua_settable(L, -3);
}


void cvs::push_k_v(const char* key, bool v)
{
	lua_pushstring(L, key);
	lua_pushboolean(L, v);
	lua_settable(L, -3);
}


void cvs::push_k_v(int32_t key, const char* v)
{
	lua_pushnumber(L, key);
	lua_pushstring(L, v);
	lua_settable(L, -3);
}

void cvs::push_k_v(int32_t key, int32_t v)
{
	lua_pushnumber(L, key);
	lua_pushnumber(L, v);
	lua_settable(L, -3);
}
