#ifndef  __xprint__
#define __xprint__

#include <iostream>
#include "lua.hpp"

using namespace std;

#define LUAPRINT() xprint::print(L)


class xprint
{
public:
	static void print(lua_State* L);
	static void stackDump(lua_State * L);
};

#endif // ! __xprint__


