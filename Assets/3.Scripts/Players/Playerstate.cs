using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerstateInfo{
    public string playerName, description;

    public int playerLevel, maxLevel;

    public int currentExp;

    public int[] nextlevelExp;//貌似更新了，只能解析单个对象，不能解析多个对象（即对象是数组） 应该了解原理之后就可以思考对策

    public float currentHp, currentMp, maxHp, maxMp;
    public float attack, defense;

    public void Init()
    {
        nextlevelExp = new int[maxLevel + 1];
        nextlevelExp[0] = 0;
        if(nextlevelExp.Length<2)
        {
            Debug.Log("nextlevelExp 长度小于2");
            //nextlevelExp[1] = 1000;
        }
        else
        {
            nextlevelExp[1] = 1000;
        }
        
        for (int i = 2; i < maxLevel; i++)
        {
            nextlevelExp[i] = Mathf.RoundToInt(nextlevelExp[i - 1] * 1.1f);
        }
        
    }
}



public class Playerstate : MonoBehaviour
{

    public static Playerstate instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            
        }

        info.Init();

    }
   
    public PlayerstateInfo info;
    

    

    private void Start()
    {

        gameObject.SetActive(true);
        //监听经验拾取事件
        EventCenter.GetInstance().AddEventListener<int>(EventCfg.ADD_EXP, AddExp);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AddExp(600);
            //EventCenter.GetInstance().EventTrigger(EventCfg.ADD_EXP, 600);
        }
    }

    public void AddExp(int amount)
    {
        Debug.Log("经验增加");
        info.currentExp += amount;
        PlayerInfoManager.instance.info.currentExp = info.currentExp;
        if (info.playerLevel < info.maxLevel && info.currentExp >= info.nextlevelExp[info.playerLevel])
        {
            levelUp();
            //触发升级事件
            EventCenter.GetInstance().EventTrigger(EventCfg.LEVEL_UP);

        }
        if (info.playerLevel >= info.maxLevel)
        {
            info.currentExp = 0;
        }
    }
    private void levelUp()
    {
        info.currentExp -= info.nextlevelExp[info.playerLevel];
        info.playerLevel++;
        info.maxHp  =Mathf.RoundToInt(200 + info.maxHp * 1.2f);
        info.currentHp = info.maxHp;
        info.maxMp += 20;
        info.currentMp = info.maxMp;
        info.attack = Mathf.CeilToInt(info.attack * 1.1f);//取上线最小整数7.8取8
        info.defense = Mathf.CeilToInt(info.defense * 1.05f);
    }
}
