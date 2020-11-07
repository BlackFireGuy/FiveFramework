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
    string inventoryDir = "/inventory.txt";
    string mainInfoDir = "/infomation.txt";

    public Inventory myInventory;
    public void SaveGame()
    {
        SaveInventory();
        SavePlayerInfo();
    }

    public void LoadGame()
    {
        LoadInventory();
        LoadPlayerInfo();
    }

    public bool IsSaved()
    {
        if (File.Exists(Application.persistentDataPath + path + inventoryDir)&& File.Exists(Application.persistentDataPath + path + mainInfoDir))
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
        if (!Directory.Exists(Application.persistentDataPath + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
        }
        BinaryFormatter formatter = new BinaryFormatter();//二进制转化
        FileStream file = File.Create(Application.persistentDataPath + path + inventoryDir);
        var json = JsonUtility.ToJson(myInventory);
        //Debug.Log(json);
        formatter.Serialize(file, json);
        file.Close();
    }
    /// <summary>
    /// 载入仓库信息
    /// </summary>
    public void LoadInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + path + inventoryDir))
        {
            FileStream file = File.Open(Application.persistentDataPath + path + inventoryDir, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), myInventory);
            file.Close();
            //Debug.Log("加载完背包!");
        }
        InventoryManager.RefreshItem();
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
        //Debug.Log(json);
        formatter.Serialize(file, json);
        file.Close();
    }
    /// <summary>
    /// 载入玩家信息
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
    }
}
