#ifndef  __xlua__
#define __xlua__

#include "lua.hpp"
#include "xprint.h"

class xlua
{
public:
	xlua();
	void exec(lua_State* L);

private:
	const char* file;
	const char* tag;
};


#endif // ! __xlua__


