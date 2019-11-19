#include "cvs.h"


#define PUSH_NUMBER_ARRAY(key, p, len) \
lua_pushstring(L, key); \
lua_newtable(L); \
for (size_t i = 0; i < len; i++) { \
	lua_pushnumber(L, *(p + i)); \
	lua_rawseti(L, -2, i); \
} \
lua_settable(L, -3); \
delete[] p; 


#define PUSH_NUMBER_KV(key, v) \
lua_pushstring(L, key); \
lua_pushnumber(L, v); \
lua_settable(L, -3); 


cvs::cvs(const char* table, int row, int col, string* title)
{
	this->table = table;
	this->col = col;
	this->row = row;
	this->title = title;
	L = luaL_newstate();
	luaL_openlibs(L);
	lua_newtable(L); // 最外层的打表
}


cvs::~cvs()
{
	delete[] title;
	title = nullptr;
	lua_close(L);
	L = nullptr;
}

void cvs::begin_row()
{
	lua_newtable(L); //每一行里的小表
}

void cvs::fill(int i_row, int i_col, int32_t v)
{
	push_k_v(title[i_col].c_str(), v);
}

void cvs::fill(int i_row, int i_col, uint32_t v)
{
	push_k_v(title[i_col].c_str(), v);
}

void cvs::fill(int i_row, int i_col, int64_t v)
{
	push_k_v(title[i_col].c_str(), v);
}

void cvs::fill(int i_row, int i_col, float v)
{
	push_k_v(title[i_col].c_str(), v);
}

void cvs::fill(int i_row, int i_col, double v)
{
	push_k_v(title[i_col].c_str(), v);
}

void cvs::fill(int i_row, int i_col, string v)
{
	push_k_v(title[i_col].c_str(), v.c_str());
}

void cvs::fill(int i_row, int i_col, bool v)
{
	push_k_v(title[i_col].c_str(), v);
}

void cvs::fill(int i_row, int i_col, int16_t *p, size_t len)
{
	push_array(title[i_col].c_str(), p, len);
}

void cvs::fill(int i_row, int i_col, int32_t *p, size_t len)
{
	push_array(title[i_col].c_str(), p, len);
}

void cvs::fill(int i_row, int i_col, float *p, size_t len)
{
	push_array(title[i_col].c_str(), p, len);
}

void cvs::fill(int i_row, int i_col, double *p, size_t len)
{
	push_array(title[i_col].c_str(), p, len);
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
	lua_setglobal(L, table);
}

void cvs::push_array(const char* key, int16_t *p, size_t len)
{
	PUSH_NUMBER_ARRAY(key, p, len);
}

void cvs::push_array(const char* key, int32_t *p, size_t len)
{
	PUSH_NUMBER_ARRAY(key, p, len);
}

void cvs::push_array(const char * key, float *p, size_t len)
{
	PUSH_NUMBER_ARRAY(key, p, len);
}

void  cvs::push_array(const char* key, double *p, size_t len)
{
	PUSH_NUMBER_ARRAY(key, p, len);
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

void cvs::push_k_v(const char* key, int32_t v)
{
	PUSH_NUMBER_KV(key, v);
}

void cvs::push_k_v(const char* key, int16_t v)
{
	PUSH_NUMBER_KV(key, v);
}

void cvs::push_k_v(const char* key, uint32_t v)
{
	PUSH_NUMBER_KV(key, v);
}

void cvs::push_k_v(const char* key, int64_t v)
{
	PUSH_NUMBER_KV(key, v);
}

void cvs::push_k_v(const char* key, double v)
{
	PUSH_NUMBER_KV(key, v);
}

void cvs::push_k_v(const char* key, float v)
{
	PUSH_NUMBER_KV(key, v);
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
