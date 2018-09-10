#ifndef  __xprint__
#define __xprint__

#include <iostream>
#include "lua.hpp"

using namespace std;

#define LUAPRINT(tag) xprint::print(L,tag)

class xprint
{
public:
	static void print(lua_State* L);
	static void print(lua_State* L, const char* tag);
	static void stackDump(lua_State * L);
};

#endif // ! __xprint__


