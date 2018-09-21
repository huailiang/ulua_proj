## ulua 使用教程

1. 清除所有wrap文件

	点击菜单栏->Lua->Clear Wrap Files

2. 确保使用前生成warp

	点击菜单栏->Lua->Gen Lua Wrap Files

3. 所有自定义的lua文件需要放在Assets\ulua\Resources\Lua\Hotfix目录 或者相对于Resources的Bundle路径

	自定义的lua的脚本以hotfix开头， hotfix开头的lua脚本，我们直接进入指定的路径加载，不再去搜索其他的文件路径，提升搜索效率

4. 为cs类或者方法打标签[Hotfix]或者[HotfixIgnore] 来为dll注入lua代码