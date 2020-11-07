using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject Dialog;


    TextAsset textfile;

    Sprite face01, face02;

    public bool isattack;
    public bool isButtonActive;
    public void SetDialogInfo(TextAsset file,Sprite f01, Sprite f02)
    {
        textfile = file;
        face01 = f01;
        face02 = f02;
        if(Dialog!=null)
            Dialog.SetActive(true);
    }

    public TextAsset Textfile
    {
        get { return textfile; }
    }

    public Sprite Face01
    {
        get { return face01; }
    }

    public Sprite Face02
    {
        get { return face02; }
    }

}
