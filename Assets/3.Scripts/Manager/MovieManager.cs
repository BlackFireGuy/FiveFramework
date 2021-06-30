using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieManager : MonoBehaviour
{
    public static MovieManager instance;

    public GameObject dialogBox;




    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }

    internal void SetDialogue(string characterName, string dialogueLine, int dialogueSize)
    {
        //将轨道上设置的信息展示在画布上
    }

    internal void ToggleDialogueBox(bool v)
    {
        //显示对话框或者画布
    }
}
