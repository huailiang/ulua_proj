
#define LUA_LIB

#include "lua.h"
#include "lualib.h"
#include "lauxlib.h"
#include <string.h>
#include <stdint.h>
#include <stdlib.h>
#include <stdbool.h>
#include "lstate.h"

static const char *const hooknames[] = {"call", "return", "line", "count", "tail return"};
static int hook_index = -1;
static int tag = 0;

#if LUA_VERSION_NUM ==503
LUA_API int lua_setfenv(lua_State *L, int idx)
{
    int type = lua_type(L, idx);
    if(type == LUA_TUSERDATA || type == LUA_TFUNCTION)
    {
        lua_setupvalue(L, idx, 1);
        return 1;
    }
    else
    {
        return 0;
    }
}

#undef lua_insert
LUA_API void lua_insert (lua_State *L, int idx) {
    lua_rotate(L, idx, 1);
}

#undef lua_remove
LUA_API void lua_remove (lua_State *L, int idx) {
	lua_rotate(L, idx, -1);
	lua_pop(L, 1);
}

#undef lua_replace
LUA_API void lua_replace (lua_State *L, int idx) {
	lua_copy(L, -1, idx);
	lua_pop(L, 1);
}

#undef lua_pcall
LUA_API int lua_pcall (lua_State *L, int nargs, int nresults, int errfunc) {
	return lua_pcallk(L, nargs, nresults, errfunc, 0, NULL);
}

#undef lua_tonumber
LUA_API lua_Number lua_tonumber (lua_State *L, int idx) {
	return lua_tonumberx(L, idx, NULL);
}

#endif


LUA_API void ulua_rawgeti (lua_State *L, int idx, int64_t n) {
	lua_rawgeti(L, idx, (lua_Integer)n);
}

LUA_API void ulua_rawseti (lua_State *L, int idx, int64_t n) {
	lua_rawseti(L, idx, (lua_Integer)n);
}

LUA_API int ulua_ref_indirect(lua_State *L, int indirectRef) {
	int ret = 0;
	lua_rawgeti(L, LUA_REGISTRYINDEX, indirectRef);
	lua_pushvalue(L, -2);
	ret = luaL_ref(L, -2);
	lua_pop(L, 2);
	return ret;
}

LUA_API void ulua_getref_indirect(lua_State *L, int indirectRef, int reference) {
	lua_rawgeti(L, LUA_REGISTRYINDEX, indirectRef);
	lua_rawgeti(L, -1, reference);
	lua_remove(L, -2);
}

LUA_API int ulua_tointeger (lua_State *L, int idx) {
	return (int)lua_tointeger(L, idx);
}

LUA_API void ulua_pushinteger (lua_State *L, int n) {
	lua_pushinteger(L, n);
}

LUA_API void ulua_pushlstring (lua_State *L, const char *s, int len) {
	lua_pushlstring(L, s, len);
}

LUALIB_API int uluaL_loadbuffer (lua_State *L, const char *buff, int size,
                                const char *name) {
	return luaL_loadbuffer(L, buff, size, name);
}

void print_top(lua_State *L) {
	lua_getglobal(L, "print");
	lua_pushvalue(L, -2);
	lua_call(L, 1, 0);
}

void print_str(lua_State *L, char *str) {
	lua_getglobal(L, "print");
	lua_pushstring(L, str);
	lua_call(L, 1, 0);
}

void print_value(lua_State *L,  char *str, int idx) {
	idx = lua_absindex(L, idx);
	lua_getglobal(L, "print");
	lua_pushstring(L, str);
	lua_pushvalue(L, idx);
	lua_call(L, 2, 0);
}

LUA_API int errorfunc(lua_State *L) {
	lua_getglobal(L, "debug");
	lua_getfield(L, -1, "traceback");
	lua_remove(L, -2);
	lua_pushvalue(L, 1);
	lua_pushnumber(L, 2);
	lua_call(L, 2, 1);
    return 1;
}

LUA_API int get_error_func_ref(lua_State *L) {
	lua_pushcclosure(L, errorfunc, 0);
	return luaL_ref(L, LUA_REGISTRYINDEX);
}

LUA_API int load_error_func(lua_State *L, int ref) {
	lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
	return lua_gettop(L);
}

LUA_API int pcall_prepare(lua_State *L, int error_func_ref, int func_ref) {
	lua_rawgeti(L, LUA_REGISTRYINDEX, error_func_ref);
	lua_rawgeti(L, LUA_REGISTRYINDEX, func_ref);
	return lua_gettop(L) - 1;
}

static void hook(lua_State *L, lua_Debug *ar)
{
	int event;
	
	lua_pushlightuserdata(L, &hook_index);
	lua_rawget(L, LUA_REGISTRYINDEX);

	event = ar->event;
	lua_pushstring(L, hooknames[event]);
  
	lua_getinfo(L, "nS", ar);
	if (*(ar->what) == 'C') {
		lua_pushfstring(L, "[?%s]", ar->name);
	} else {
		lua_pushfstring(L, "%s:%d", ar->short_src, ar->linedefined > 0 ? ar->linedefined : 0);
	}

	lua_call(L, 2, 0);
}

static void call_ret_hook(lua_State *L) {
	lua_Debug ar;
	
	if (lua_gethook(L)) {
		lua_getstack(L, 0, &ar);
		lua_getinfo(L, "n", &ar);
		
		lua_pushlightuserdata(L, &hook_index);
		lua_rawget(L, LUA_REGISTRYINDEX);
		
		if (lua_type(L, -1) != LUA_TFUNCTION){
			lua_pop(L, 1);
			return;
        }
		
		lua_pushliteral(L, "return");
		lua_pushfstring(L, "[?%s]", ar.name);
		lua_pushliteral(L, "[C#]");
		
		lua_sethook(L, 0, 0, 0);
		lua_call(L, 3, 0);
		lua_sethook(L, hook, LUA_MASKCALL | LUA_MASKRET, 0);
	}
}

static int profiler_set_hook(lua_State *L) {
	if (lua_isnoneornil(L, 1)) {
		lua_pushlightuserdata(L, &hook_index);
		lua_pushnil(L);
		lua_rawset(L, LUA_REGISTRYINDEX);
			
		lua_sethook(L, 0, 0, 0);
	} else {
		luaL_checktype(L, 1, LUA_TFUNCTION);
		lua_pushlightuserdata(L, &hook_index);
		lua_pushvalue(L, 1);
		lua_rawset(L, LUA_REGISTRYINDEX);
		lua_sethook(L, hook, LUA_MASKCALL | LUA_MASKRET, 0);
	}
	return 0;
}



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

/*ulua extend functions*/

LUA_API const char* lua_tocbuffer(const char* csBuffer, int sz) 
{	
	char* buffer = (char*)malloc(sz + 1);
	memcpy(buffer, csBuffer, sz);
	buffer[sz]=0;			
	return buffer;
}

LUA_API void ulua_getfloat2(lua_State* L, int ref, int pos, float* x, float* y)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  *x = (float)lua_tonumber(L, -2);
  *y = (float)lua_tonumber(L, -1);  
  lua_pop(L, 2);
}

LUA_API void ulua_getfloat3(lua_State* L, int ref, int pos, float* x, float* y, float* z)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  *x = (float)lua_tonumber(L, -3);
  *y = (float)lua_tonumber(L, -2);
  *z = (float)lua_tonumber(L, -1);
  lua_pop(L, 3);
}

LUA_API void ulua_getfloat4(lua_State* L, int ref, int pos, float* x, float* y, float* z, float* w)
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

LUA_API void ulua_getfloat6(lua_State* L, int ref, int pos, float* x, float* y, float* z, float* x1, float* y1, float* z1)
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

LUA_API void ulua_pushfloat2(lua_State* L, int ref, float x, float y)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushnumber(L, x);
  lua_pushnumber(L, y);
  lua_call(L, 2, -1);
}

LUA_API void ulua_pushfloat3(lua_State* L, int ref, float x, float y, float z)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushnumber(L, x);
  lua_pushnumber(L, y);
  lua_pushnumber(L, z);
  lua_call(L, 3, -1);
}

LUA_API void ulua_pushfloat4(lua_State* L, int ref, float x, float y, float z, float w)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushnumber(L, x);
  lua_pushnumber(L, y);
  lua_pushnumber(L, z);
  lua_pushnumber(L, w);
  lua_call(L, 4, -1);
}

LUA_API void ulua_pushf3(lua_State* L, int ref, int pos, float* f)
{
  lua_rawgeti(L, LUA_REGISTRYINDEX, ref);
  lua_pushvalue(L, pos);
  lua_call(L, 1, -1);
  f[1] = (float)lua_tonumber(L, -3);
  f[2] = (float)lua_tonumber(L, -2);
  f[3] = (float)lua_tonumber(L, -1);
  lua_pop(L, 3);
}


LUA_API void ulua_pushvec3(lua_State* L, int ref, float x, float y, float z)
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

LUA_API void ulua_getvec3(lua_State* L, int pos, float* x, float* y, float* z)
{
	lua_getfield(L, pos, "x");
	*x = (float)lua_tonumber(L, -1);
	lua_getfield(L, pos, "y");
	*y = (float)lua_tonumber(L, -1);
	lua_getfield(L, pos, "z");
	*z = (float)lua_tonumber(L, -1);
	lua_pop(L, 3);
}

int ulua_index(lua_State* L)
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

LUA_API void ulua_setindex(lua_State* L)
{
  lua_pushstring(L, "__index");  
  lua_pushcclosure(L, ulua_index, 0);
  lua_rawset(L, -3);
}

int ulua_newIndex(lua_State* L)
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

LUA_API void ulua_setnewindex(lua_State* L)
{
  lua_pushstring(L, "__newindex");
  lua_pushcclosure(L, ulua_newIndex, 0);
  lua_rawset(L, -3);
}

LUA_API bool ulua_pushudata(lua_State* L, int reference, int index)
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

LUA_API bool ulua_pushnewudata(lua_State* L, int metaRef, int weakTableRef, int index)
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
