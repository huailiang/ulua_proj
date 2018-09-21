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


基于IL代码注入的方式实现热修


借鉴于xlua可以利用IL注入的方式实现原c#代码覆盖，既不污染c#代码，也不需要埋点。现已在游戏中实现类似的功能。
 
使用方式：
在新的功能模块中的类名或者方法名加上[Hotfix]标签 而如果忽略某个函数可以使用[HotfixIgnore]
如果线上对应的代码出现问题，可以在HotfixPatch.lua 的Regist函数注册你要重载的函数，在HotfixCallback.lua实现要重载的函数
 
注意事项：
```
1. 构造函数不能被热修

2. 参数含有ref、out的不能被热修

3. 加入[Hotfix]标签后执行LuaTools->Injector->Inject来注入，
	然后在编辑器里运行，确保注入成功没有错误后再执行LuaTools->Injector->Clean来清除IL注入。
	切记注入之后，不要上传dll，上传之前一定要清掉，避免污染代码
	
4. 原先已经成熟的模块就不要在注入了 因为注入会增加额外的代码量

5. 考虑到性能的问题 最好Update方法都主动加一个[HotfixIgnore] 不去热修

```

![](/doc/img/il1.png)

![](/doc/img/il2.png)
