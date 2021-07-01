using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginInit : MonoBehaviour
{
    public void Awake()
    {
        //0.资源预下载
        ResMgr.GetInstance().Preload();
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();

        //2实例化切换场景工具并设置跨场景不销毁
        //PathCfg.PATH_UI+ "LevelLoader"  变为"LevelLoader"
        ResMgr.GetInstance().Load<GameObject>(PathCfg.UI_LevelLoader, (obj)=>{
            var instance = Instantiate(obj.Result);
            GameObject.DontDestroyOnLoad(instance);
        });
        //3.显示UI
        UIManager.GetInstance().ShowPanel<Main>("Main", E_UI_Layer.Mid, null);
        //4.放音乐
        MusicMgr.GetInstance().PlayBMusic("BK2");
    }
}
