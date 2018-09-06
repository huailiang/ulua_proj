@echo off
if exist "%VS120COMNTOOLS%" (
	set VCVARS="%VS120COMNTOOLS%..\..\VC\bin\"
	goto build
	)  else (goto missing)


:build

@set ENV64="%VCVARS%amd64\vcvars64.bat"

call "%ENV64%"

echo Swtich to x64 build env
cd %~dp0\luajit-2.1.0b3\src
call msvcbuild_mt.bat static
cd ..\..

goto :buildulua

:missing
echo Can't find Visual Studio 2013.
pause
goto :eof

:buildulua
mkdir build_lj64 & pushd build_lj64
cmake -DUSING_LUAJIT=ON -G "Visual Studio 12 2013 Win64" ..
popd
cmake --build build_lj64 --config Release
md plugin_luajit\Plugins\x86_64
copy /Y build_lj64\Release\ulua.dll plugin_luajit\Plugins\x86_64\ulua.dll
pause