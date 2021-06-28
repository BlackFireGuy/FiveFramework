using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : BasePanel
{
    public static LevelLoader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }


    public GameObject[] CrossList;
    //public float transitionTime = 1f;
    
    public Animator transition;

    private void Start()
    {
        
    }
    /// <summary>
    /// 用于在各个场景内设置过渡动画
    /// </summary>
    /// <param name="num">过渡动画</param>
    public void SetCrossActive(int num)
    {

        if (num > CrossList.Length) num = 1;
        
        foreach(GameObject item in CrossList)
        {
            item.SetActive(false);
        }
        CrossList[num].SetActive(true);
        transition = this.GetComponentInChildren<Animator>();

    }


    public void LoadNextLevel(string name)
    {
        //清空资源缓存池
        PoolMgr.GetInstance().Clear();
        //清空事件缓存池
        EventCenter.GetInstance().Clear();
        //清空音效池
        MusicMgr.GetInstance().ClearSounds();

        StartCoroutine(LoadLevel(name));
    }
    public void LoadNextLevel(int id)
    {
        //清空资源缓存池
        PoolMgr.GetInstance().Clear();
        //清空事件缓存池
        EventCenter.GetInstance().Clear();
        //清空音效池
        MusicMgr.GetInstance().ClearSounds();
        
        StartCoroutine(LoadLevel(id));
    }
    IEnumerator LoadLevel(string name)
    {
        SetCrossActive(GameManager.instance.crossNum);
        if (transition == null)
            transition = this.GetComponent<Animator>();

        transition.SetTrigger("start");

        
        yield return new WaitForSeconds(transition.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        ScenesMgr.GetInstance().LoadScene(name,null); ;
    }
    IEnumerator LoadLevel(int id)
    {
        SetCrossActive(GameManager.instance.crossNum);
        if (transition == null)
            transition = this.GetComponent<Animator>();

        transition.SetTrigger("start");

        yield return new WaitForSeconds(transition.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        ScenesMgr.GetInstance().LoadScene(id, null); ;
    }
    public void End()
    {
        transition.SetTrigger("end");
    }
}
