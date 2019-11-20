#ifndef  __csv__
#define __csv__

#include <iostream>
#include <string>
#include "lua.hpp"

using namespace std;


class cvs
{
public:
	cvs(string name, int row, int col, string* title);
	~cvs();
	void begin_row();
	void fill(int i_row, int i_col, uint16_t v);
	void fill(int i_row, int i_col, int32_t v);
	void fill(int i_row, int i_col, uint32_t v);
	void fill(int i_row, int i_col, int64_t v);
	void fill(int i_row, int i_col, float v);
	void fill(int i_row, int i_col, double v);
	void fill(int i_row, int i_col, string v);
	void fill(int i_row, int i_col, bool v);
	void fill(int i_row, int i_col, uint32_t *p, size_t len);
	void fill(int i_row, int i_col, uint16_t *p, size_t len);
	void fill(int i_row, int i_col, int32_t *p, size_t len);
	void fill(int i_row, int i_col, float *p, size_t len);
	void fill(int i_row, int i_col, double *p, size_t len);
	void fill(int i_row, int i_col, string *p, size_t len);
	void end_row(int i_row);
	void end();
	lua_State* GetLuaL();


protected:
	void push_array(const char* key, uint32_t *p, size_t len);
	void push_array(const char* key, uint16_t *p, size_t len);
	void push_array(const char* key, int32_t *p, size_t len);
	void push_array(const char* key, float *p, size_t len);
	void push_array(const char* key, double *p, size_t len);
	void push_array(const char* key, string* p, size_t len);
	void push_k_v(const char* key, const char* v);
	void push_k_v(const char* key, int32_t v);
	void push_k_v(const char* key, int16_t v);
	void push_k_v(const char* key, uint32_t v);
	void push_k_v(const char* key, int64_t v);
	void push_k_v(const char* key, double v);
	void push_k_v(const char* key, float v);
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



