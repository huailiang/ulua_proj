#include "xstate.h"


xstate::xstate()
{
	//lua_State 中放的是 lua 虚拟机中的环境表、注册表、运行堆栈、虚拟机的上下文等数据。
	L = luaL_newstate();
	luaL_openlibs(L);
}

xstate::~xstate()
{
	lua_close(L);
	L = NULL;
}

void xstate::main()
{
	lua_pushstring(L, "hello");
	lua_pushnumber(L, 3);
	lua_pushstring(L, "world");
	

	//将指定位置的element 加到栈顶
	lua_pushvalue(L, 3);

	//将栈顶element移到指定位置
	lua_insert(L, 2);
	LUAPRINT();

	//移除idx索引上的值  
	lua_remove(L, 2);
	LUAPRINT();

	//弹出栈顶元素，并替换索引idx位置的值
	lua_replace(L, 1);
	LUAPRINT();

	//从栈顶开始弹出个数 若第二个参数是-1，则全部弹出
	lua_pop(L, 1);
	LUAPRINT();
}
