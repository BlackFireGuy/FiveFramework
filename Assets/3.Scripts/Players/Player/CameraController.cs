using UnityEngine;
/// <summary>
/// 通过鼠标控制和AI控制镜头的旋转和远近高低
/// </summary>
public class CameraController : MonoBehaviour
{
    //角色的transform
    Transform target;

    //平滑缓冲的程度
    int dampTrace = 4;//摄像机跟随的移动速度
    //偏移量
    [Header("偏移量")]
    public Vector3 offset = new Vector3(0,0,-10);

    public float speed = 1;

    private void Update()
    {
        if(target == null)
            target = FindObjectOfType<PlayerController>().transform;
        else
        {
            float t = Util.Distance2D(target.position, transform.position);
            speed = (1 > t ?1:t);

            speed = dampTrace * speed;

        }

    }

    private void LateUpdate()
    {
        if (target == null) { return; }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);

    }
}
