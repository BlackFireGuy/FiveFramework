using System;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            Debug.Log("删除玩家信息管理器desory PlayerinfoManager");
        }
            
    }


    public PlayerInfomation info;

    public ItemData ItemData;

    //进入地图前保存玩家信息
    public void SavePlayerInfo(PlayerInfomation playerInfo)
    {

    }

    //载入玩家信息

    //使用物品人物获得效果
    public void UseSomthing(ItemData itemData)
    {
        //根据物品的类型选择是装备、恢复，还是永久性提高
        info.health += itemData.health;
        info.poisonResistance += itemData.poisonResistance;
        info.hitResistance += itemData.hitResistance;
        info.magicResistance += itemData.magicResistance;
        info.attack += itemData.attack;
        info.critRate += itemData.critRate;
        info.critPower += itemData.critPower;
        info.power += itemData.power;
        info.magic += itemData.magic;
    }


    public PlayerInfomation GetPlayerInfo()
    {
        //return info;
        
        PlayerInfomation obj = info;
        return obj;

    }
}
