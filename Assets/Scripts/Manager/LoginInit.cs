using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginInit : MonoBehaviour
{
    public void Start()
    {
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();
        UIManager.GetInstance().ShowPanel<Main>("Main", E_UI_Layer.Null, null);
        MusicMgr.GetInstance().PlayBMusic("BK2");
    }
}
