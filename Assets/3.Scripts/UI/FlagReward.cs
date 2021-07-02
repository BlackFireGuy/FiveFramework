using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlagReward : BasePanel
{
    public Button playAgain, mainMenu;
    Button beSure;
    public GameObject HomeMenu;
    public GameObject Sure;
    Text deadNum, pointNum;
    private void Start()
    {
        //resume = this.GetControl<Button>("Resume");
        //playAgain = this.GetControl<Button>("Play Again");
        //mainMenu = this.GetControl<Button>("Main Menu");
        beSure = this.GetControl<Button>("Be Sure");
        deadNum = this.GetControl<Text>("DeadNum");
        pointNum = this.GetControl<Text>("PointNum");




        //resume.onClick.AddListener(ResumeGame);
        playAgain.onClick.AddListener(PlayAgain);
        mainMenu.onClick.AddListener(GoToMainMenu);
        beSure.onClick.AddListener(ShowHomeMenu);

        deadNum.text = PlayerPrefs.GetInt("DeadNumPerMatch").ToString();
        pointNum.text = PlayerPrefs.GetInt("PointsPerMatch").ToString();

    }

    private void ShowHomeMenu()
    {
        HomeMenu.SetActive(true);
        Sure.SetActive(false);

    }

    private void PlayAgain()
    {
        Time.timeScale = 1;
        //ScenesMgr.GetInstance().RestartSccene();
        LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
        MusicMgr.GetInstance().ClearSounds();
        //胜利，则保存信息
        GameSaveManager.instance.SaveGame();
    }

    private void GoToMainMenu()
    {
        //恢复时间流动
        Time.timeScale = 1;
        //清空音效
        MusicMgr.GetInstance().ClearSounds();
        //载入场景
        //ScenesMgr.GetInstance().LoadScene(0, null);
        //ScenesMgr.GetInstance().LoadScene("Home", null);
        LevelLoader.instance.LoadNextLevel("Home");
        PlayerInfoManager.instance.info.frontSucess++;

        //保存背包
        GameSaveManager.instance.SaveGame();

    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager.GetInstance().HidePanel("Pause Menu");
    }


    private void OnEnable()
    {
        Time.timeScale = 0;
        
    }
}
