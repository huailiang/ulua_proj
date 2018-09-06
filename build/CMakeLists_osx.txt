cmake_minimum_required(VERSION 2.8)

project(XLua)

find_path(XLUA_PROJECT_DIR NAMES SConstruct
    PATHS 
    ${CMAKE_SOURCE_DIR}
    NO_DEFAULT_PATH
    )

MARK_AS_ADVANCED(XLUA_PROJECT_DIR)

include_directories(
  ${CMAKE_SOURCE_DIR}
  lua-5.1.5/src
  lua-5.1.5/etc
  ${CMAKE_CURRENT_BINARY_DIR}
  ${THIRDPART_INC}
)
set ( LUA_CORE lua-5.1.5/src/lapi.c lua-5.1.5/src/lcode.c lua-5.1.5/src/ldebug.c lua-5.1.5/src/ldo.c lua-5.1.5/src/ldump.c lua-5.1.5/src/lfunc.c 
  lua-5.1.5/src/lgc.c lua-5.1.5/src/llex.c lua-5.1.5/src/lmem.c lua-5.1.5/src/lobject.c lua-5.1.5/src/lopcodes.c lua-5.1.5/src/lparser.c lua-5.1.5/src/lstate.c 
  lua-5.1.5/src/lstring.c lua-5.1.5/src/ltable.c lua-5.1.5/src/ltm.c lua-5.1.5/src/lundump.c lua-5.1.5/src/lvm.c lua-5.1.5/src/lzio.c lua-5.1.5/src/lauxlib.c 
  lua-5.1.5/src/lbaselib.c lua-5.1.5/src/ldblib.c lua-5.1.5/src/liolib.c lua-5.1.5/src/lmathlib.c lua-5.1.5/src/loslib.c lua-5.1.5/src/ltablib.c 
  lua-5.1.5/src/lstrlib.c lua-5.1.5/src/linit.c lua-5.1.5/src/loadlib.c)

set ( LUA_LIB )


set ( XLUA_CORE
    i64lib.c
    perflib.c
    xlua.c
    lua_wrap.c
)


macro(source_group_by_dir proj_dir source_files)
    if(MSVC OR APPLE)
        get_filename_component(sgbd_cur_dir ${proj_dir} ABSOLUTE)
        foreach(sgbd_file ${${source_files}})
			get_filename_component(sgbd_abs_file ${sgbd_file} ABSOLUTE)
            file(RELATIVE_PATH sgbd_fpath ${sgbd_cur_dir} ${sgbd_abs_file})
            string(REGEX REPLACE "\(.*\)/.*" \\1 sgbd_group_name ${sgbd_fpath})
            string(COMPARE EQUAL ${sgbd_fpath} ${sgbd_group_name} sgbd_nogroup)
            string(REPLACE "/" "\\" sgbd_group_name ${sgbd_group_name})
            if(sgbd_nogroup)
                set(sgbd_group_name "\\")
            endif(sgbd_nogroup)
            source_group(${sgbd_group_name} FILES ${sgbd_file})
        endforeach(sgbd_file)
    endif(MSVC OR APPLE)
endmacro(source_group_by_dir)

source_group_by_dir(${CMAKE_CURRENT_SOURCE_DIR} LUA_CORE)
source_group_by_dir(${CMAKE_CURRENT_SOURCE_DIR} LUA_LIB)
source_group_by_dir(${CMAKE_CURRENT_SOURCE_DIR} XLUA_CORE)


set(CMAKE_OSX_ARCHITECTURES "$(ARCHS_STANDARD)")
add_library(ulua MODULE
    ${LUA_CORE}
    ${LUA_LIB}
    ${XLUA_CORE}
    ${THIRDPART_SRC}
)
set_target_properties ( ulua PROPERTIES BUNDLE TRUE )

target_link_libraries(ulua
    m
    ${THIRDPART_LIB} 
)
