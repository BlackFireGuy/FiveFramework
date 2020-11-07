using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InfoPanel : BasePanel
{
    Button openInfo;

    public GameObject mainInfo;

    bool isOpen =false;
    //--------------------------个人信息-------------------------
    Text face;
    Text age;
    Text health;
    Text poisonResistance;
    Text hitResistance;
    Text magicResistance;
    Text attack;
    Text critRate;
    Text critPower;
    Text power;
    Text magic;
    //-------------------------------
    Text time;
    Text playTime;
    Text pastSuccess;
    Text forentSucces;

    // Start is called before the first frame update
    void Start()
    {
        openInfo = this.GetControl<Button>("OpenInfo");

        openInfo.onClick.AddListener(OpenInfo);
        //----------------------------------------------
        face = this.GetControl<Text>("face");
        age = this.GetControl<Text>("age");
        health = this.GetControl<Text>("health");

        poisonResistance = this.GetControl<Text>("poisonResistance");
        hitResistance = this.GetControl<Text>("hitResistance");
        magicResistance = this.GetControl<Text>("magicResistance");

        attack = this.GetControl<Text>("attack");
        critRate = this.GetControl<Text>("critRate");
        critPower = this.GetControl<Text>("critPower");

        power = this.GetControl<Text>("power");
        magic = this.GetControl<Text>("magic");

        time = this.GetControl<Text>("time");
        playTime = this.GetControl<Text>("playTime");
        pastSuccess = this.GetControl<Text>("pastSuccess");
        forentSucces = this.GetControl<Text>("forentSucces");

        mainInfo.SetActive(false);

    }

    void Update()
    {
        //刷新信息页
        RefreshInfo();
    }

    private void OpenInfo()
    {
        isOpen = !isOpen;
        mainInfo.SetActive(isOpen);

        //打开角色信息时载入角色信息 不是很合理啊，目前只在使用物品的时候才保存
        GameSaveManager.instance.LoadPlayerInfo();
        //Debug.Log(PlayerInfoManager.instance.info.ToString());
    }

    private void RefreshInfo()
    {
        //我踏马就把它注掉就可以了！，你妹的为啥啊，草，这个函数会new一个新的，我踏马浪废了这么多时间
        //time.text = (PlayerInfoManager.instance.LoadPlayerInfo().playTime/3600).ToString();

        //---------------
        age.text = PlayerInfoManager.instance.info.age.ToString();
        health.text = PlayerInfoManager.instance.info.health.ToString();

        poisonResistance.text = PlayerInfoManager.instance.info.poisonResistance.ToString();
        hitResistance.text = PlayerInfoManager.instance.info.hitResistance.ToString();
        magicResistance.text = PlayerInfoManager.instance.info.magicResistance.ToString();

        attack.text = PlayerInfoManager.instance.info.attack.ToString();
        critRate.text = PlayerInfoManager.instance.info.critRate.ToString();
        critPower.text = PlayerInfoManager.instance.info.critPower.ToString();

        power.text = PlayerInfoManager.instance.info.power.ToString();
        magic.text = PlayerInfoManager.instance.info.magic.ToString();
    }

}
