﻿using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfomation 
{
    public int points;//天赋点
    public string face;//面容状态
    public int age;//年龄
    public float playTime;//游戏时长
    public float health;//身体素质
    public float poisonResistance;//毒抗
    public float hitResistance;//物抗
    public float magicResistance;//魔抗
    public float attack;//攻击力
    public float critRate;//暴击率
    public float critPower;//暴击幅度
    public float power;//力量
    public float magic;//魔力
    public float speed;//速度

    
    public float currentExp;//当前经验值
    public string playerName, description;//角色名称以及描述
    public int playerLevel, maxLevel;//级别/最大级别
    public float currentHp, currentMp, maxHp, maxMp;//当前生命值、能量值、最大生命值、能量值

    public int nextlevelExp;//

    //--------------------------------------
    public int pastSucess;//回溯成功次数
    public int frontSucess;//前游成功次数



    /*PlayerInfomation()
    {
        health = 10;
        attack = 1;
    }*/

    /*public void PlayerInfoManager()
    {
        health = 10;
        attack = 1;
    }*/
}

public static class SkillTable{
    public static List<string> skillpath = new List<string> {
        "Prefabs/Bomb&TVAndSoOn/GUN&Bullet/Gun001",
        "Prefabs/Bomb&TVAndSoOn/GUN&Bullet/Gun002",
    };

}

