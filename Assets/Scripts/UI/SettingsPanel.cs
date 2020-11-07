using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BasePanel
{
    public GameObject menu;

    public Slider musicSlider;
    public Slider soundSlider;
    public Button btn;
    public Button exist;


    bool isOpen;
    void Start()
    {
        //btn = this.GetControl<Button>("Setting");
        /*musicSlider = this.GetControl<Slider>("MusicSlider");
        soundSlider = this.GetControl<Slider>("SoundSlider");*/

        //btn.onClick.AddListener(MenuShow);



        exist.onClick.AddListener(OnBackMenu);
    }

    private void OnBackMenu()
    {
        ScenesMgr.GetInstance().LoadScene("Main", null);
    }

    public void MenuShow()
    {
        isOpen = !isOpen;
        menu.SetActive(isOpen);

    }

    void Update()
    {
        MusicMgr.GetInstance().ChangeBKValue(musicSlider.value*2);
        MusicMgr.GetInstance().ChangeSoundValue(soundSlider.value*2);
    }
}
