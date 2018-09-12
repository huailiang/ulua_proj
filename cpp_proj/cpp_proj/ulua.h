#ifndef  __ulua__
#define __ulua__

#include "lua.hpp"
#include "xprint.h"

class ulua
{
public:
	ulua();
	~ulua();
	void exec();
	void global();

private:
	lua_State* L;
	int LUA_GLOBALSINDEX;
	const char* tag;
};



#endif // ! __ulua__



