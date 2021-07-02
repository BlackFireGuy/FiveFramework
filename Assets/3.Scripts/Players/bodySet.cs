using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodySet :MonoBehaviour
{
    Rigidbody2D rigidbody2;
    Collider2D collider2;
    bool isAddForce = false;
    private void Start()
    {
        rigidbody2 = this.GetComponent<Rigidbody2D>();
        collider2 = this.GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (GameManager.instance.gameOver)
        {
            rigidbody2.mass = 1;
            //StartCoroutine(SetSpeed());
            if(isAddForce == false)
            {
                rigidbody2.AddForce(new Vector2(300 * GameManager.instance.horizontal, 0f));
                isAddForce = true;
            }
            
            rigidbody2.gravityScale = MyRandom.GetRandomNumber(1,4);
            collider2.isTrigger = false;
        }
    }




    IEnumerator SetSpeed()
    {
        //rigidbody2.velocity = new Vector2(Random.value, Random.value);
        yield return new WaitForSeconds(0.1f);
        //rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, rigidbody2.velocity.y);
    }
}
