cp CMakeLists_osx.txt CMakeLists.txt

mkdir -p build_lj_osx && cd build_lj_osx

cmake -DUSING_LUAJIT=ON  -GXcode ../

cd ..

cmake --build build_lj_osx --config Release

mkdir -p plugin_luajit/Plugins/ulua.bundle/Contents/MacOS/

cp build_lj_osx/Release/ulua.bundle/Contents/MacOS/ulua plugin_luajit/Plugins/ulua.bundle/Contents/MacOS/ulua