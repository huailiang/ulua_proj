using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace LuaInterface
{
#pragma warning disable 414
    public class MonoPInvokeCallbackAttribute : System.Attribute
    {
        private Type type;
        public MonoPInvokeCallbackAttribute(Type t) { type = t; }
    }
#pragma warning restore 414

    // basic types & defined in lua.h
    public enum LuaTypes
    {
        LUA_TNONE = -1,
        LUA_TNIL = 0,
        LUA_TBOOLEAN = 1,
        LUA_TLIGHTUSERDATA = 2,
        LUA_TNUMBER = 3,
        LUA_TSTRING = 4,
        LUA_TTABLE = 5,
        LUA_TFUNCTION = 6,
        LUA_TUSERDATA = 7,
        LUA_TTHREAD = 8,
        LUA_NUMTAGS = 9
    }

    // garbage-collection function and options & defined in lua.h
    public enum LuaGCOptions
    {
        LUA_GCSTOP = 0,
        LUA_GCRESTART = 1,
        LUA_GCCOLLECT = 2,
        LUA_GCCOUNT = 3,
        LUA_GCCOUNTB = 4,
        LUA_GCSTEP = 5,
        LUA_GCSETPAUSE = 6,
        LUA_GCSETSTEPMUL = 7,
        LUA_GCISRUNNING = 9
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ReaderInfo
    {
        public String chunkData;
        public bool finished;
    }

#if !UNITY_IPHONE
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
#endif
    public delegate int LuaCSFunction(IntPtr luaState);

    public delegate string LuaChunkReader(IntPtr luaState, ref ReaderInfo data, ref uint size);

    public delegate int LuaFunctionCallback(IntPtr luaState);

#if !UNITY_IPHONE
    [SuppressUnmanagedCodeSecurity]
#endif
    public class LuaAPI
    {
        public const int LUA_REGISTRYINDEX = -1001000;

        public const int LUA_MULTRET = -1;

#if UNITY_IPHONE
#if UNITY_EDITOR
        const string LUADLL = "ulua";
#else
        const string LUADLL = "__Internal";
#endif
#else
        const string LUADLL = "ulua";
#endif

        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_tothread(IntPtr L, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_xmove(IntPtr from, IntPtr to, int n);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_newthread(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_resume(IntPtr L, int narg);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_status(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_pushthread(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_gc(IntPtr luaState, LuaGCOptions what, int data);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string lua_typename(IntPtr luaState, LuaTypes type);
        public static string luaL_typename(IntPtr luaState, int stackPos)
        {
            return lua_typename(luaState, lua_type(luaState, stackPos));
        }
        public static int lua_isfunction(IntPtr luaState, int stackPos)
        {
            return Convert.ToInt32(lua_type(luaState, stackPos) == LuaTypes.LUA_TFUNCTION);
        }
        public static int lua_islightuserdata(IntPtr luaState, int stackPos)
        {
            return Convert.ToInt32(lua_type(luaState, stackPos) == LuaTypes.LUA_TLIGHTUSERDATA);
        }
        public static int lua_istable(IntPtr luaState, int stackPos)
        {
            return Convert.ToInt32(lua_type(luaState, stackPos) == LuaTypes.LUA_TTABLE);
        }
        public static int lua_isthread(IntPtr luaState, int stackPos)
        {
            return Convert.ToInt32(lua_type(luaState, stackPos) == LuaTypes.LUA_TTHREAD);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void luaL_error(IntPtr luaState, string message);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string luaL_gsub(IntPtr luaState, string str, string pattern, string replacement);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_isuserdata(IntPtr luaState, int stackPos);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_rawequal(IntPtr luaState, int stackPos1, int stackPos2);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_setfield(IntPtr luaState, int stackPos, string name);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_callmeta(IntPtr luaState, int stackPos, string name);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luaL_newstate();
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_close(IntPtr luaState);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void luaL_openlibs(IntPtr luaState);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_rawlen(IntPtr luaState, int stackPos);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_loadstring(IntPtr luaState, string chunk);
        public static int luaL_dostring(IntPtr luaState, string chunk)
        {
            int result = luaL_loadstring(luaState, chunk);
            if (result != 0) return result;
            return lua_pcall(luaState, 0, -1, 0);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_createtable(IntPtr luaState, int narr, int nrec);
        public static void lua_newtable(IntPtr luaState)
        {
            lua_createtable(luaState, 0, 0);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_getglobal(IntPtr luaState, string name);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_setglobal(IntPtr luaState, string name);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_settop(IntPtr luaState, int newTop);
        public static void lua_pop(IntPtr luaState, int amount)
        {
            lua_settop(luaState, -(amount) - 1);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_insert(IntPtr luaState, int newTop);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_remove(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_gettable(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_rawget(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_settable(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_rawset(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_setmetatable(IntPtr luaState, int objIndex);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_getmetatable(IntPtr luaState, int objIndex);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushvalue(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_copy(IntPtr luaState, int fromidx, int toidx);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_gettop(IntPtr luaState);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern LuaTypes lua_type(IntPtr luaState, int index);
        public static bool lua_isnil(IntPtr luaState, int index)
        {
            return lua_type(luaState, index) == LuaTypes.LUA_TNIL;
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool lua_isnumber(IntPtr luaState, int index);
        public static bool lua_isboolean(IntPtr luaState, int index)
        {
            return lua_type(luaState, index) == LuaTypes.LUA_TBOOLEAN;
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_ref(IntPtr luaState, int registryIndex);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_newuserdata(IntPtr luaState, int size);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_touserdata(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void luaL_unref(IntPtr luaState, int registryIndex, int reference);
        public static void lua_unref(IntPtr luaState, int reference)
        {
            luaL_unref(luaState, LUA_REGISTRYINDEX, reference);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool lua_isstring(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool lua_iscfunction(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushnil(IntPtr luaState);
        public static void lua_pushstdcallcfunction(IntPtr luaState, LuaCSFunction function, int n = 0)
        {
            IntPtr fn = Marshal.GetFunctionPointerForDelegate(function);
            lua_pushcclosure(luaState, fn, n);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_pcall(IntPtr luaState, int nArgs, int nResults, int errfunc);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_tocfunction(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double lua_tonumber(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool lua_toboolean(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]//[,,m]
        public static extern int get_error_func_ref(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]//[,,m]
        public static extern int load_error_func(IntPtr L, int Ref);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_tolstring(IntPtr luaState, int index, out IntPtr strLen);

        public static string lua_tostring(IntPtr luaState, int index)
        {
            IntPtr strlen;

            IntPtr str = lua_tolstring(luaState, index, out strlen);
            if (str != IntPtr.Zero)
            {
                string ret = Marshal.PtrToStringAnsi(str, strlen.ToInt32());
                if (ret == null)
                {
                    int len = strlen.ToInt32();
                    byte[] buffer = new byte[len];
                    Marshal.Copy(str, buffer, 0, len);
                    return Encoding.UTF8.GetString(buffer);
                }
                return ret;
            }
            else
            {
                return null;
            }
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_atpanic(IntPtr luaState, LuaCSFunction panicf);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushnumber(IntPtr luaState, double number);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushinteger(IntPtr luaState, long number);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushboolean(IntPtr luaState, bool value);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushlstring(IntPtr luaState, byte[] str, int size);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushstring(IntPtr luaState, string str);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_newmetatable(IntPtr luaState, string meta);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_getfield(IntPtr luaState, int stackPos, string meta);
        public static void luaL_getmetatable(IntPtr luaState, string meta)
        {
            lua_getfield(luaState, LUA_REGISTRYINDEX, meta);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luaL_checkudata(IntPtr luaState, int stackPos, string meta);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern LuaTypes luaL_getmetafield(IntPtr luaState, int stackPos, string field);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_load(IntPtr luaState, LuaChunkReader chunkReader, ref ReaderInfo data, string chunkName);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_loadbufferx(IntPtr luaState, byte[] buff, int size, string name, string mode);
        public static int luaL_loadbuffer(IntPtr luaState, byte[] buff, int size, string name)
        {
            return luaL_loadbufferx(luaState, buff, size, name, null);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool luaL_checkmetatable(IntPtr luaState, int obj);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luanet_tonetobject(IntPtr luaState, int obj);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void luanet_newudata(IntPtr luaState, int val);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luanet_rawnetobj(IntPtr luaState, int obj);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luanet_checkudata(IntPtr luaState, int obj, string meta);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_error(IntPtr luaState);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool lua_checkstack(IntPtr luaState, int extra);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_next(IntPtr luaState, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushlightuserdata(IntPtr luaState, IntPtr udata);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luanet_gettag();
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void luaL_where(IntPtr luaState, int level);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushcclosure(IntPtr luaState, IntPtr fn, int n);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string lua_getupvalue(IntPtr L, int funcindex, int n);
        public static int luaL_typerror(IntPtr luaState, int narg, string tname)
        {
            lua_pushstring(luaState, tname + " expected, got " + luaL_typename(luaState, narg));
            string msg = lua_tostring(luaState, -1);
            return luaL_argerror(luaState, narg, msg);
        }
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_argerror(IntPtr luaState, int narg, string extramsg);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_getfloat2(IntPtr luaState, int reference, int stack, ref float x, ref float y);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_getfloat3(IntPtr luaState, int reference, int stack, ref float x, ref float y, ref float z);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_getfloat4(IntPtr luaState, int reference, int stack, ref float x, ref float y, ref float z, ref float w);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_getfloat6(IntPtr luaState, int reference, int stack, ref float x, ref float y, ref float z, ref float x1, ref float y1, ref float z1);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_pushfloat2(IntPtr luaState, int reference, float x, float y);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_pushfloat3(IntPtr luaState, int reference, float x, float y, float z);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_pushfloat4(IntPtr luaState, int reference, float x, float y, float z, float w);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ulua_pushudata(IntPtr L, int reference, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ulua_pushnewudata(IntPtr L, int metaRef, int weakTableRef, int index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_setindex(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_setnewindex(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_rawgeti(IntPtr luaState, int tableIndex, long index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ulua_rawseti(IntPtr luaState, int tableIndex, long index);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaopen_pb(System.IntPtr L);
    }
}