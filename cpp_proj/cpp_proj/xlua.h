#ifndef  __xlua__
#define __xlua__

#include "lua.hpp"
#include "xprint.h"

class xlua
{
public:
	xlua();
	~xlua();
	void exec();

private:
	const char* file;
	lua_State* L;
	const char* tag;
};


#endif // ! __xlua__


