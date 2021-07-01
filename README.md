[TOC]

# Five

## 游戏主要场景及功能

| 场景 | 功能                                | 脚本                  | 脚本功能                                                     | 子功能       |              |
| ---- | ----------------------------------- | --------------------- | ------------------------------------------------------------ | ------------ | ------------ |
| Main | 进入游戏界面                        | LoginInit             | 1.初始化场景内所有UI                                         |              |              |
|      |                                     | 进入游戏控制器        | 2.实例化切换场景工具 并设置跨场景不销毁                      | LevelLoader  |              |
|      |                                     |                       | 3.显示进入游戏UI                                             | Main         |              |
|      |                                     |                       | 4.播放背景音乐                                               |              |              |
|      |                                     | GameSaveManager       | 1.保存和载入仓库信息                                         |              |              |
|      |                                     | 游戏保存和载入控制器  | 2.保存和载入玩家信息                                         |              |              |
|      | 1.进入新游戏或者载入以前的存档/退出 | Main                  |                                                              |              |              |
|      | 2跨场景切换功能                     | LevelLoader           |                                                              |              |              |
|      |                                     |                       |                                                              |              |              |
|      |                                     |                       |                                                              |              |              |
| Home | 管理游戏进行一定的初始化和设置      | GameManager           | 1.管理GameMode、场景类别、敌人列表、NPC列表、是否释放技能中、是否装有装备、是否boss死亡、是否游戏结束 |              |              |
|      |                                     | 游戏管理员            | 2.当主角死亡同时没有死亡界面时ShowGameOverPanel              |              |              |
|      |                                     |                       | 3.载入游戏存档，没有存档则载入空存档                         |              |              |
|      |                                     |                       | 4.场景切换的过渡                                             |              |              |
|      |                                     |                       | 5.初始化对话窗口                                             |              |              |
|      |                                     |                       | 6.根据不同场景显示特定UI（ButtonInHome、Controller、Settings InfoPanel） |              |              |
|      |                                     |                       | 7.根据不同视角选择不同主角                                   |              |              |
|      |                                     | Inventorymanager      | 1.当InventoryManager启动时 刷新当前仓库                      |              |              |
|      |                                     | 仓库管理器            | 2.更新商品信息                                               |              |              |
|      |                                     |                       | 3.刷新当前仓库                                               |              |              |
|      |                                     | DialogManager         | 1.设置对话信息包括响应函数                                   | DialogSystem | 对话系统实现 |
|      |                                     | 对话管理器            |                                                              |              |              |
|      |                                     | AdsPrepare            | 1.返回广告准备好了                                           |              |              |
|      |                                     | 广告准备器            | 2.当广告结束时给予奖励                                       |              |              |
|      |                                     | PlayerInfoManager     | 1.玩家使用物品获得效果                                       |              |              |
|      |                                     | 玩家信息展示管理器    | 2.装备                                                       |              |              |
|      |                                     |                       | 3.挂载技能                                                   |              |              |
|      |                                     |                       | 4.清空技能挂载点                                             |              |              |
|      |                                     |                       | 5.存储角色信息                                               |              |              |
|      |                                     | GameSaveManager       | 1.保存和载入仓库信息                                         |              |              |
|      |                                     | 游戏保存和载入控制器  | 2.保存和载入玩家信息                                         |              |              |
|      |                                     | SkillTreeManager      | 1.显示技能信息                                               |              |              |
|      |                                     | 技能/天赋管理器       | 2.更新UI和按钮                                               |              |              |
|      |                                     |                       | 3.更新天赋点                                                 |              |              |
|      |                                     |                       | 4.升级天赋                                                   |              |              |
|      |                                     |                       | 5.更新技能信息                                               |              |              |
|      |                                     | BeatManager           | 1.管理攻击方向、力道、玩家朝向是否连击中等等                 |              |              |
|      |                                     | 攻击管理器            |                                                              |              |              |
|      |                                     | PlayerState           | 管理玩家名称 描述 等级 个人信息等                            |              |              |
|      |                                     | 升级管理器            |                                                              |              |              |
|      | 管理角色                            | Bodyhit               |                                                              |              |              |
|      |                                     | ArmourHit             |                                                              |              |              |
|      |                                     | PlayerController      |                                                              |              |              |
|      |                                     | PlayerAnimation       |                                                              |              |              |
|      |                                     | HealthBarSlider       |                                                              |              |              |
|      |                                     | HitPoint              |                                                              |              |              |
|      | 游戏设计                            |                       | 音效大小                                                     |              |              |
|      |                                     |                       | 音量大小                                                     |              |              |
|      | 角色信息展示                        | Settings(Clone)       |                                                              |              |              |
|      |                                     | Button In Home(Clone) |                                                              |              |              |
|      |                                     | Controller(Clone)     |                                                              |              |              |
|      |                                     | Dialog Panel(Clone)   |                                                              |              |              |
|      |                                     |                       |                                                              |              |              |
|      |                                     |                       |                                                              |              |              |
| Map  | 管理野外地区战斗                    | RoomGenerator         |                                                              |              |              |

## 角色功能

 功能：   攻击系统、天赋系统、升级系统、背包系统、本地存储系统 、随机地图系统、小地图、NPC、对话系统、云端资源分发、热更新、预下载

### 角色建模

| 角色躯干                                     | 脚本             | 功能                                                         |
| -------------------------------------------- | ---------------- | ------------------------------------------------------------ |
|                                              | BodyHit          | 检测位置、用于受击者受击后生成对应效果                       |
| 躯干碰撞体/刚体/动画机/CinmachineSource/IK2D | ArmourHit        |                                                              |
|                                              | PlayerCOntroller |                                                              |
|                                              | PlayerrAnimation |                                                              |
| healthBarCanVas上挂载                        | HealthBarSlider  |                                                              |
| HitPoint挂载                                 | HitPoint         | 用于当攻击者进行攻击、开门、扔炸弹等类似检测和生成对应效果，其中部分工作转移到bodyhit和ArmourHit |
| landFX/JumpFX                                | JumpFX           | 提供起跳和落地动画的需要的事件                               |

攻击特效主要有三种。根据生成的时机不同放不到的位置生成。

* 一：瞬发特效。无论是否打到人，在攻击的一瞬间生成特效。主要包括全屏特效、前摇特效、剑光特效...
* 二：响应特效。当攻击到人的时候，会产生响应的特效。这个特效不同于受击特效，这个特效的来源来自于攻击者。包括刀剑砍出血、拳头打出灰...
* 三：再总结
* 四：受击特效也需要讲一下。这两个在一起分析会好一点。受击特效就是当受到攻击时会产生的效果。比如溅血、受击哀嚎受击踉跄、甚至受击死亡。有些是不变的，也有些会根据攻击武器的不同产生不的特效，被锤子打成粉末、被刀剑切碎。



合理安排攻击，移动以及两者之间的细节，就离不开beatmanager。beatManager相当于一个面板+控制中心。此外还有IDamageable。这是通用的伤害接口。





| 角色攻击移动技能系统 | 左走/右走    | 其实这两个还OK的 |
| -------------------- | ------------ | ---------------- |
|                      | 左跑/右跑    |                  |
|                      | 跳跃         | 也还OK           |
|                      | 边跳跃边移动 |                  |
|                      | 普攻         |                  |
|                      | 跳击         |                  |
|                      | 空中攻击     |                  |
|                      | 空中滞空连击 |                  |



### 升级系统

升级系统主要由 PlayerInfoManager（玩家信息看板+背包物品使用信息处理器+游戏保存信息看板）、PlayerState（角色信息+升级+管理器）、InfoPanel（角色信息界面看板）

| PlayerState.cs  | 注释                     |      |
| --------------- | ------------------------ | ---- |
| playerName      | 角色名称                 |      |
| description     | 角色描述                 |      |
| currentExp      | 当前经验值               |      |
| playerLevel     | 级别                     |      |
| maxLevel        | 最大级别                 |      |
| [] nextlevelExp | 到达下一级别需要的经验值 |      |
| maxHp           | 最大生命值               |      |
| currentHp       | 当前生命值               |      |
| maxMp           | 最大能量值               |      |
| currentMp       | 当前能量值               |      |
| attack          | 攻击力                   |      |
| defence         | 防御力                   |      |
|                 |                          |      |

| PlayerInfoManager.cs  |                  |              |
| --------------------- | ---------------- | ------------ |
| PlayerInfomation info | points           | 天赋点       |
|                       | face             | 面容状态     |
|                       | age              | 年龄         |
|                       | playTime         | 游戏时长     |
|                       | health           | 身体素质     |
|                       | poisonResistance | 毒抗         |
|                       | hitResistance    | 物抗         |
|                       | magicResistance  | 魔抗         |
|                       | attack           | 攻击力       |
|                       | critRate         | 暴击率       |
|                       | critPower        | 暴击幅度     |
|                       | power            | 力量         |
|                       | magic            | 魔力         |
|                       | speed            | 速度         |
|                       | pastSucess       | 回溯成功次数 |
|                       | frontSucess      | 前游成功次数 |
|                       | currentExp       | 当前经验值   |
|                       | playerLevel      | 级别         |
|                       | maxHp            | 最大生命值   |
|                       | currentHp        | 当前生命值   |
|                       | maxMp            | 最大能量值   |
| ItemData infoItemData | 装备的技能信息   |              |

| 角色属性数值计算公式0.1 | (所有固定数值只在开发测试中使用)                          |
| ----------------------- | --------------------------------------------------------- |
| 最大等级                | 10                                                        |
| 所需经验值基值          | 1000                                                      |
| 升级所需经验值递增倍数  | 1.1                                                       |
| **最大生命值(升级时)**  | **maxHp = 200+maxHp*1,2f**                                |
| **身体素质(嗑药)**      | **info.health += itemData.health;**                       |
| **最大生命值(嗑药)**    | **maxHp += health**                                       |
| 最大能量值（升级）      | maxMp += 20;                                              |
| 最大能量值（嗑药）      | maxMp += health*0.5;                                      |
| 天赋点获取              | 一个史莱姆掉落一个                                        |
| 天赋点使用              | 升级一个天赋消耗一个                                      |
| 毒抗（嗑药）            | poisonResistance +=itemData.poisonResistance              |
| 魔抗                    | magicResistance +=itemData.magicResistance                |
| 物抗                    | hitResistance =  defense*1.1                              |
| 攻击力                  | attack(playerinfomanager) = attack(Playerstate)+power*2.1 |
| 速度                    | 7                                                         |
| 暴击率                  | critRate = critRate +itemData.critRate                    |
| 暴击幅度                | critPower = criPower + itemData.critPower                 |
| pastSucess、frontSucess | 退出副本时结算                                            |
|                         |                                                           |
|                         |                                                           |

| 攻击数值计算公式 |      |
| ---------------- | ---- |
|                  |      |
|                  |      |
|                  |      |
|                  |      |
|                  |      |
|                  |      |
|                  |      |
|                  |      |
|                  |      |

### 过场动画

Timeline+CineMachine设计有对话台词的过场动画

### 攻击系统

包含冷却、普通攻击和技能等

### 天赋系统

使用ScriptableObject作为技能信息，然后进行Json转化，保存到本地。

### 背包系统&本地存储系统

使用ScriptableObject+IO

### 

### 随机地图系统

先生成随机位置->15种基础地图->随机机关



### 小地图

简易处理，摄像机法

### NPC&对话系统

可以全屏的非人对话和在角色头顶的人物对话

### 

### 云端资源分发&预下载

ACC+CCD

### 热更新

等待...



### 





