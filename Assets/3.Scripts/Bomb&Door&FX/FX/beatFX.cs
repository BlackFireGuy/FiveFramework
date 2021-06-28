using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatFX : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<Animator>().SetTrigger("enable");

        StartCoroutine(PushObj());
    }
    /// <summary>
    /// 因为击打特效结构是脚本挂在子特效物体上，所以回收时需要回收父物体
    /// 在制作新的特效时只需要注意在特效上挂一个父物体即可。
    /// </summary>
    /// <returns></returns>
    private IEnumerator PushObj()
    {
        yield return new WaitForSeconds(1f);
        //PoolMgr.GetInstance().PushObj(PathCfg.PATH_FX + this.name, this.transform.parent.gameObject);
        if (this.gameObject != null)
        {
            PoolMgr.GetInstance().PushObj(this.transform.parent.name, this.transform.parent.gameObject);
        }
        
    }


}
