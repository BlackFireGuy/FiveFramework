using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻击 各种攻击姿势
/// </summary>
public class ArmourHit : MonoBehaviour
{
    //计时器
    //float timer;//攻击0.5s之后才可以进行下一次攻击

    private Rigidbody2D rb;
    private Animator ani;
    private AnimatorStateInfo stateInfo;
    AnimatorOverrideController overrideController;
    RuntimeAnimatorController tempController;

    public bool isPlayer;
    [Header("动画片段-轻击")]
    public AnimationClip[] attacks_lightly;//默认up,middle,down,upM,downM
    [Header("动画片段-普通攻击")]
    public AnimationClip[] attacks_normally;
    [Header("动画片段-重击")]
    public AnimationClip[] attacks_heavily;
    string attackClipNameLightly = "attack_middle_forward_lightly";
    string attackClipNameNormally = "attack_middle_forward_normal";
    string attackClipNameHeavilly = "attack_middle_forward_heavily";
    [Header("技能：前冲距离/面向方向")]
    public float speed=1f;
    [Header("攻击检测区位置")]
    public Vector3 hitpointPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        tempController = ani.runtimeAnimatorController;
        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = tempController;


        AddAnimationEvents();

        
    }

    private void Update()
    {
        ani.SetBool("isAttacking", BeatManager.instance.isAttacking);

        //若动画为三种状态之一并且已经播放完毕
        stateInfo = ani.GetCurrentAnimatorStateInfo(1);
        if ((stateInfo.IsName("Beat.attack1") || stateInfo.IsName("Beat.attack2") || stateInfo.IsName("Beat.attack3")) && stateInfo.normalizedTime >= 0.9f)
        {
            BeatManager.instance.currentAttack = 0;   //将currentAttack重置为0，即Idle状态
            ani.SetInteger("currentAttack", 0);
            BeatManager.instance.isAttacking = false;
        }


    }

    public void Attack()
    {
        //根据连击层数和击打方向改变击打动作
        switch (BeatManager.instance.dir)
        {
            case BeatDir.up:
                PlayAnimation(attackClipNameLightly, attacks_lightly[0]);
                PlayAnimation(attackClipNameNormally, attacks_normally[0]);
                PlayAnimation(attackClipNameHeavilly, attacks_heavily[0]);
                break;
            case BeatDir.middle:
                PlayAnimation(attackClipNameLightly, attacks_lightly[1]);
                PlayAnimation(attackClipNameNormally, attacks_normally[1]);
                PlayAnimation(attackClipNameHeavilly, attacks_heavily[1]);
                break;
            case BeatDir.down:
                PlayAnimation(attackClipNameLightly, attacks_lightly[2]);
                PlayAnimation(attackClipNameNormally, attacks_normally[2]);
                PlayAnimation(attackClipNameHeavilly, attacks_heavily[2]);
                break;
            case BeatDir.upM:
                break;
            case BeatDir.downM:
                break;
        }
        //击打
        PlayAttack();
    }
    private void PlayAttack()
    {
        //若处于Idle状态，则直接打断并过渡到attack1(攻击阶段一)
        if (stateInfo.IsName("Beat.empty") && BeatManager.instance.currentAttack == 0)
        {
            BeatManager.instance.attacktime = Time.time;
            BeatManager.instance.currentAttack = 1;
            ani.SetTrigger("attack");
            ani.SetInteger("currentAttack", BeatManager.instance.currentAttack);
        }
        //如果当前动画处于attack1(攻击阶段一)并且该动画播放进度小于66%，此时按下攻击键可过渡到攻击阶段二
        else if (stateInfo.IsName("Beat.attack1") && BeatManager.instance.currentAttack == 1 && stateInfo.normalizedTime < 0.70f)
        {
            BeatManager.instance.attacktime = Time.time;
            BeatManager.instance.currentAttack = 2;
        }
        //同上
        else if (stateInfo.IsName("Beat.attack2") && BeatManager.instance.currentAttack == 2 && stateInfo.normalizedTime < 0.70f)
        {
            BeatManager.instance.attacktime = Time.time;
            BeatManager.instance.currentAttack = 3;
        }

        //生成特效
        //

       

    }
    //-----------------------------------------------
    //events
    void SetIsMoveTrue()
    {
        rb.velocity = new Vector2(BeatManager.instance.playerForwardDir * speed, rb.velocity.y);

       

    }
    void SetIsMoveFalse()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        
    }
    /*void CurrentAttack(int i)
    {
        BeatManager.instance.currentAttack=i;
    }*/
    void GoToNextAttackAction()
    {
        ani.SetInteger("currentAttack", BeatManager.instance.currentAttack);
    }
    //给动画添加events
    private void AddAnimationEvents()
    {
        AnimationEvent newEvent = new AnimationEvent();
        newEvent.functionName = "SetIsMoveTrue";
        newEvent.time = 0.0f;
        AnimationEvent newEvent1 = new AnimationEvent();
        newEvent1.functionName = "SetIsMoveFalse";
        newEvent1.time = 0.20f;
        AnimationEvent newEvent2 = new AnimationEvent();
        newEvent2.functionName = "GoToNextAttackAction";
        newEvent2.time = 0.66f;
        
        for (int i = 0; i < attacks_lightly.Length; i++)
        {
            attacks_lightly[i].AddEvent(newEvent);
            attacks_lightly[i].AddEvent(newEvent1);
            newEvent2.intParameter = 1;
            attacks_lightly[i].AddEvent(newEvent2);
        }
        for (int i = 0; i < attacks_normally.Length; i++)
        {
            attacks_normally[i].AddEvent(newEvent);
            attacks_normally[i].AddEvent(newEvent1);
            newEvent2.intParameter = 2;
            attacks_normally[i].AddEvent(newEvent2);
        }
        for (int i = 0; i < attacks_heavily.Length; i++)
        {
            attacks_heavily[i].AddEvent(newEvent);
            attacks_heavily[i].AddEvent(newEvent1);
            newEvent2.intParameter = 3;
            attacks_heavily[i].AddEvent(newEvent2);
        }
    }
    //播放动画
    private void PlayAnimation(string clipName,AnimationClip animationClip)
    {
        /*if (BeatManager.instance.attacktime + 0.5f < Time.time)//当时间在上一次攻击0.5s之后可以进行下一次攻击
        {
            if(BeatManager.instance.currentAttack < 3)
            {
                overrideController[clipName] = animationClip;
                ani.runtimeAnimatorController = overrideController;
                BeatManager.instance.attacktime = Time.time;
                BeatManager.instance.currentAttack++;

                ani.SetTrigger("attack");
                ani.SetInteger("currentAttack", BeatManager.instance.currentAttack);
            }
            else
            {
                BeatManager.instance.currentAttack = 0;
                ani.SetInteger("currentAttack", 1);
            }
            
        }
        else
        {
            return;
        }*/


        overrideController[clipName] = animationClip;
        ani.runtimeAnimatorController = overrideController;

        

    }

    
}
