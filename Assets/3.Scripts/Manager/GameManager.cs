using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("场景设置")]
    public bool isMain;
    public bool isUpDown;
    public bool isInMap;
    public Transform bornPos;
    public Transform ObjPos;
    public static GameManager instance;
    private PlayerController player;

    private Door doorExit;

    public bool gameOver;
    [Header("场景过渡动画号")]
    public int crossNum;

    [Header("敌人列表")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("NPC列表")]
    public List<GameObject> npcs = new List<GameObject>();

    public bool isSkillShoot;
    public bool isEquipEquiped;
    public bool isBossDead;

    [Header("第一次进入场景生成的物品")]
    public List<GameObject> Objs = new List<GameObject>();
    public PlayableDirector playableDirector;
    public Camera mainCamera;
    bool isgameoverpaenlshowed = false;
    
    public enum GameMode { GamePlay, DialogueMoment,Normal}
    public GameMode gameMode;

    [Header("玩家的操作")]
    public float horizontal;
    public float vertical;
    

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameMode = GameMode.Normal;
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
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.SetCrossActive(crossNum);
            LevelLoader.instance.End();
        }
        if (PlayerPrefs.GetInt("CG") == 0)
        {
            //showCG 
            //进入Show模式, 成成初始物品
            gameMode = GameMode.GamePlay;
            if (!GameManager.instance.isInMap&&!GameManager.instance.isMain)
            {
                playableDirector.Play();
                for (int i = 0; i < Objs.Count; i++)
                {
                    Instantiate(Objs[i]).transform.position = ObjPos.position - i * Vector3.right;
                }
            }
            else
            {
                PlayerPrefs.GetInt("CG", 1);
                SetGameModeNormal();
            }


        }
        else
        {
            SetGameModeNormal();
        }
        

        

    }
    public void SetGameModeNormal()
    {
        gameMode = GameMode.Normal;
        SetGameSettings();
    }
    private void SetGameSettings()
    {
        
        if (isMain) return;
        GameSaveManager.instance.LoadGame();
        //----------------------------------------------------
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();

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
            UIManager.GetInstance().ShowPanel<InfoPanel>("Info", E_UI_Layer.Mid, null);
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

    internal void PauseTimeline(PlayableDirector playableDirector)
    {
        
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
