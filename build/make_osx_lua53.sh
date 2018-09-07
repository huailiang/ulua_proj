mkdir -p build_osx && cd build_osx
cmake -GXcode ../
cd ..
cmake --build build_osx --config Release
mkdir -p plugin_lua53/Plugins/ulua.bundle/Contents/MacOS/
cp build_osx/Release/ulua.bundle/Contents/MacOS/ulua plugin_lua53/Plugins/ulua.bundle/Contents/MacOS/ulua

