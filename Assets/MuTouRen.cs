using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuTouRen : MonoBehaviour
{

    public GameObject mutouren;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj =  Instantiate(mutouren);
        obj.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
