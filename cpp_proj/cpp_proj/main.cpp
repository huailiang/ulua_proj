#include "xlua.h"
#include "xtable.h"
#include "behit.h"

using namespace std;

xlua* pXlua;
xtable* pTable;
Behit* pTab;

void init()
{
	pXlua = new xlua();
	string *title = new string[3]{ "c1", "c2","c3" };
	pTable = new xtable("m_table", 4, 3, title);
	pTab = new Behit("BeHit.bytes");
}

void tip()
{
	cout << "** 1.xlua, 2.table, 3.behit, any other number break **" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	SAFE_DELETE(pXlua);
	SAFE_DELETE(pTable);
	SAFE_DELETE(pTab);
}

int main()
{
	cout << "32 int: " << sizeof(int32_t) << endl;
	init();
	int cmd = 0;
	bool loop = true;
	while (loop)
	{
		tip();
		cin >> cmd;
		switch (cmd)
		{
		case 1:
			pXlua->exec();
		case 2:
			pTable->exec();
		case 3:
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