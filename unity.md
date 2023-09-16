Rule tilemap: https://www.youtube.com/watch?v=6Q2q3YkYX2U


> 瓦片有缝在Grid→Cell Size调整

>Cinemamachine引入后的残影在Main Camera→Update Method里调整

>卡在地图缝里→{
>	可能发生碰撞的高速对象，选择Continuous Dynamic
>	可能与高速对象碰撞的对象，选择Continuous
>	其他的选择Discrete
>	}

>在 Edit->  ProjectSettings -> Input 中，我们可以看到Unity自带的输入管理器：

>alt和shift可用于调整碰撞体大小时的等比例缩放

>ctrl+R+R批量重命名

>出现地板硌脚时地图用Composite Collider 2D

>用Header帮助整理多种属性

>Kinematic不受碰撞影响

>二段跳错误：猜测原因是第一帧时人物跳跃，跳跃次数减1；第二帧时人物可能仍然在地面上，跳跃次数恢复了，也可能不在地面上，跳跃次数没有恢复。修正方法是利用协程，人物接触地面的0.01s后才将跳跃次数恢复，这样无论第二帧时人物在不在地面上，跳跃次数都会恢复。

>像素图片模糊：Filter Mode选择Point(no filter)；Compression选择None

>PhysicUpdate 先于 LogicUpdate

>Cinemamachine有个Save during play

>委托挺好用的,就是=>

>用ScriptableObject时，多个角色需要多个ScriptableObject

>通过tag找物体：GameObject.FindGameObjectsWithTag("Fist")[0];//返回数组包含所有这个tag的物体，且是随机顺序

>Console下的collaps可以折叠报错，这样就不会999+了

>局部变量的定义可以用var，初始化时指定类型即可

>public Weapon[] weapons之后，能够在Project和Hierarchy界面拖 包含Weapon这一script的gameobject进来，weapons[i]指的是拖进来的gameobject的script这一组件，如果执行Destroy（weapons[i]）,删掉的是script而不是gameobject

>创建Animator Controller之后，就会出现可以连线的那个动画面板；创建Animation之后，就会出现可以加关键帧的面板；把Animation拖入Animator Controller的面板，再把AC拖入物体的组件，就可以实现控制。

>Animation Event的脚本需要挂载在Animator组件的父物体上

>AnimatorStateInfo stateInfo = obj.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0); 0指的是 Animator的 动画层layer，base layer，具体的不太清楚，然后通过if (stateInfo.IsName("Hit1"))就可以判断一个gameobject正在播放的动画是什么

>onTriggerEnter2D只需要碰撞双方其中一个有触发器，比如说A物体不是trigger，B物体是trigger，两个物体挂载的脚本中都有onTriggerEnter2D函数，此时如果两物体接触，双方的onTriggerEnter都会被触发，尽管对于B物体来说，并没有一个“trigger”进入了（Enter）自己。

>transform的中心点是sprite的那个小蓝圈

>旋转用RotateAround，可以在指定平面上（通过选定旋转轴）绕着指定点旋转

>transform的rotation在面板中显示的是角度，但是调用的时候是弧度

>判断对象类型：if (myGun.GetType() == typeof(Initial))

>旋转一个指定向量：
>```
>Vector3 thisDir = new Vector3();
>Quaternion q = Quaternion.AngleAxis(10, new Vector3(0,0,1));//10代表旋转角度，vector代表旋转轴
>thisDir = q*direction ;
>```

>Vector3与Quaternion相互转换
>```
>//四元数转化成欧拉角
>Vector3 p = transform.rotation.eulerAngles;
>//欧拉角转换成四元数     
>Quaternion rotation = Quaternion.Euler(p)
>```

>Physics2D.OverlapCircleAll//获取一个圆形区域内的所有Collider2D

>Vector2.nomalize可以将向量的方向不变，长度变为1

>InputSystem的完整使用流程:
>先创建InputActions，然后在Inspector中点击创建C#文件，然后在需要使用GameInput的类中InputActions actions = new InputActions(),然后通过Enable()启用

>点击一个sprite，进入sprite editor，可以看到sprite的大小（比如说是20*20），这时在Inspector中吧Pixels Per Unit改为20，那么这个sprite的大小就刚好可以塞进一个格子了

>要删除Tilemap中不想要的格子，先选中Edit，然后选中要删除的格子，然后按Delete键

>如果出现Gameobject卡入地图缝里，可以在Edit->ProjectSettings->Physics2D中把Auto Sync Transforms改为false

>A* Pathfinding的在2D中的使用步骤如下：
>1.创建一个空物体，挂载Pathfinding组件,在Pathfinding组件中设置Grid Graph，点击scan
>2.在敌人身上挂载Seeker脚本，然后自己新建一个EnemyController脚本，调用Seeker.StartPath()方法，传入两个参数，一个是起点，一个是终点,它会返回一个Path类型，里面存储的就是路径

>VectorA-VectorB，得到的向量指向VectorA

>Quaternion.LookRotation(transform.position, dir)这个方法返回一个旋转

>寻路插件，在Seeker中的traversable graph中，可以设置当前对象的寻路表格是哪一张

>如果说有很多个Gameobject它们完全重叠到了一起，这时候用一个别的物体（它身上挂载了一个脚本，里面有OnTriggerEnter2D函数）去碰撞它们,会触发多次OnTriggerEnter2D

>如果要修改prefab的动画，必须先open prefab，否则不能插入关键帧

>碰撞体的Static，Kinematic，Dynamic之间的交互：https://blog.csdn.net/qq_39108767/article/details/107548286

>获取某一块瓦片的位置：tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position))
>已知某一块瓦片的位置，删除这块瓦片：tilemap.SetTile(tilemap.WorldToCell(tilemap.GetCellCenterWorld(tilemap.WorldToCell(transform.position))), null)

>layer不同于layermask：如果说有一个layer的name为“ground”，层级为5，那么它的layermask的层级为2^5，即32；layermask主要用于raycast

>unity urp的使用步骤：
>1.package manager里面先下载urp
>2.在project window里右键，create->render->urp asset
>3.在project setting里面把scriptable render pipeline asset改为新建的urp asset
>4.选中相机，把rendering里的post processing改为true
>5.新建volume

>求垂线：Vector2.Perpendicular(Vector2),返回的垂线方向相当于原向量的逆时针旋转90度