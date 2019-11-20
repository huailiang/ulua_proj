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
	void exec();
	void table();

private:
	const char* tag;
	const char* name;
	const char* file;
};


#endif // ! xtable