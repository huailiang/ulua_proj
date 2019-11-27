
rem MIT License

rem Copyright (c) 2018 huailiang


mkdir build64 & pushd build64
cmake -G "Visual Studio 15 2017 Win64" ..
popd
cmake --build build64 --config Release
md plugin_lua53\Plugins\x86_64
copy /Y build64\Release\ulua.dll plugin_lua53\Plugins\x86_64\ulua.dll
pause