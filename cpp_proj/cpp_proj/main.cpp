#include "xlua.h"
#include "xstate.h"
#include "ulua.h"
#include "xtable.h"

using namespace std;

xstate* pState;
xlua* pXlua;
ulua* pUlua;
xtable* pTable;

void init()
{
	pState = new xstate();
	pXlua = new xlua();
	pUlua = new ulua();
	pTable = new xtable;
}

void tip()
{
	cout << "** 1.state, 2.xlua, 3.ulua, 4.table, any other number break **" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	delete pState;
	delete pXlua;
	delete pUlua;
	delete pTable;
	pState = NULL;
	pXlua = NULL;
	pUlua = NULL;
	pTable = NULL;
}

int main()
{
	init();
	int a = 0;
	bool loop = true;
	while (loop)
	{
		tip();
		cin >> a;
		switch (a)
		{
		case 1:
			pState->exec();
			break;
		case 2:
			pXlua->exec();
			break;
		case 3:
			pUlua->exec();
			break;
		case 4:
			pTable->exec();
			break;
		default:
			loop = false;
			break;
		}
	}
	dispose();
	//system("pause");
	return 0;
}