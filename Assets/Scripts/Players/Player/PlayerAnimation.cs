using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public bool isUpDown;
    private Animator ani;
    private Rigidbody2D rig;
    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpDown)
        {
            ani.SetFloat("speed", Mathf.Abs(rig.velocity.x));
            ani.SetFloat("velocityY", Mathf.Abs(rig.velocity.y));
            ani.SetBool("jump", controller.isJump);
            ani.SetBool("ground", controller.isGround);
        }
        else
        {
            ani.SetFloat("speed", Mathf.Abs(rig.velocity.magnitude));
        }

    }
}
