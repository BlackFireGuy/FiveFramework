using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : BasePanel
{
    Button skill, attack;

    PlayerController player;
    void Start()
    {
        skill = this.GetControl<Button>("Skill");
        attack = this.GetControl<Button>("Attack");

        skill.onClick.AddListener(SkillAttack);
        attack.onClick.AddListener(NormalAttack);
    }

    private void Update()
    {
        if(player == null)
            player = FindObjectOfType<PlayerController>();
    }

    private void NormalAttack()
    {
        if (player != null)
        {
            //player.ButtonJump();
            player.Attack();
        }
    }

    private void SkillAttack()
    {
        //角色放技能
        if (player != null)
        {
            Debug.Log("技能攻击");
            //player.Attack();
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
