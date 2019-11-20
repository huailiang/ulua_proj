#include "xlua.h"
#include "xtest.h"
#include "xtable.h"
#include "xtype.h"

using namespace std;

xlua* pXlua;
xtest* pTest;
XTable* pTab, *pTab2;
lua_State* L;


void init()
{
	L = luaL_newstate();
	luaL_openlibs(L);

	pXlua = new xlua();
	string *title = new string[3]{ "c1", "c2","c3" };
	pTest = new xtest("m_table", 4, 3, title);


	/*
	headers, types最终由lua传过来， luaState由c#传过来
	*/
	string* headers = new string[25]{
		"presentid",  // 0
		"hitid",
		"hit_front",
		"hit_front_curve",
		"hit_back",   // 4
		"hit_back_curve",
		"hit_left",
		"hit_left_curve",
		"hit_right",  // 8
		"hit_right_curve",
		"death",
		"death_curve",
		"cameraShake",  // 12
		"qte",
		"QTETime",
		"setFace",
		"postEffect", // 16
		"postEffectParams",
		"BuffIDs",
		"BuffTime",
		"PVESP", // 20
		"PVPSP",
		"PostEffectTime",
		"ChangeFlyTime",
		"setHitFromDir" //24
	};

	int* types = new int[25]{
		UINT32,  // presentid
		INT32,  // hitid
		STRING_ARR, // hit_front
		STRING_ARR, // hit_front_curve
		STRING_ARR,
		STRING_ARR,
		STRING_ARR,
		STRING_ARR, // hit_left_curve
		STRING_ARR,
		STRING_ARR, // hit_right_curve
		STRING,  // death
		STRING_ARR, // death_curve
		FLOAT_ARR,
		UINT32_ARR, // qte
		FLOAT_LIST,
		INT32,
		INT32,
		FLOAT_ARR, // postEffectParams
		INT32_LIST,
		FLOAT_LIST,
		INT32,
		INT32, //PVPSP
		FLOAT_SEQ,
		FLOAT_SEQ, // ChangeFlyTime
		BOOLEAN
	};

	pTab = new XTable("BeHit", headers, types, 25);

	string* headers2 = new string[11]{
		"actorId",
		"rotation",
		"scale",
		"offset",
		"idle",
		"touch",
		"interact1",
		"interact2",
		"interact3",
		"filters",
		"pvpStartAction",
	};

	int* types2 = new int[11]{
		UINT32,
		FLOAT_SEQ,
		FLOAT_SEQ,
		FLOAT_SEQ,
		STRING,
		STRING,
		STRING,
		STRING,
		STRING,
		UINT32_ARR,
		STRING_ARR
	};

	pTab2 = new XTable("ActorTable", headers2, types2, 11);
}

void tip()
{
	cout << "** 1.xlua, 2.test, 3.behit, any other number break **" << endl;
	cout << "please your input op: ";
}

void dispose()
{
	SAFE_DELETE(pXlua);
	SAFE_DELETE(pTest);
	SAFE_DELETE(pTab);

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
			pTab->Read(L);
			pTab2->Read(L);
			info();
		default:
			loop = false;
			break;
		}
	}

	system("pause");
	dispose();
	return 0;
}