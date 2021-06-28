using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public GameObject Text;
    public GameObject Button;
    //public GameObject talkUI;
    [Header("文本文件")]
    public TextAsset textfile;

    [Header("头像")]
    public Sprite face01, face02;

    public bool beginTalk;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //可以计划在这里进行UDialogPrefab的赋值
            Button.SetActive(true);
            if(Text != null)
                Text.SetActive(true);
            else
            {
                //恢复默认的DIalogUI
                DialogManager.instance.SetDefaultDialogUI();
            }
            DialogManager.instance.isButtonActive = true;
        }
            

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button.SetActive(false);
            if (Text != null)
                Text.SetActive(false);
            DialogManager.instance.Dialog = null;
            DialogManager.instance.isButtonActive = false;
        }
            


    }

    private void Update()
    {
        if (Button.activeSelf && (Input.GetKeyDown(KeyCode.R)|| DialogManager.instance.isattack))
        {
            Button.SetActive(false);
            //可以计划在这里进行UDialogPrefab的赋值
            if (Text != null)
            {
                //UIManager.GetInstance().HidePanel("Dialog Panel");
                Text.SetActive(true);
                
            }
            else
                DialogManager.instance.SetDefaultDialogUI();
            //talkUI.SetActive(true);
            DialogManager.instance.SetDialogInfo(textfile, face01, face02, Show);
        }
    }

    public virtual void Show()
    {
        
    }
    
}
