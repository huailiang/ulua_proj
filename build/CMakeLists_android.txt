cmake_minimum_required(VERSION 2.8)

project(XLua)

find_path(XLUA_PROJECT_DIR NAMES SConstruct
    PATHS 
    ${CMAKE_SOURCE_DIR}
    NO_DEFAULT_PATH
    )

MARK_AS_ADVANCED(XLUA_PROJECT_DIR)

set(LUAJIT_SRC_PATH luajit-2.1.0b3/src)

include_directories(
  ${CMAKE_SOURCE_DIR}
  ${LUAJIT_SRC_PATH}
  ${CMAKE_CURRENT_BINARY_DIR}
  ${THIRDPART_INC}
)

set ( LUA_CORE )
set_property( SOURCE xlua.c APPEND PROPERTY COMPILE_DEFINITIONS USING_LUAJIT )
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


add_library(ulua SHARED
    ${LUA_CORE}
    ${LUA_LIB}
    ${XLUA_CORE}
    ${THIRDPART_SRC}
)


if(UINT_ESPECIALLY)
    ADD_DEFINITIONS(-DUINT_ESPECIALLY)
endif()
	

target_link_libraries(ulua
${CMAKE_SOURCE_DIR}/${LUAJIT_SRC_PATH}/libluajit.a
m
${THIRDPART_LIB} 
)


