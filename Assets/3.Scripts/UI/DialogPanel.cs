using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : BasePanel
{
    
    public bool isFullScreen;
    public GameObject dialog;

    private void Start()
    {
        //全屏对话需要单独设置
        //DialogPanel是共用的，在GameManager中执行生成，并在生成的时候直接赋给Dialogmanager。
        if (isFullScreen)
        {
            DialogManager.instance.DialogMachine = dialog;
        }
        else
        {
            DialogManager.instance.Dialog = dialog;
        }
    }
    private void OnEnable()
    {
        
            DialogManager.instance.Dialog = dialog;

    }

}
