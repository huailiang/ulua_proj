#include "xlua.h"
#include "xstate.h"
#include "ulua.h"
#include "xtable.h"
#include "behit.h"

using namespace std;

xstate* pState;
xlua* pXlua;
ulua* pUlua;
xtable* pTable;
Behit* pTab;

void init()
{
	pState = new xstate();
	pXlua = new xlua();
	pUlua = new ulua();
	string *title = new string[3]{ "c1", "c2","c3" };
	pTable = new xtable("m_table", 4, 3, title);
	pTab = new Behit("BeHit.bytes");
}

void tip()
{
	cout << "** 1.state, 2.xlua, 3.ulua, 4.table, any other number break **" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	SAFE_DELETE(pState);
	SAFE_DELETE(pXlua);
	SAFE_DELETE(pUlua);
	SAFE_DELETE(pTable);
	SAFE_DELETE(pTab);
}

int main()
{
	cout << "32 int: " << sizeof(int32_t) << endl;
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
		case 5:
			pTab->Read();
		default:
			loop = false;
			break;
		}
	}
	
	system("pause");
	dispose();
	return 0;
}