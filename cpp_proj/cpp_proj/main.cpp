#include "xlua.h"
#include "xtest.h"
#include "xtable.h"
#include "xtype.h"

using namespace std;

xlua* pXlua;
xtest* pTest;
lua_State* L;


void init()
{
	L = luaL_newstate();
	luaL_openlibs(L);

	pXlua = new xlua();
	string *title = new string[3]{ "c1", "c2","c3" };
	pTest = new xtest("m_table", 4, 3, title);
}

void tip()
{
	cout << "** 1.xlua, 2.test, 3.table, any other number break **" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	SAFE_DELETE(pXlua);
	SAFE_DELETE(pTest);

	lua_close(L);
	L = nullptr;
}


void info()
{
	luaL_dofile(L, "behit.lua");
	lua_getglobal(L, "prt_behit");
	lua_pcall(L, 0, 0, 0);
	lua_getglobal(L, "prt_actor");
	lua_pcall(L, 0, 0, 0);
}

void load()
{
	luaL_dofile(L, "regist.lua");
	lua_getglobal(L, "Tables");
	if (!lua_istable(L, -1))
	{
		std::cerr << "error, not found table" << endl;
	}
	else
	{
		size_t count = lua_rawlen(L, -1);
		string* str = new string[count];
		loop(count)
		{
			lua_rawgeti(L, -1, i + 1);
			*(str + i) = lua_tostring(L, -1);
			lua_pop(L, 1);
		}
		loop(count)
		{
			lua_getglobal(L, (str[i] + "_headers").c_str());
			size_t cnt = lua_rawlen(L, -1);
			string* headers = new string[cnt];
			int* types = new int[cnt];
			loop(cnt)
			{
				lua_rawgeti(L, -1, i + 1);
				*(headers + i) = lua_tostring(L, -1);
				lua_pop(L, 1);
			}
			lua_getglobal(L, (str[i] + "_types").c_str());
			loop(cnt)
			{
				lua_rawgeti(L, -1, i + 1);
				*(types + i) = lua_tointeger(L, -1);
				lua_pop(L, 1);
			}
			XTable* pTab = new XTable(str[i], headers, types, (char)cnt);
			pTab->Read(L);
			delete pTab;
		}
		info();
		delete[] str;
	}
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
			pXlua->exec(L);
			break;
		case 2:
			pTest->exec(L);
			break;
		case 3:
			load();
		default:
			loop = false;
			break;
		}
	}

	system("pause");
	dispose();
	return 0;
}