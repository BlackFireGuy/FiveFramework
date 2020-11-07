using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("场景设置")]
    public bool isUpDown;
    public bool isInMap;

    public static GameManager instance;
    private PlayerController player;

    private Door doorExit;

    public bool gameOver;

    [Header("敌人列表")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("NPC列表")]
    public List<GameObject> npcs = new List<GameObject>();


    public bool isBossDead;
    //[Header("个人信息")]//包含各种详细信息，包括背包，合成表等
    /*public Button MainInfo;
    public GameObject bag;
    bool isBagOpen = false;*/
    bool isgameoverpaenlshowed = false;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if(player!=null)
            gameOver = player.isDead;
        //UIMgr.instance.GameOverUI(gameOver);
        if (gameOver&&!isgameoverpaenlshowed)
        {
            UIManager.GetInstance().ShowPanel<GameOverPanel>("Game Over Panel", E_UI_Layer.Mid, null);
            isgameoverpaenlshowed = true;
        }

        /*else
        {
            isgameoverpaenlshowed = false;
            UIManager.GetInstance().HidePanel("Game Over Panel");
        }*/
            
    }

    /*public void OpenBag()
    {
        isBagOpen = !isBagOpen;
        if (isBagOpen) Time.timeScale = 0;
        else Time.timeScale = 1;
        bag.SetActive(isBagOpen);
    }*/

    public void Start()
    {
        GameSaveManager.instance.LoadGame();
        //MainInfo.onClick.AddListener(OpenBag);
        //----------------------------------------------------
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();
        //Debug.Log("载入home");
        if (isInMap)
        {
            UIManager.GetInstance().ShowPanel<MapPanel>("Map", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<HealthBar>("Health Bar", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<PauseButton>("Pause Button", E_UI_Layer.Mid, null);
           
        }
        else
        {
            UIManager.GetInstance().ShowPanel<InfoPanel>("Info", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<SettingsPanel>("Settings", E_UI_Layer.Mid, null);
        }
        UIManager.GetInstance().ShowPanel<BagPanel>("Bag Panel", E_UI_Layer.Mid, null);
        UIManager.GetInstance().ShowPanel<DialogPanel>("Dialog Panel", E_UI_Layer.Mid, null);
        UIManager.GetInstance().ShowPanel<Controller>("Controller", E_UI_Layer.Mid, null);
        
        //----------------------------------------------------
        //再初始化角色和NPC
        if (isUpDown)//第三人称俯视角
        {
            ResMgr.GetInstance().Load<GameObject>("Prefabs/Player");
        }
        else//第三人称2D横板
        {
            ResMgr.GetInstance().Load<GameObject>("Prefabs/Player Control");
        }
            
        //播放音乐
        MusicMgr.GetInstance().PlayBMusic("BK1");
    }
    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            doorExit.OpenDoor();
            SaveData();
        }
    }
    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }
    public void IsDoor(Door door)
    {
        doorExit = door;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public float LoadHealth()
    {
        /*if (!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 13f);
        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth");*/
        
        float currentHealth = PlayerInfoManager.instance.info.health;
        return currentHealth;
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.Save();
    }
}
