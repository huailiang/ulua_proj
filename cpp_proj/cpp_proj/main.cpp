#include "xlua.h"
#include "xstate.h"

using namespace std;

xstate* pState;
xlua* pLua;

void init()
{
	pState = new xstate();
	pLua = new xlua();
}

void tip()
{
	cout << "* 1.luaState, 2.luaFile, other any key break *" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	delete pState;
	delete pLua;
	pState = NULL;
	pLua = NULL;
}

void main()
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
			pState->main();
			break;
		case 2:
			pLua->main();
			break;
		default:
			loop = false;
			break;
		}
	}
	//system("pause");
}