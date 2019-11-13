#include "xtable.h"

/*
 学习参考 
 1. https://blog.csdn.net/linuxheik/article/details/18660675
 2. https://www.cnblogs.com/chevin/p/5889119.html
*/

xtable::xtable()
{
	L = luaL_newstate();
	luaL_openlibs(L);
	tag = "xtable";
	name = "global_c_write";
}


xtable::~xtable()
{
	lua_close(L);
	L = NULL;
}


void xtable::exec()
{
	lua_pushstring(L, "hello world");

	//先动态创建一个table
	lua_newtable(L);
	lua_pushnumber(L, 101); // 先将值压入栈 key=101
	lua_pushstring(L, "www.jb51.net"); //global_c_write[101]="www.jb51.net"
	LUAPRINT(tag);
	lua_settable(L, -3);
	LUAPRINT(tag);
	lua_pushstring(L, "baidu");
	lua_pushstring(L, "www.baidu.com"); ////global_c_write["baidu"]="www.baidu.com"
	LUAPRINT(tag);
	lua_settable(L, -3);
	LUAPRINT(tag);
	lua_setglobal(L, name);
	LUAPRINT(tag);

	//访问table里的值
	lua_getglobal(L, name);
	feach();
	lua_rawgeti(L, -1, 101);		//key为number
	lua_getfield(L, -2, "baidu");   //key位string
	LUAPRINT(tag);
}

void xtable::feach()
{
	size_t size = lua_rawlen(L, -2);//相关于#table  
	for (int i = 1; i <= size; i++)
	{
		lua_pushnumber(L, i);
		lua_gettable(L, -2);
		if (lua_isstring(L, -1))
		{
			cout << "feach： " << i << " " << lua_tostring(L, -1) << endl;
		}
		else if(lua_isnumber(L,-1))
		{
			cout << "feach： " << i << " " << lua_tonumber(L, -1) << endl;
		}
		//这时table[i]的值在栈顶了  
		lua_pop(L, 1);//把栈顶的值移出栈，保证栈顶是table以便遍历。  
	};
}
