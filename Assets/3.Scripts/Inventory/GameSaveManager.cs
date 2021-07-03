using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;
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

    string path = "/game_SaveData";
    [Header("仓库")]
    public List<Inventory> inventoyies = new List<Inventory>();
    [Header("技能树")]
    public List<SkillTree> skillTrees = new List<SkillTree>();
    string mainInfoDir = "/infomation.txt";
    string playerStateDir = "/playerState.txt";
    /// <summary>
    /// 1经过传送门时保存
    /// 2升级技能树时保存
    /// 3使用物品时保存
    /// 4打开人物信息表时保存
    /// //5增加经验时保存
    /// 
    /// </summary>
    public void SaveGame()
    {
        SaveInventory();
        SavePlayerInfo();
        Debug.Log("保存");
    }
    /// <summary>
    /// 游戏开始时读取存档，没有存档则返回
    /// </summary>
    public void LoadGame()
    {
        LoadInventory();
        LoadPlayerInfo();
        Debug.Log("载入");
    }

    public void NewGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
            


        }
        else
        {
            Directory.Delete(Application.persistentDataPath + path, true);
            Directory.CreateDirectory(Application.persistentDataPath + path);
            
        }
        PlayerPrefs.DeleteAll();
    }

    public bool IsSaved()
    {
        if (File.Exists(Application.persistentDataPath + path + mainInfoDir))
        {
            return true;
        }
        else//如果没有存档 就立下flag
        {
            return false; ;
        }
    }
    /// <summary>
    /// 保存仓库信息
    /// </summary>
    public void SaveInventory()
    {
        //仓库
        foreach (Inventory item in inventoyies)
        {
            if (!Directory.Exists(Application.persistentDataPath + path))
            {
                Directory.CreateDirectory(Application.persistentDataPath + path);
            }
            BinaryFormatter formatter = new BinaryFormatter();//二进制转化
            FileStream file = File.Create(Application.persistentDataPath + path + "/" + item.name.ToString()+".txt");
            var json = JsonUtility.ToJson(item);
            //Debug.Log(json);
            formatter.Serialize(file, json);
            file.Close();
        }
        //技能树
        foreach (SkillTree skillTree in skillTrees)
        {
            if (!Directory.Exists(Application.persistentDataPath + path))
            {
                Directory.CreateDirectory(Application.persistentDataPath + path);
            }
            foreach(SkillData item in skillTree.skillList)
            {
                BinaryFormatter formatter = new BinaryFormatter();//二进制转化
                FileStream file = File.Create(Application.persistentDataPath + path + "/" + item.name.ToString() + ".txt");
                var json = JsonUtility.ToJson(item);
                //Debug.Log(json);
                formatter.Serialize(file, json);
                file.Close();
            }
        }

    }
    /// <summary>
    /// 载入仓库信息
    /// </summary>
    public void LoadInventory()
    {
        //仓库
        foreach (Inventory item in inventoyies)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + path + "/" + item.name.ToString() + ".txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + path + "/" + item.name.ToString() + ".txt", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), item);
                file.Close();
            }
        }
        //技能树
        foreach (SkillTree skillTree in skillTrees)
        {
            foreach (SkillData item in skillTree.skillList)
            {
                BinaryFormatter bf = new BinaryFormatter();
                if (File.Exists(Application.persistentDataPath + path + "/" + item.name.ToString() + ".txt"))
                {
                    FileStream file = File.Open(Application.persistentDataPath + path + "/" + item.name.ToString() + ".txt", FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), item);
                    file.Close();
                }
            }
        }

    }

    /// <summary>
    /// 保存玩家信息
    /// </summary>
    public void SavePlayerInfo()
    {
        if (!Directory.Exists(Application.persistentDataPath + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
        }
        BinaryFormatter formatter = new BinaryFormatter();//二进制转化

        FileStream file = File.Create(Application.persistentDataPath + path + mainInfoDir);
        var json = JsonUtility.ToJson(PlayerInfoManager.instance.info);

        formatter.Serialize(file, json);
        file.Close();

        //playerstate
        if (!Directory.Exists(Application.persistentDataPath + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
        }
        BinaryFormatter formatter1 = new BinaryFormatter();//二进制转化

        FileStream file1 = File.Create(Application.persistentDataPath + path + playerStateDir);
        var json1 = JsonUtility.ToJson(Playerstate.instance.info);

        formatter.Serialize(file1, json1);
        file1.Close();
    }
    /// <summary>
    /// 载入玩家信息，貌似还需要同时保存和载入PlayerState, 貌似还是可以合到一起的
    /// </summary>
    public void LoadPlayerInfo()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + path + mainInfoDir))
        {
            FileStream file = File.Open(Application.persistentDataPath + path + mainInfoDir, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), PlayerInfoManager.instance.info);
            file.Close();
        }
        //playerstate
        BinaryFormatter bf1 = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + path + playerStateDir))
        {
            FileStream file1 = File.Open(Application.persistentDataPath + path + playerStateDir, FileMode.Open);
            //坑：JsonUtility
            //1.只能解析单个对象，不能解析多个对象（即对象是数组）必须套个壳子，让其变成一个元素
            JsonUtility.FromJsonOverwrite((string)bf1.Deserialize(file1), Playerstate.instance.info);
            file1.Close();
        }


    }
}
