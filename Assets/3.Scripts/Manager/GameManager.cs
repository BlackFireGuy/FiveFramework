using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("场景设置")]
    public bool isMain;
    public bool isUpDown;
    public bool isInMap;
    public Transform bornPos;
    public static GameManager instance;
    private PlayerController player;

    private Door doorExit;

    public bool gameOver;

    [Header("敌人列表")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("NPC列表")]
    public List<GameObject> npcs = new List<GameObject>();

    public bool isSkillShoot;
    public bool isEquipEquiped;
    public bool isBossDead;
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
    }
    public void Start()
    {

        if (isMain) return;
        GameSaveManager.instance.LoadGame();
        //----------------------------------------------------
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();
        if(LevelLoader.instance != null)
        {
            LevelLoader.instance.End();
        }

        UIManager.GetInstance().ShowPanel<DialogPanel>("Dialog Panel", E_UI_Layer.Mid, null);
        if (isInMap)
        {
            UIManager.GetInstance().ShowPanel<MapPanel>("Map", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<PauseButton>("Pause Button", E_UI_Layer.Mid, null);
            //UIManager.GetInstance().ShowPanel<BagPanel>("Bag Panel", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<Controller>("Controller", E_UI_Layer.Mid, null);
        }
        else
        {
            UIManager.GetInstance().ShowPanel<ButtonInHome>("Button In Home", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<Controller>("Controller", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<SettingsPanel>("Settings", E_UI_Layer.Mid, null);
            //UIManager.GetInstance().ShowPanel<InfoPanel>("Info", E_UI_Layer.Mid, null);
        }
        //----------------------------------------------------
        //再初始化角色和NPC
        if (isUpDown)//第三人称俯视角
        {
            GameObject obj = ResMgr.GetInstance().Load<GameObject>("Prefabs/Player/BlackMan4");
            obj.transform.position = bornPos.position;

        }
        else//第三人称2D横板
        {
            GameObject obj = ResMgr.GetInstance().Load<GameObject>("Prefabs/Player/BlackMan4");
            obj.transform.position = bornPos.position;
        }
            
        //播放音乐
        //MusicMgr.GetInstance().PlayBMusic("BK1");
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
            //doorExit.OpenDoor();
            
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
        float currentHealth = PlayerInfoManager.instance.info.health;
        return currentHealth;
    }
}
