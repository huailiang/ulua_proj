#include "xlua.h"
#include "xstate.h"
#include "ulua.h"

using namespace std;

xstate* pState;
xlua* pXlua;
ulua* pUlua;

void init()
{
	pState = new xstate();
	pXlua = new xlua();
	pUlua = new ulua();
}

void tip()
{
	cout << "* 1.luaState, 2.luaFile, 3.ulua, other any key break *" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	delete pState;
	delete pXlua;
	delete pUlua;
	pState = NULL;
	pXlua = NULL;
	pUlua = NULL;
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
		default:
			loop = false;
			break;
		}
	}
	//system("pause");
	return 0;
}