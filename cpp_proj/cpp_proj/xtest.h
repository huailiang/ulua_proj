#ifndef  __xtest__
#define __xtest__

#include <string>
#include "cvs.h"
#include "xprint.h"

using namespace std;


class xtest : public cvs
{
public:
	xtest(const char* table, int row, int col, string* title);
	virtual ~xtest();
	void exec(lua_State* L);
	void table(lua_State* L);

private:
	const char* tag;
	const char* name;
	const char* file;
};


#endif // ! xtable