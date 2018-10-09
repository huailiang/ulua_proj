<p align="center">
    <a href="https://www.lua.org/">
	    <img src="http://www.runoob.com/manual/lua53doc/logo.gif" width="110" height="100">
	</a>
	<a href="https://unity3d.com/cn/">
	    <img src="https://huailiang.github.io/img/unity.jpeg" width="200" height="100">
	</a>
    	<a href="https://huailiang.github.io/">
    	<img src="https://huailiang.github.io/img/avatar-Alex.jpg" width="120" height="100">
   	</a>
</p>


# lua native build for unity



除了pc平台的dll在windows上生成， 其他的平台都是在macos上生成的。


进入build/ 目录， build采用的方式跟xlua一样，都是cmake生成工程，再由相应的工程生成对应的库文件

不同平台下，c++编译的目标库文件也不相同， 下面列出所有平台的c++库文件格式：

```
windows:	*.dll   	shared
macosx:		*.bundle	shared	
android:	*.so		shared
ios:		*.a         	static
```



## 注意：

1. android平台会同时生成armv7 和x64 两个不同cpu指令集下的库
2. ios平台是静态库，其他平台都是动态链接库
3. android 生成的库文件命名必须以lib开头



# build文件说明：

```
1. make_android_lua53.sh 	mac上生成android库文件
2. make_ios_lua53.sh  		mac上生成ios库文件
3. make_osx_lua53.sh        mac上生成osx库文件
4. make_win64_lua53.bat     windows上生成dll文件
```



## 注意：
make_win64_lua53.bat 默认使用的编译ide是vs2013, 如果你是用的visual studio其他的版本，需要改一下脚本里对应的参数
make_android_lua53.sh 需要指定ndk的本地路径，请根据自己的实际路径配置ANDROID_NDK这个变量



lua-protobuf/ 	是lua支持protobuf的c原码

lua-5.3.5/    	是lua53经过不同平台适应修改后的lua原码

CMakeLists.txt 	是cmake的配置文件，扩展和删除native插件，编译添加的宏可以通过此来配置