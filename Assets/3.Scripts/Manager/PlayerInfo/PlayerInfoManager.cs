﻿using System;
using UnityEngine;

[RequireComponent(typeof(Playerstate))]
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
    Playerstate playerstate;
    [Header("运行时观测")]
    public PlayerInfomation info;
    public ItemData infoItemData;

    public float playTime;
    //载入玩家信息

    //使用物品人物获得效果
    public void UseSomthing(ItemData itemData,int id)
    {

        switch (itemData.types)
        {
            case ItemType.equip://装备
                if (GameManager.instance.isEquipEquiped == false)
                {
                    GameManager.instance.isEquipEquiped = true;
                    //装备
                    //Equip(itemData, id);
                }
                else
                {
                    //这里是因为之前有过处理，点击之后 isequip为true 点击的是未装备的，执行的是装备操作
                    if (InventoryManager.instance.myBag.itemList[id].isEquip)//点击的是未装备的
                    {
                        //Equip(itemData, id);
                    }
                    else//点击的是已装备的 需要卸下
                    {
                        //仓库内所有技能设置为未装备
                        for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)
                        {
                            InventoryManager.instance.myBag.itemList[i].isEquip = false;
                        }
                        //玩家shootparent下清空
                        //ClearSkillParent(itemData);
                        //技能栏添加图片和技能描述
                        //infoItemData = new ItemData();
                        //通知gamemanager 没技能了，需要平A了
                        GameManager.instance.isSkillShoot = false;
                    }
                }
                break;
            case ItemType.medicine://回复类药物（消耗品）
                break;
            case ItemType.drug://毒药（消耗品）
                break;
            case ItemType.forever://永久性提高物品(消耗品）

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
                info.speed += itemData.speed;

                

                break;
            case ItemType.skill://技能
                if(GameManager.instance.isSkillShoot == false)
                {
                    GameManager.instance.isSkillShoot = true;
                    Equip(itemData,id);
                }
                else
                {
                    //这里是因为之前有过处理，点击之后 isequip为true 点击的是未装备的，执行的是装备操作
                    if (InventoryManager.instance.myBag.itemList[id].isEquip)//点击的是未装备的
                    {
                        Equip(itemData,id);
                    }
                    else//点击的是已装备的 需要卸下
                    {
                        //仓库内所有技能设置为未装备
                        for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)
                        {
                            InventoryManager.instance.myBag.itemList[i].isEquip = false;
                        }
                        //玩家shootparent下清空
                        ClearSkillParent(itemData);
                        //技能栏添加图片和技能描述
                        infoItemData = new ItemData();
                        //通知gamemanager 没技能了，需要平A了
                        GameManager.instance.isSkillShoot = false;
                    }
                }
               
                break;
            case ItemType.others://其他
                break;
            default:
                break;
        }
        
    }

    private void Equip(ItemData itemData,int id)
    {
        //仓库内所有技能设置为未装备
        for (int i = 0; i < InventoryManager.instance.myBag.itemList.Count; i++)
        {
            InventoryManager.instance.myBag.itemList[i].isEquip = false;
        }

        //当前技能设置为装备 
        InventoryManager.instance.myBag.itemList[id].isEquip = true;
        //玩家shootparent下清空
        ClearSkillParent(itemData);
        //技能栏添加图片和技能描述
        infoItemData = itemData;
        //在角色身上挂载技能
        //PoolMgr.GetInstance().GetObj(SkillTable.skillpath[itemData.skillId], SkillInit);
        GameObject obj =  ResMgr.GetInstance().Load<GameObject>(SkillTable.skillpath[itemData.skillId]);
        SkillInit(obj);
    }

    //在角色身上挂载技能
    private void SkillInit(GameObject arg0)
    {
        PlayerController playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        arg0.transform.SetParent(playerController.ShootParent);

        arg0.transform.localPosition = Vector3.zero;
        arg0.transform.localScale = Vector3.one;

    }

    //清空技能挂载点
    private void ClearSkillParent(ItemData itemData)
    {
        PlayerController playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        if (playerController.ShootParent.transform.childCount > 0)
            //PoolMgr.GetInstance().PushObj(SkillTable.skillpath[itemData.skillId], playerController.ShootParent.transform.GetChild(0).gameObject);
            Destroy(playerController.ShootParent.transform.GetChild(0).gameObject);
    }

    public void Start()
    {
        //TimerMgr.instance.CreateTimerAndStart(60, -1, SaveGame);
        playerstate = GetComponent<Playerstate>();
        playerstate.enabled = true;
        SyncInfo();
        //升级时同步过来数据 
        EventCenter.GetInstance().AddEventListener(EventCfg.LEVEL_UP,SyncInfo);
        EventCenter.GetInstance().AddEventListener<int>(EventCfg.ADD_EXP, SyncInfoExp);
    }
    void SyncInfoExp(int exp)
    {
        SyncInfo();
        GameSaveManager.instance.SaveGame();
    }
    void SyncInfo()
    {
        playerstate.enabled = true;

        info.playerName = Playerstate.instance.info.playerName;
        info.description = Playerstate.instance.info.description;
        info.playerLevel = Playerstate.instance.info.playerLevel;
        info.maxLevel = Playerstate.instance.info.maxLevel;
        info.maxHp = Playerstate.instance.info.maxHp ;
        info.maxMp = Playerstate.instance.info.maxMp ;
        info.currentHp = Playerstate.instance.info.currentHp ;
        info.currentMp = Playerstate.instance.info.currentMp ;
        info.attack = Playerstate.instance.info.attack ;
        info.hitResistance = Playerstate.instance.info.defense;
        info.nextlevelExp = Playerstate.instance.info.nextlevelExp[Playerstate.instance.info.playerLevel];

        //
    }

    /*
    private void SaveGame()
    {
        Debug.Log("保存一次");
        GameSaveManager.instance.SaveGame();
        info.playTime += 60;
    }*/

    public void Update()
    {
        /*playTime = Time.time;
        info.age = 35 + (int)info.playTime / 3600;
        info.playTime = (int)info.playTime;
        switch (info.age)
        {
            case 35:
                info.face = "完美无缺";
                break;
            case 36:
                info.face = "惊为天人";
                break;
            case 37:
                info.face = "平平无奇";
                break;
            case 38:
                info.face = "棱角分明";
                break;
            case 39:
                info.face = "帅";
                break;
            case 40:
                info.face = "普通人";
                break;
            case 41:
                info.face = "普通人以下";
                break;
            case 42:
                info.face = "出现了皱纹";
                break;
            default:
                info.face = "年老色衰";
                break;
        }*/
        if(SkillManager.instance != null)
        {
            SkillManager.instance.skillPoint = info.points;
        }

    }
}
