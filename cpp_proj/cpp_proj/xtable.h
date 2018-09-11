#ifndef  __xtable__
#define __xtable__


#include "lua.hpp"
#include "xprint.h"


class xtable
{
public:
	xtable();
	~xtable();
	void exec();
	void feach();

private:
	lua_State * L;
	const char* tag;
	const char* name;
};


#endif // ! xtable