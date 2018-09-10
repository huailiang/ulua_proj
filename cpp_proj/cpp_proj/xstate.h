#ifndef  __xstate__
#define __xstate__

#include "lua.hpp"
#include "xprint.h"

class xstate
{
public:
	xstate();
	~xstate();
	void main();

private:
	lua_State * L;
};


#endif // ! xstate



