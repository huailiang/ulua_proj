#ifndef  __xstate__
#define __xstate__

#include "lua.hpp"
#include "xprint.h"

class xstate
{
public:
	xstate();
	~xstate();
	void exec();

private:
	lua_State * L;
	const char* tag;
};


#endif // ! xstate



