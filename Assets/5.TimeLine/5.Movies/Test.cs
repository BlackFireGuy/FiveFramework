using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Test : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public Transform position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            playableDirector.Play();
        }
        position.position = transform.position;
        transform.position = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") * Time.deltaTime, transform.position.y, transform.position.z);

    }
}
