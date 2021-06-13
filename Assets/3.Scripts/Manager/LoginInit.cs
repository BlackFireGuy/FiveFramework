using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginInit : MonoBehaviour
{
    public void Start()
    {
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();
        //UIManager.GetInstance().ShowPanel<LevelLoader>("LevelLoader", E_UI_Layer.Mid, null);
        GameObject obj = ResMgr.GetInstance().Load<GameObject>(PathCfg.PATH_UI+ "LevelLoader");
        GameObject.DontDestroyOnLoad(obj);
        UIManager.GetInstance().ShowPanel<Main>("Main", E_UI_Layer.Mid, null);
        MusicMgr.GetInstance().PlayBMusic("BK2");
    }
}
