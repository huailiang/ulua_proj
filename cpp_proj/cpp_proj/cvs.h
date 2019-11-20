#ifndef  __csv__
#define __csv__

#include <iostream>
#include <string>
#include "lua.hpp"

using namespace std;


#define PUSH_NUMBER_ARRAY(key, p, len) \
lua_pushstring(L, key); \
lua_newtable(L); \
for (size_t i = 0; i < len; i++) { \
	lua_pushnumber(L, *(p + i)); \
	lua_rawseti(L, -2, i); \
} \
lua_settable(L, -3); 


#define PUSH_NUMBER_KV(key, v) \
lua_pushstring(L, key); \
lua_pushnumber(L, v); \
lua_settable(L, -3); 

class cvs
{
public:
	cvs(string name, int row, int col, string* title);
	~cvs();
	
	template<typename T>
	void fill(int i_row, int i_col, T v)
	{
		push_k_v(title[i_col].c_str(), v);
	}

	template<typename T>
	void fill(int i_row, int i_col, T *p, size_t len)
	{
		push_array(title[i_col].c_str(), p, len);
	}

	void fill(int i_row, int i_col, string v);
	void fill(int i_row, int i_col, string *p, size_t len);
	void begin_row();
	void end_row(int i_row);
	void begin(lua_State* L);
	void end();
	lua_State* GetLuaL();


protected:

	template<typename T>
	void push_array(const char* key, T *p, size_t len)
	{
		PUSH_NUMBER_ARRAY(key, p, len);
	}

	template<typename T>
	void push_k_v(const char* key, T v)
	{
		PUSH_NUMBER_KV(key, v);
	}

	void push_array(const char* key, string* p, size_t len);
	void push_k_v(const char* key, const char* v);
	void push_k_v(const char* key, bool v);
	void push_k_v(int32_t key, const char* v);
	void push_k_v(int32_t key, int32_t v);

protected:
	lua_State * L;
	int col, row;
	string* title;
	string table;
};



#endif // ! __csv__



