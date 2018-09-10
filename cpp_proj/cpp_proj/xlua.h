#ifndef  __xlua__
#define __xlua__

#include "lua.hpp"
#include "xprint.h"

class xlua
{
public:
	xlua();
	~xlua();
	void main();

private :
	const char* file;
	lua_State* L;
};


#endif // ! __xlua__


