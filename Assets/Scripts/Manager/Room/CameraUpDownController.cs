using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpDownController : MonoBehaviour
{
    public static CameraUpDownController instance;

    public float speed;

    public Transform target;

    private void Awake()
    {
            instance = this;
    }

    private void Update()
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,target.position.y,transform.position.z),speed*Time.deltaTime);
    }

    public void ChangeTarget(Transform newtarget)
    {
        target = newtarget;
    }
}
