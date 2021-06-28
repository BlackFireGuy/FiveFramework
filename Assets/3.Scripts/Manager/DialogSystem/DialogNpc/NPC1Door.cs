using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Door : DialogButton
{

    Door door;

    private void Start()
    {
        door = this.GetComponent<Door>();
    }

    public override void Show()
    {
        //
        StartCoroutine(door.WaitForAnimationPlayOver(1f));
        Debug.Log("进入门");
    }
}
