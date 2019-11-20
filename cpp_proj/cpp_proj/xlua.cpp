#include "xlua.h"

xlua::xlua()
{
	file = "main.lua";
	tag = "xlua";
}


void xlua::exec(lua_State* L)
{
	lua_settop(L, 0);// 清空栈
	luaL_dofile(L, file);
	luaL_dostring(L, "print(\"called in cpp\")");

	//读取LUA中变量  
	lua_getglobal(L, "str");
	string str = lua_tostring(L, -1);
	cout << "str = " << str.c_str() << endl;
	LUAPRINT(tag);

	//读取table  
	lua_getglobal(L, "tbl");
	lua_getfield(L, -1, "id");
	lua_getfield(L, -2, "name");
	lua_getfield(L, -3, "tb2");
	LUAPRINT(tag);

	//读取函数
	lua_getglobal(L, "add");        // 获取函数，压入栈中  
	lua_pushnumber(L, 10);          // 压入第一个参数  
	lua_pushnumber(L, 20);          // 压入第二个参数  
	int iRet = lua_pcall(L, 2, 1, 0);// 调用函数，调用完成以后，会将返回值压入栈中，2表示参数个数，1表示返回结果个数。  
	if (iRet)                       // 调用出错  
	{
		const char *pErrorMsg = lua_tostring(L, -1);
		cout << pErrorMsg << endl;
	}
	if (lua_isnumber(L, -1))        //取值输出  
	{
		cout << lua_type(L, 0) << " xx " << lua_type(L, -1) << endl;
		double fValue = lua_tonumber(L, -1);
		cout << "Result is " << fValue << endl;
		
	}
	LUAPRINT(tag);
}