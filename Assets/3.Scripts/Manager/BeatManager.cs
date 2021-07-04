using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public static BeatManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    [Header("Player的攻击特性：力道/攻击方向/玩家朝向/时间流逝速度")]
    public int force;//力道
    public BeatDir dir;//方向
    public int playerForwardDir = 1;//玩家朝向
    public float speed =1;//时间流逝速度
    [Header("状态标识：是否攻击中/当前连招层级/连击数/连击间隔")]
    public bool isAttacking = false;
    public int currentAttack = 0;
    public int combo = 0;//连击数
    public float rate = 1;
    public float attacktime = 0;//敌人受击和我方攻击时更新

    private FixedJoystick joystick;
    
    private void Update()
    {
        if (joystick == null)
        {
            joystick = FindObjectOfType<FixedJoystick>();
            return;
        }
            
        //游戏流逝速度
        Time.timeScale = speed;





        //判断是否连击
        if(Time.time > attacktime + rate)//超时
        {
            isAttacking = false;//不在攻击中
            currentAttack = 0;//连招进度恢复为0；

            force = 2;
        }
        else
        {
            isAttacking = true;
            force = currentAttack * force;
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        //根据摇杆判断击打方向
        if(Mathf.Abs(ver) >0.01 || Mathf.Abs(hor) > 0.01)
        {
            CheckInpout(hor, ver);
            GameManager.instance.vertical = ver;
        }
        else
        {
            CheckInpout(joystick.Horizontal, joystick.Vertical);
            GameManager.instance.vertical = joystick.Vertical;
        }

        

    }

    private void CheckInpout(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            if (vertical > 0)
            {
                if (horizontal > vertical)
                {
                    dir = BeatDir.middle;
                }
                else
                {
                    dir = BeatDir.up;
                }
            }
            else
            {
                if (horizontal > Mathf.Abs(vertical))
                {
                    dir = BeatDir.middle;
                }
                else
                {
                    dir = BeatDir.down;
                }
            }
        }
        else
        {
            if (vertical > 0)
            {
                if (Mathf.Abs(horizontal) > vertical)
                {
                    dir = BeatDir.middle;
                }
                else
                {
                    dir = BeatDir.up;
                }
            }
            else
            {
                if (Mathf.Abs(horizontal) >= Mathf.Abs(vertical))
                {
                    dir = BeatDir.middle;
                }
                else
                {
                    dir = BeatDir.down;
                }
            }
        }
    }
}

public enum BeatDir
{
    up,middle,down,upM,downM
}
