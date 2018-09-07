/* 
* This file is based on LuaInterface
*/
#define LUA_LIB

#include <string.h>
#include <stdlib.h>
#include <stdbool.h>
#include "lua.h"
#include "lualib.h"
#include "lauxlib.h"

static int tag = 0;

LUA_API int luaL_checkmetatable(lua_State *L,int index) 
{
    int retVal=0;
    if(lua_getmetatable(L,index)!=0) {
        lua_pushlightuserdata(L,&tag);
        lua_rawget(L,-2);
        retVal=!lua_isnil(L,-1);
        lua_settop(L,-3);
    }
    return retVal;
}

LUA_API void *luanet_gettag() 
{
    return &tag;
}

LUA_API void *checkudata(lua_State *L, int ud, const char *tname)
{
  void *p = lua_touserdata(L, ud);

  if (p != NULL) 
  {  /* value is a userdata? */
    if (lua_getmetatable(L, ud))
	{
		int isEqual;

		/* does it have a metatable? */
		lua_getfield(L, LUA_REGISTRYINDEX, tname);  /* get correct metatable */

		isEqual = lua_rawequal(L, -1, -2);

		lua_pop(L, 2);  /* remove both metatables */

		if (isEqual)   /* does it have the correct mt? */
			return p;
	}
  }
  return NULL;
}


LUA_API int luanet_tonetobject(lua_State *L,int index) 
{
  int *udata;
  if(lua_type(L,index)==LUA_TUSERDATA) {
    if(luaL_checkmetatable(L,index)) {
      udata=(int*)lua_touserdata(L,index);
      if(udata!=NULL) return *udata;
    }
    udata=(int*)checkudata(L,index,"luaNet_class");
    if(udata!=NULL) return *udata;
    udata=(int*)checkudata(L,index,"luaNet_searchbase");
    if(udata!=NULL) return *udata;
    udata=(int*)checkudata(L,index,"luaNet_function");
    if(udata!=NULL) return *udata;
  }
  return -1;
}

LUA_API void luanet_newudata(lua_State *L,int val) 
{
  int* pointer=(int*)lua_newuserdata(L,sizeof(int));
  *pointer=val;
}

LUA_API int luanet_checkudata(lua_State *L,int index,const char *meta) 
{
  int *udata=(int*)checkudata(L,index,meta);
  if(udata!=NULL) return *udata;
  return -1;
}

LUA_API int luanet_rawnetobj(lua_State *L,int index) 
{
  int *udata = lua_touserdata(L,index);
  if(udata!=NULL) return *udata;
  return -1;
}

/*tolua extend functions*/

LUA_API const char* lua_tocbuffer(const char* csBuffer, int sz) 
{	
	char* buffer = (char*)malloc(sz + 1);
	memcpy(buffer, csBuffer, sz);
	buffer[sz]=0;			
	return buffer;
}

LUA_API void tolua_getfloat2(lua_State* L, int ref, int pos, float* x, float* y)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  *x = (float)lua_tonumber(L, -2);
  *y = (float)lua_tonumber(L, -1);  
  lua_pop(L, 2);
}

LUA_API void tolua_getfloat3(lua_State* L, int ref, int pos, float* x, float* y, float* z)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  *x = (float)lua_tonumber(L, -3);
  *y = (float)lua_tonumber(L, -2);
  *z = (float)lua_tonumber(L, -1);
  lua_pop(L, 3);
}

LUA_API void tolua_getfloat4(lua_State* L, int ref, int pos, float* x, float* y, float* z, float* w)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  *x = (float)lua_tonumber(L, -4);
  *y = (float)lua_tonumber(L, -3);
  *z = (float)lua_tonumber(L, -2);
  *w = (float)lua_tonumber(L, -1);
  lua_pop(L, 4);
}

LUA_API void tolua_getfloat6(lua_State* L, int ref, int pos, float* x, float* y, float* z, float* x1, float* y1, float* z1)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  *x = (float)lua_tonumber(L, -6);
  *y = (float)lua_tonumber(L, -5);
  *z = (float)lua_tonumber(L, -4);
  *x1 = (float)lua_tonumber(L, -3);
  *y1 = (float)lua_tonumber(L, -2);
  *z1 = (float)lua_tonumber(L, -1);
  lua_pop(L, 6);
}

LUA_API void tolua_pushfloat2(lua_State* L, int ref, float x, float y)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushnumber(L, x);
  lua_pushnumber(L, y);
  lua_call(L, 2, -1);
}

LUA_API void tolua_pushfloat3(lua_State* L, int ref, float x, float y, float z)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushnumber(L, x);
  lua_pushnumber(L, y);
  lua_pushnumber(L, z);
  lua_call(L, 3, -1);
}

LUA_API void tolua_pushfloat4(lua_State* L, int ref, float x, float y, float z, float w)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushnumber(L, x);
  lua_pushnumber(L, y);
  lua_pushnumber(L, z);
  lua_pushnumber(L, w);
  lua_call(L, 4, -1);
}

LUA_API void tolua_pushf3(lua_State* L, int ref, int pos, float* f)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  f[1] = (float)lua_tonumber(L, -3);
  f[2] = (float)lua_tonumber(L, -2);
  f[3] = (float)lua_tonumber(L, -1);
  lua_pop(L, 3);
}


LUA_API void tolua_pushvec3(lua_State* L, int ref, float x, float y, float z)
{
	lua_createtable(L, 0, 3);
	lua_pushnumber(L, x);
	lua_setfield(L, -2, "x");
	lua_pushnumber(L, y);
	lua_setfield(L, -2, "y");
	lua_pushnumber(L, z);
	lua_setfield(L, -2, "z");

	lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
	lua_pushvalue(L, -2);
	lua_setmetatable(L, -2);
	lua_pop(L, 1);   	
}

LUA_API void tolua_getvec3(lua_State* L, int pos, float* x, float* y, float* z)
{
	lua_getfield(L, pos, "x");
	*x = (float)lua_tonumber(L, -1);
	lua_getfield(L, pos, "y");
	*y = (float)lua_tonumber(L, -1);
	lua_getfield(L, pos, "z");
	*z = (float)lua_tonumber(L, -1);
	lua_pop(L, 3);
}

int tolua_index(lua_State* L)
{
  int ret = lua_getmetatable(L, 1);  

  while (ret != 0)
  {            
    lua_pushvalue(L, 2);
    lua_rawget(L, -2);
    int type = lua_type(L, -1);            

    if (type == LUA_TFUNCTION)
    {         
        return 1;
    }
    else if (type == LUA_TTABLE)
    {                      
      lua_rawgeti(L, -1, 1);        
      lua_pushvalue(L, 1);
      lua_call(L, 1, -1);                
      return 1;
    }
    else
    {
      lua_pop(L, 1);
      ret = lua_getmetatable(L, -1);                
    }
  }

  lua_settop(L, 2);
  luaL_error(L, "field or property %s does not exist", lua_tostring(L, 2));        
  return 1;
}

LUA_API void tolua_setindex(lua_State* L)
{
  lua_pushstring(L, "__index");  
  lua_pushcclosure(L, tolua_index, 0);
  lua_rawset(L, -3);
}

int tolua_newIndex(lua_State* L)
{  
  int ret = lua_getmetatable(L, 1);  
  
  while(ret != 0)
  {
    lua_pushvalue(L, 2);
    lua_rawget(L,-2);

    if (!lua_isnil(L, -1))
    {
      lua_rawgeti(L, -1, 2);
      lua_pushvalue(L, 1);
      lua_pushvalue(L, 2);
      lua_pushvalue(L, 3);
      lua_call(L, 3, 0);    
      return 0;
    }
    else
    {
      lua_pop(L, 1);
      ret = lua_getmetatable(L, -1);    
    }  
  }

  lua_settop(L, 3);
  luaL_error(L, "field or property %s does not exist", lua_tostring(L, 2));        
  return 1;
}

LUA_API void tolua_setnewindex(lua_State* L)
{
  lua_pushstring(L, "__newindex");
  lua_pushcclosure(L, tolua_newIndex, 0);
  lua_rawset(L, -3);
}

LUA_API bool tolua_pushudata(lua_State* L, int reference, int index)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, reference);
  lua_rawgeti(L, -1, index);

  if (!lua_isnil(L, -1))
  {
    lua_remove(L, -2);
    return true;
  }

  lua_pop(L, 2);
  return false;
}

LUA_API bool tolua_pushnewudata(lua_State* L, int metaRef, int weakTableRef, int index)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, weakTableRef);
  luanet_newudata(L, index);
  lua_rawgeti(L, LUA_REGISTRYINDEX, metaRef);
  lua_setmetatable(L, -2);                 
  lua_pushvalue(L, -1);
  lua_rawseti(L, -3, index);
  lua_remove(L, -2);
  return true;  
}
