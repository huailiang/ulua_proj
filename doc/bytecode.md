# Lua 导出bytecode

### lua使用bytecode 的好处主要有两点：
```
 a. 二进制文件，为了加密
 b. 编译后的中间件，提升效率
```

 那么如何导出bytecode呢？

### 1. luajit 

  a. 进入luajit\LuaJIT-2.0.1\src 目录
  
  b. uajit -b  需要编译的lua文件路径 编译输出文件保存路径

  ```shell
# luajit -b d:\src.lua d:\des.lua
luajit -b d:\src.lua d:\des.lua
  ```

### 2. luac (mac下)

a.  下载源文件

```sh
curl -R -O http://www.lua.org/ftp/lua-5.3.1.tar.gz 
tar zxf lua-5.3.1.tar.gz 
cd lua-5.3.1 
make macosx test
```

b. 安装, 输入以下命令，会要求输入Password: 输入相应密码（你的密码），然后回车就自动安装了 

```shell
sudo make install
```

c.配置编译器  sublime下执行Tools->Build System->New Build System 
输入： 

```
{ 
"cmd": ["/usr/local/bin/lua", "$file"], 
"file_regex": "^(…?):([0-9]):?([0-9]*)", 
"selector": "source.lua"
} 
```

保存为Lua.sublime-build，然后Tools-Build System上就能选择lua来编译脚本了

d. luac生成bytecode, 使用如下命令：


```shell
luac -o test.luac test.lua
```


### 注意：

如果项目在PC下正常运行，但是安装到Android手机就报错：ulua.lua: cannot load incompatible bytecode，那么说明你的运行时luajit和编译时luajit版本不一致，你需要删除LuaEncoder文件夹下的luajit，然后，把LuaFramework下的luajit拷贝过来，然后在运行就可以了。


如果运行时候出现这个报错

```
LuaException: error loading module Main from CustomLoader,
Main: size_t size mismatch in precompiled chunk
```

解决：
所使用的luac编译工具得区分32、64位 , 安卓需在32位的编译文件

https://github.com/Tencent/xLua/issues/356

https://www.jianshu.com/p/3c49cf454502

https://github.com/Tencent/xLua/issues/356
