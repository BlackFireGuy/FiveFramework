using System;
using UnityEngine;
/// <summary>
/// 控制行走行动，跳跃，和攻击的中枢转移控制
/// </summary>
[RequireComponent(typeof(ArmourHit))]
[RequireComponent(typeof(BodyHit))]
public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator ani;
    private ArmourHit ahit;
    private BodyHit bhit;
    private FixedJoystick joystick;
    private HealthBarSlider healthBar;
    

    public float swimSpeed = 1;
    public float speed;
    public float jumpForce;
    [Header("是否为俯视角")]
    public bool isUpDown;//是否是俯视视角移动

    [Header("玩家状态")]
    //public float health;
    public bool isDead;

    [Header("状态标识")]
    public bool canJump;
    int JumpNum;
    [Header("动作特效")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("攻击设置")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;
    public Transform ShootParent;

    

    [Header("技能和装备")]
    public Inventory inventorySkill;


    [Header("死亡时需要禁止的脚本& 物体")]
    public  Behaviour[] lists;
    public GameObject[] ListO;
    bool hasDesDead;//是否进行过死亡之后的处理
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        ahit = GetComponent<ArmourHit>();
        bhit = GetComponent<BodyHit>();
        joystick = FindObjectOfType<FixedJoystick>();
        healthBar = GetComponentInChildren<HealthBarSlider>();

        GameManager.instance.IsPlayer(this);//告诉游戏管理这我就是玩家
        //初始化玩家血量
        /*health = GameManager.instance.LoadHealth();
        healthBar.maxHp = health;*/
        //检测是否装备了技能,.是则装备
        for (int i = 0; i < inventorySkill.itemList.Count; i++)
        {
            if (inventorySkill.itemList[i].isEquip)//这个是装备的
            {
                PlayerInfoManager.instance.infoItemData = inventorySkill.itemList[i];
                //PoolMgr.GetInstance().GetObj(SkillTable.skillpath[inventorySkill.itemList[i].skillId], SkillInit);

                GameObject obj = ResMgr.GetInstance().Load<GameObject>(SkillTable.skillpath[inventorySkill.itemList[i].skillId]);
                SkillInit(obj);
                GameManager.instance.isSkillShoot = true;
            }
        }

    }

    //死亡不进行控制
    void Update()
    {
        ani.SetBool("dead", isDead);
        
        if (GameManager.instance.gameMode != GameManager.GameMode.Normal) return;
        if (isDead&& !hasDesDead) {
            for(int i = 0; i < lists.Length; i++)
            {
                lists[i].enabled = false;
            }
            for(int i = 0; i < ListO.Length; i++)
            {
                Destroy(ListO[i]);
            }
            PlayerPrefs.SetInt("DeadNumPerMatch", PlayerPrefs.GetInt("DeadNumPerMatch")+1);
            PlayerInfoManager.instance.info.pastSucess ++ ;
            PlayerInfoManager.instance.info.currentHp = PlayerInfoManager.instance.info.maxHp;
            GameSaveManager.instance.SaveGame();
            hasDesDead = true;
            return;
        } 
        if (bhit.beAttacking) return;
        if (joystick == null)
            joystick = FindObjectOfType<FixedJoystick>();

        healthBar.maxHp = PlayerInfoManager.instance.info.maxHp;
        healthBar.hp = PlayerInfoManager.instance.info.currentHp;

    }
    // 死亡或者失控则不进行控制
    //固定时长执行，跟物理有关的
    private void FixedUpdate()
    {
        if (GameManager.instance.gameMode != GameManager.GameMode.Normal) return;
        //是否死亡
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //是否失控
        if (bhit.beAttacking||BeatManager.instance.isAttacking) return;

        MoveController();

        //编辑器下调试
        if (bhit.isGround||bhit.isAgainstwall)
        {
            JumpNum = 0;
            canJump = true;
        }
        MovePC();
    }


    //--------------------------------------------------------------------------
    void MovePC()
    {
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
        {
            horizontalInput = 1;
            BeatManager.instance.playerForwardDir = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            healthBar.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (horizontalInput < 0)
        {
            horizontalInput = -1;
            BeatManager.instance.playerForwardDir = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
            //血条不动
            healthBar.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        


        if (Input.GetKeyDown("space"))
        {
            
            if (canJump)
            {
                JumpNum = 0;
                ani.SetTrigger("jump");
            }
            else
            {
                if(JumpNum < 2)
                {
                    ani.SetTrigger("jump");
                    
                }
            }
        }

        if (horizontalInput < 0.0001f && horizontalInput > -0.0001f) return;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

    }
    //玩家移动动作
    void MoveController()
    {

        //ani.SetFloat("test", Input.GetAxis("Horizontal"));
        //操作杆
        if (joystick != null && BeatManager.instance.isAttacking == false)
        {
            float horizontalInput = joystick.Horizontal;
            if (horizontalInput > 0)
            {
                horizontalInput = 1;
                BeatManager.instance.playerForwardDir = 1;
                transform.eulerAngles = new Vector3(0, 0, 0);
                healthBar.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (horizontalInput < 0)
            {
                horizontalInput = -1;
                BeatManager.instance.playerForwardDir = -1;
                transform.eulerAngles = new Vector3(0, 180, 0);
                //血条不动
                healthBar.transform.localEulerAngles = new Vector3(0, 180, 0);
            }

            if (horizontalInput > 0)
                GameManager.instance.horizontal = horizontalInput;
            else
                GameManager.instance.horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontalInput < 0.0001f&&horizontalInput>-0.0001f) return;
            GameManager.instance.horizontal = horizontalInput;
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
    }
    //玩家跳跃动作
    public void JumpController()
    {
        if (GameManager.instance.gameMode != GameManager.GameMode.Normal) return;
        if (joystick != null)
        {
            if (bhit.isGround||bhit.isAgainstwall) canJump = true;
            
            if (canJump)
            {
                JumpNum = 0;
                ani.SetTrigger("jump");
            }
            else
            {
                if (JumpNum < 2)
                {
                    ani.SetTrigger("jump");

                }
            }
        }
    }
    // 玩家攻击动作(除受伤外）
    public void Attack()
    {
        if (GameManager.instance.gameMode != GameManager.GameMode.Normal) return;
        if (isDead) return;

        if (DialogManager.instance.isButtonActive)
        {

            return;
        }

        if (Time.time > nextAttack)
        {
            //这里可以用缓存池和
            ahit.Attack();
            nextAttack = Time.time + attackRate;
            //后续设计到技能上
            /*Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
            nextAttack = Time.time + attackRate;*/
        }
    }
    /// <summary>
    /// 玩家冲刺
    /// </summary>
    internal void RushController()
    {
        if (GameManager.instance.gameMode != GameManager.GameMode.Normal) return;
        if (isDead) return;
        //被攻击不允许冲刺
        if (bhit.isAttacked) return;
        //如果速度不低于speed,就不允许冲刺
        if (rb.velocity.x > speed|| rb.velocity.x < -speed) return;
        ani.SetTrigger("Rush");
        rb.velocity = new Vector2(rb.velocity.x, 0); ;

        rb.AddForce(new Vector2((float)BeatManager.instance.playerForwardDir,0)*800 );
        
    }
    //--------------------------------------------------------------------------
    // 搭载装备的实现
    private void SkillInit(GameObject arg0)
    {
        arg0.transform.SetParent(ShootParent);
        arg0.transform.localPosition = Vector3.zero;
        arg0.transform.localScale = Vector3.one;
    }
    //受伤的playercontroller内实现
    public void GetHit(float damage)
    {
        if (!ani.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))
        {
            MusicMgr.GetInstance().PlaySound("hurt");

            float temp = (damage - PlayerInfoManager.instance.info.hitResistance) < 0 ? 1 : (damage - PlayerInfoManager.instance.info.hitResistance);
            PlayerInfoManager.instance.info.currentHp -= temp;
            //health -= damage;
            if (PlayerInfoManager.instance.info.currentHp < 1)
            {
                PlayerInfoManager.instance.info.currentHp = 0;
                isDead = true;
            }
            ani.SetTrigger("hit");

            //healthBar.hp = health;
        }
    }


    //--------------------------------------------------------------------------
    // animation event
    public void LandFX()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void Jump()
    {

        JumpNum++;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
        jumpFX.SetActive(true);
        jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);

    }
    


}
