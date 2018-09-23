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


# Google protobuf support for Lua

项目集成使用的lua源码来自于：https://github.com/starwing/lua-protobuf

lua-protobuf实际上是一个纯C的protobuf协议实现，和对应的Lua绑定。协议实现在一个single-header文件里，即pb.h，这个文件实现了完整的protobuf协议。pb.c是针对Lua的绑定。lua-protobuf支持Lua5.1/LuaJIT, Lua5.2和Lua5.3。库本身是平台无关的，可以在PC或者移动设备上使用。


## 为啥重复发明轮子？
首先实现这个库的时候（其实到现在也是这样），protobuf的Lua支持比较好的只有pbc，而pbc采用了懒惰加载的方式，返回的并不是纯粹的Lua表（plain lua table），而是读取什么解析什么，这就给实际使用数据造成了困难：你必须完全知道协议里有什么域，如果不知道，你必须直接使用pairs遍历这张表。如果直接访问了不存在的field，那么pbc会直接产生错误消息。

另外一个原因是pbc是Lua和C的混合库，很多事情是在Lua库里实现的，实际上pbc的C库起到一个底层解析和消息数据库的作用。lua-protobuf所有的解析代码都在C里面，只需要编译pb.c一个文件就有全部功能了，使用起来更加方便。

lua-protobuf的实现也更为简单，使用了一个同时支持string和integer的哈希表实现，而不是两个哈希表；直接用C代码解析了pb文件，不依赖元数据，这些都让lua-protobuf的代码更加直观。

lua-protobuf天然分成三个模块，利用三种不同的类型来区分：pb_State提供了类型信息，pb_Slice专门负责解析二进制数据，而pb_Buffer专门负责编码二进制数据。代码上非常清晰。


## 高层接口
pb.dll 提供四个模块：

pb模块：高层接口，提供和pbc兼容的encode/decode接口。

pb.conv：这是一个转换工具库，负责在Lua里方便地在protobuf提供的各种类型和Lua原生类型之间转换。

pb.slice：提供了底层的protobuf协议解析能力，能够在不知道message的情况下解析协议二进制数据。

pb.buffer：提供了底层的protobuf的协议序列化能力，能够在不知道message的情况下序列化信息。

pb.io：这个主要是为写protoc插件使用的。protoc会把pb二进制文件通过stdin传递给插件，然而stdin在Windows下默认是用文本模式打开的，这就会导致解析错误。因此pb.io提供了二进制模式下的IO读写功能。



## 高层接口还提供了这些函数：

pb.clear()，清除之前注册的所有消息

pb.clear(msgName)，清除某个之前注册的消息

pb.load(chunk)，直接解析字符串/Slice格式的二进制pb数据注册消息。


## 底层接口

底层接口和C接口主要的功能是在没有/不知道pb数据的情况下，解析二进制的protobuf数据。通常情况下是用不上的，如果有需求的话后续会在这里更新使用说明。

## 注意事项

完全兼容支持protobuf2， protobuf3某些特性可能没有，比如oneof或者map

与 pbc, lubpb 一样，解析pb动态加载 proto 文件，不需要代码生成, 这一点和ulua 、tolua使用的方式截然不同
