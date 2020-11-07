using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;
    public float textSpeed;
    int index = 0;
    bool textFinished;
    
    Sprite face01, face02;
    TextAsset textfile;
    

    List<string> textList = new List<string>();
    public bool touch;
    /*private void Awake()
    {
        GetTextFromFile(textfile);
        index = 0;
    }*/
    private void OnEnable()
    {
        textfile = DialogManager.instance.Textfile;
        face01 = DialogManager.instance.Face01;
        face02 = DialogManager.instance.Face02;
        GetTextFromFile(textfile);
        index = 0;
        StartCoroutine(SetTextUI());
    }

    private void Update()
    {

        if((DialogManager.instance.isattack||(Input.GetKeyDown(KeyCode.R)))&& index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if ((DialogManager.instance.isattack || (Input.GetKeyDown(KeyCode.R))) && textFinished)
        {
            StartCoroutine(SetTextUI());
        }

        //离开一定区域，这里是指离开触发器，也就是npc身上的button不显示，则关闭dialog，并初始化
        //清空列表，index归位
        if (!DialogManager.instance.isButtonActive)
        {
            textList.Clear();
            index = 0;
            this.gameObject.SetActive(false);
        }
    }

   IEnumerator SetTextUI()
   {
        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = face01;
                index++;
                break;
            case "B\r":
                faceImage.sprite = face02;
                index++;
                break;
            default:
                Debug.Log("无法匹配到A或者B");
                break;
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }

    public void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData =  file.text.Split('\n');

        foreach (var item in lineData)
        {
            textList.Add(item);
        }
    }

    public void PutDown()
    {
        DialogManager.instance.isattack = true;
    }
    public void PutUp()
    {
        DialogManager.instance.isattack = false;
    }
}
