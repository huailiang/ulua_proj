#include "xprint.h"


void xprint::print(lua_State* L)
{
	print(L, NULL);
}

void  xprint::print(lua_State* L, const char* tag)
{
	int size = lua_gettop(L);
	cout << "*******  " << tag << " size: " << size << " *******" << endl;
	for (int i = 1;i <= size;i++)
	{
		if (lua_isnumber(L, i))
		{
			cout << i << ": " << lua_tonumber(L, i) << endl;
		}
		else if (lua_isboolean(L, i))
		{
			cout << i << ": is boolean with value: " << (lua_toboolean(L, i) == 1) << endl;
		}
		else if (lua_isstring(L, i))
		{//lua的栈索引是正负数对称的
			cout << i << ": " << lua_tostring(L, i) << endl;// " <-> " << lua_tostring(L, i - size - 1) << endl;
		}
		else if (lua_iscfunction(L, i))
		{
			cout << i << ": is function" << endl;
		}
		else if (lua_isuserdata(L, i))
		{
			cout << i << ": is userdata" << endl;
		}
		else if (lua_istable(L, i))
		{
			cout << i << ": is table" << endl;
		}
		else if (lua_isnoneornil(L, i))
		{
			cout << i << ": is nil" << endl;
		}
		else
		{
			cout << i << " is not released" << endl;
		}
	}
	cout << endl << endl;
}


void xprint::stackDump(lua_State* L)
{
	cout << "\nbegin dump lua stack" << endl;
	int i = 0;
	int top = lua_gettop(L);
	for (i = 1; i <= top; ++i)
	{
		int t = lua_type(L, i);
		switch (t)
		{
		case LUA_TSTRING:
		{
			printf("'%s' ", lua_tostring(L, i));
		}
		break;
		case LUA_TBOOLEAN:
		{
			printf(lua_toboolean(L, i) ? "true " : "false ");
		}
		break;
		case LUA_TNUMBER:
		{
			printf("%g ", lua_tonumber(L, i));
		}
		break;
		default:
		{
			printf("%s ", lua_typename(L, t));
		}
		break;
		}
	}
	cout << "\nend dump lua stack" << endl;
}
