#ifndef  __xtable__
#define __xtable__

#include <string>
#include "cvs.h"
#include "xprint.h"

using namespace std;


class xtable : public cvs
{
public:
	xtable(const char* table, int row, int col, string* title);
	virtual ~xtable();
	void exec();
	void table();

private:
	const char* tag;
	const char* name;
	const char* file;
};


#endif // ! xtable