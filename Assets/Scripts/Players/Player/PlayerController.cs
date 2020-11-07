using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator ani;
    private FixedJoystick joystick;
    private HealthBar healthBar;
    Vector2 Movement;

    public float speed;
    public float jumpForce;
    //public bool isMobile;//是否是移动端
    public bool isUpDown;//是否是俯视视角移动

    [Header("Player State")]
    public float health;
    public bool isDead;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("States Check")]
    public bool canJump;
    public bool isJump;
    public bool isGround;

    [Header("JumpFX")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        joystick = FindObjectOfType<FixedJoystick>();
        healthBar = FindObjectOfType<HealthBar>();
        GameManager.instance.IsPlayer(this);

        health = GameManager.instance.LoadHealth();
        
    }

    void Update()
    {
        ani.SetBool("dead", isDead);
        if (isDead)
            return;
        CheckInput();
        /*if (isGround)
            rb.gravityScale = 4;
        else
        {
            rb.gravityScale = 1;
        }*/
        if(healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }
        else
        {
            healthBar.UpdateHealth(health);
        }
        if(joystick == null)
            joystick = FindObjectOfType<FixedJoystick>();

    }
    //固定时长执行，跟物理有关的
    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //PhysicsCheck()不能放在下面，否则rb.gravityScale = 4，人物无法立刻离开检测区，则isground仍为true，则rb.gravityScale又恢复为1
        if(!isUpDown)
            PhysicsCheck();
        MoveController();
        JumpController();
        
    }

    void CheckInput()
    {
        if (isUpDown) return;//如果是俯视角，则不检测jump和按键F
        if (Input.GetButtonDown("Jump")&& isGround)
        {
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    void MoveController()
    {
        //键盘
        /*if (isUpDown)//俯视角操作
        {
            UpDownController();
            return;
        }*/
        //操作杆
        if (joystick != null)
        {
            float horizontalInput = joystick.Horizontal;
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            if (horizontalInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    void JumpController()
    {
        if (joystick != null)
        {
            if (!isUpDown)
            {
                float verticalInput = joystick.Vertical;
                if (verticalInput > 0.5f && isGround) canJump = true;
                if (canJump)
                {
                    isJump = true;
                    jumpFX.SetActive(true);
                    jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * verticalInput);
                    rb.gravityScale = 4;
                    canJump = false;
                }
            }
            else
            {
                float verticalInput = joystick.Vertical;
                rb.velocity = new Vector2(rb.velocity.x,verticalInput * speed);
            }
               
        }
    }
    public void ButtonJump()
    {
        if(isGround)
            canJump = true;
    }
    public void Attack()
    {
        if (isDead) return;
        if (Time.time > nextAttack)
        {
            if (!isUpDown)//横板2D
            {
                //这里可以用缓存池和
                ani.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
                //后续设计到技能上
                /*Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
                nextAttack = Time.time + attackRate;*/
            }
            else//俯视角
            {
                ani.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
            
        }
    }
    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//圆检测
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
            
    }

    public void LandFX()//animation event
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    public void GetHit(float damage)
    {
        if (!ani.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))
        {
            MusicMgr.GetInstance().PlaySound("hurt");

            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            ani.SetTrigger("hit");
            if(healthBar != null)
                healthBar.UpdateHealth(health);
        }
    }
}
