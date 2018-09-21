<p align="center">
    <a href="https://www.lua.org/">
	    <img src="http://www.runoob.com/manual/lua53doc/logo.gif" width="110" height="100" target="_blank">
	</a>
	<a href="https://unity3d.com/cn/">
	    <img src="https://huailiang.github.io/img/unity.jpeg" width="200" height="100" target="_blank">
	</a>
    	<a href="https://huailiang.github.io/">
    	<img src="https://huailiang.github.io/img/avatar-Alex.jpg" width="120" height="100" target="_blank">
   	</a>
</p>


# ulua 使用教程


### 编辑器使用

1. 清除所有wrap文件

	点击菜单栏->Lua->Clear Wrap Files

2. 确保使用前生成warp

	点击菜单栏->Lua->Gen Lua Wrap Files

3. 所有自定义的lua文件需要放在Assets\ulua\Resources\Lua\Hotfix目录 或者相对于Resources的Bundle路径

	自定义的lua的脚本以hotfix开头， hotfix开头的lua脚本，我们直接进入指定的路径加载，不再去搜索其他的文件路径，提升搜索效率

4. 为cs类或者方法打标签[Hotfix]或者[HotfixIgnore] 来为dll注入lua代码 详细参考[IL注入实现热修](/IL.md)



快速入门：

DoString- 创建一个LuaState Dostring一下就可以了

```c#
LuaState l = new LuaState();
string str = @"print('hello world!')";
l.DoString(str);
l.Close();
```

如果想解析文件， 使用DoFile就可以了

```c#
LuaState l = new LuaState();
l.DoFile("hotfix_hello");
l.Close();
```


### 更多示例

01_Helloworld: 快速入门的例子。

02_CreateGameObject: 分别用反射和wrap方式创建GameObject和ParticleSystem

03_AccessingLuaVariables: 展示怎么用c#访问lua变量和table

04_ScriptsFromFile: 展示lua读取解析lua文件

05_CallLuaFunction: 展示c#调用lua方法， 并提供没有GC的写法

06_AccessingArray: 展示lua中table转化为array，并给c#侧访问

07_LuaArray: 展示多返回值是访问lua table

08_ButtonClick: 热补丁实现按钮点击

09_Override: 泛化函数支持的演示。

10_Int64: lua如何访问64位int支持

11_LuaCall: 当C#回调给lua实现

12_LuaEnum: lua与c#枚举类型的交互

13_LuaClass: lua table实现近似类的效果

14_ProtoBuffer: lua protobuf解析