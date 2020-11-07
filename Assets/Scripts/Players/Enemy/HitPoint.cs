using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    [Header("伤害值")]
    public int damage;
    public bool bombAvilable;
    int dir;
    public bool isPlayer;

    bool isAttacked;
    public GameObject damageCanvas;
    //碰撞效果在这里写
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("进入攻击触发器内");
        if (!isPlayer)//挂载在敌人身上
        {
            if (transform.position.x > other.transform.position.x)
                dir = -1;
            else
                dir = 1;
            if (other.CompareTag("Player"))
            {
                other.GetComponent<IDamageable>().GetHit(damage);

                
            }
            if (other.CompareTag("Bomb") && bombAvilable)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 10, ForceMode2D.Impulse);
            }
        }
        else//挂载在玩家身上
        {
            //Debug.Log("进入玩家的hitpoint攻击触发器内");
            if (transform.position.x > other.transform.position.x)
                dir = -1;
            else
                dir = 1;
            if (other.CompareTag("Enemy"))
            {
                if(isAttacked == false)
                {
                    //避免重复伤害
                    other.GetComponent<IDamageable>().GetHit(PlayerInfoManager.instance.info.attack);
                    if (damageCanvas != null)
                    {
                        DamageNum dg = Instantiate(damageCanvas, other.transform.position, Quaternion.identity).GetComponent<DamageNum>();
                        dg.ShowUIDamage(PlayerInfoManager.instance.info.attack);
                    }
                    else
                    {
                        Debug.Log("damageCanvas为空");
                    }
                    //震动
                    Cinemachine.CinemachineCollisionImpulseSource MyInpulse = other.GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
                    if (MyInpulse != null)
                    {
                        MyInpulse.GenerateImpulse();
                    }


                    isAttacked = true;
                    StartCoroutine(IsAttackCo());
                    //同时击退
                    //this.transform.parent.GetComponent<Enemy>().hitDialog.SetActive()
                    Vector3 p2 = (other.transform.position - transform.position).normalized;

                    other.transform.position = new Vector2(
                        other.transform.position.x + p2.x,
                        other.transform.position.y + p2.y);
                }
                
                
            }
            if (other.CompareTag("Door"))
            {
                other.GetComponent<Door>().OpenDoor();
            }
        }
        
    }
    IEnumerator IsAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacked = false;
    }
}
