using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("一些组件")]
    public LayerMask groundLayer;//检测地面layer
    public Transform HitBox;
    public Transform DashHitBox;
    public BoxCollider2D Body;
    public CapsuleCollider2D Head;
    public Rigidbody2D playerRb;
    public Animator playerAnimator;
    public GameObject Blood;
    public GameObject Float;
    public AudioSource AttackSound;
    public AudioSource DashAttackSound;
    public AudioSource Hurt;
    //状态判断
    [Header("状态")]
    public int index;
    public GameObject data;
    public bool InvincibleEnemy = false;//无敌状态(对怪物而言）
    public bool canClimb = false;//能否攀爬
    public bool canAttack = true;//能否攻击
    public bool canCrouch = true;//能否下蹲
    public bool canStand = true;//能否移动                           敌人的hitbox是trigger，敌人的身体不是
    public bool canSlide = true;//能否下滑
    public bool canJump = true;//能否跳
    public bool canDash = true;//能否冲刺
    public bool isRunning = false;//是否正在跑步
    public bool isJumping = false;//是否正在跳跃
    public bool isJumptoFall = false;//跳跃与下落的切换动画
    public bool isFalling = false;//是否正在下落
    public bool isAttacking = false;//是否正在攻击
    public bool isClimbing = false;//是否正在攀爬
    public bool isCrouching = false;//是否正在趴下
    public bool isOnGround = true;//是否站在地面上
    public bool isSliding = false;//是否正在下滑
    public bool isDashing = false;//是否正在冲刺
    public bool isWall = false;//是否贴墙
    public bool isWallSliding = false;//是否正在贴墙下滑
    public bool isHurting = false;//是否受伤
    public bool isDashAttack = false;//冲刺攻击
    public bool isHanging = false;
    public bool isIn = false;
    public int ChangeDashCd;
    public int CoinNumber;
    public float ySpeed;
    public float xSpeed;
    public float xHitSpeed;
    public float yHitSpeed;
    //某些人物属性
    [Header("属性")]
    private float halfWide;
    private float halfHeigh;
    public int jumpTimes = 2;//跳跃次数
    public int jumpNum;
    public float speed;//左右移动的速度
    public float jumpForce;//跳跃高度
    public float climbForce;//攀爬速度
    public int healthValue=100;//生命值  //王：我把int改成了float  还是决定改回来
    public int normalHit;//平a伤害
    public int dashHit;//冲刺攻击伤害
    public bool death = false;//死亡判定
    bool dashCd = true;
    public float dashSpeed;
    public float slidSpeed;
    //碰撞检测
    [Header("碰撞检测")]
    public float footOffset;//脚边的距离
    public float headDistance;//头往上的距离
    public float groundDistance;//脚往下的距离
    public float aheadDistance;//前方的距离
    public float topAheadDistance;//头上那两个射线到前方的距离
    public float hang;
    public float hang2;
    //监听
    [Header("监听")]
    public int myhealthValue = 100;//用于监听生命值  //王：我把int改成float  还是决定改回来
    public bool myisIn = false;
    public bool myisOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        data = GameObject.FindGameObjectWithTag("Data");
        HitBox = transform.Find("HitBox");
        DashHitBox = transform.Find("DashHitBox");
        if (index == 3 || index == 2)
        {
            
            this.healthValue = DataSave.Instance.PlayerHp;
            this.CoinNumber = DataSave.Instance.PlayerCoinNum;
            this.ChangeDashCd = DataSave.Instance.PlayerCD;
            this.jumpNum = DataSave.Instance.PlayerJumpTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ySpeed = playerRb.velocity.y;
        xSpeed = playerRb.velocity.x;
        PhysicsCheck();
        checkData();
        State();
        //    //滑行时不接收键盘输入，冲刺时只接收攻击输入，攻击时固定x轴
        if ((isAttacking || isDashAttack) && !isDashing)
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        } else if (isSliding||isHurting||death||isDashing||isHanging)
        {
            //什么也不做
            //由于冲刺时禁用了movement，无法调用攻击键，所以单独写出来
            if (isDashing)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    isDashAttack = true;
                }
            }
            if (isHanging)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
                }
            }
        } else
        {
            Movement();
        }
        if (death)
        {
            playerRb.velocity = new Vector2(0, 0);
        }
        Animation();
    }

    void Movement()
    {
        float xMove = Input.GetAxis("Horizontal");
        //左右移动
        if (xMove != 0)
        {
            playerRb.velocity = new Vector2(xMove * speed, playerRb.velocity.y);
        } else
        {
            //没有按键输入时别左右动了
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
        }
        //跳跃
        if (Input.GetButtonDown("Jump")&&jumpTimes>0&&canJump)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            isClimbing = false;
            //已经忘了下面的代码是干嘛的了
            //if (isDashAttack || isDashing)//修正空中冲刺攻击时会接上普通攻击的问题
            //{
            //    canAttack = false;
            //}
            //else
            //{
            //    canAttack = true;
            //}
            jumpTimes--;
        } 
        //攀爬
        if (isClimbing)
        {
            //禁止x轴方向的移动
            playerRb.velocity = new Vector2(0, climbForce * Input.GetAxis("Vertical"));
        }
        //脸的转向
        if (!isClimbing)
        {
            changeFace();
        }
        if (isCrouching)
        {
            //长按蹲下，加上点按
            if (Input.GetButtonDown("Horizontal"))
            {
                isSliding = true;
            } 
            //退出滑行状态：1.从平台上掉落  2.执行完一次滑行后，判断是否还在缝里，是的话继续滑行，否则退出
        }
        //冲刺
        if (Input.GetKeyDown(KeyCode.LeftShift)&&dashCd)
        {
            isDashing = true;
        }
    }

    void Animation()
    {   //跑步动画
        if (isRunning)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        //攻击动画
        if (isAttacking)
        {
            playerAnimator.SetBool("isAttacking", true);
        }
        else
        {
            playerAnimator.SetBool("isAttacking", false);
        }
        //跳跃动画
        if (isJumping)
        {
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("isJumptoFall", false);
            playerAnimator.SetBool("isFalling", false);
        }
        else if (isJumptoFall)
        {
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isJumptoFall", true);
            playerAnimator.SetBool("isFalling", false);
        }
        else if (isFalling)
        {
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isJumptoFall", false);
            playerAnimator.SetBool("isFalling", true);
        } else
        {
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("isJumptoFall", false);
            playerAnimator.SetBool("isFalling", false);
        }
        //攀爬动画
        if (isClimbing)
        {
            playerAnimator.SetBool("isClimbing", true);
        }
        else
        {
            playerAnimator.SetBool("isClimbing", false);
        }
        if (isCrouching)
        {
            playerAnimator.SetBool("isCrouching",true);
        } else
        {
            playerAnimator.SetBool("isCrouching", false);
        }
        if (isSliding)
        {
            playerAnimator.SetBool("isSliding", true);
        } else
        {
            playerAnimator.SetBool("isSliding", false);
        }
        if (isWallSliding)
        {
            playerAnimator.SetBool("isWallSliding", true);
        } else
        {
            playerAnimator.SetBool("isWallSliding",false);
        }
        if (isHurting)
        {
            playerAnimator.SetBool("isHurting", true);
        } else
        {
            playerAnimator.SetBool("isHurting",false );
        }
        if (death)
        {
            playerAnimator.SetBool("Death", true);
        }
        if (isDashing)
        {
            playerAnimator.SetBool("isDashing",true );
        } else
        {
            playerAnimator.SetBool("isDashing", false);
        }
        if (isDashAttack)
        {
            playerAnimator.SetBool("isDashAttack",true);
        } else
        {
            playerAnimator.SetBool("isDashAttack", false);
        }
        if (isHanging)
        {
            playerAnimator.SetBool("isHanging",true);
        } else
        {
            playerAnimator.SetBool("isHanging", false);
        }
    }

    void State()
    {
        //判断是否在跑步
        if ((Mathf.Abs(playerRb.velocity.x) > 2&&isOnGround)||(!isCrouching&&isWall&&isOnGround&&Mathf.Abs(Input.GetAxis("Horizontal"))!=0))//设置跑步的阈值
        {
            isRunning = true;
            isAttacking = false;
        }
        else
        {
            isRunning = false;
        }
        //攀爬时
        //判断跳跃次数
        if (isClimbing)
        {
            jumpTimes = jumpNum;
            if (playerRb.velocity.y > 0)//box可能卡住
            {
                this.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        } else
        {
            this.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        //y轴速度为0时才重置跳跃次数，是为了处理在地上跳跃时有时候不会正确减少跳跃次数的问题
        if (isOnGround)
        {
            Invoke("jumpRefresh", 0.01f);
        }
        //判断跳跃
        //有时候会卡在isJumptoFall
            if (playerRb.velocity.y > 2)
            {
                isJumping = true; 
                isJumptoFall = false;
                isFalling = false;

            }
            else if (Mathf.Abs(playerRb.velocity.y) <= 2 && Mathf.Abs(playerRb.velocity.y) >= 1)
            {
                isJumping = false;
                isJumptoFall = true;
                isFalling = false;
            }
            else if (playerRb.velocity.y < -2)
            {
                isJumping = false;
                isJumptoFall = false;
                isFalling = true;
            }
            else if (Mathf.Abs(playerRb.velocity.y) < 1)
            {
                isJumping = false;
                isJumptoFall = false;
                isFalling = false;
            }
        //判断攻击,按下
        if (Input.GetButtonDown("Fire1") && canAttack&&!isDashAttack&&!isSliding)//防止冲刺中连按攻击键会自动攻击
        {
            isAttacking = true;
        }
        if (isHurting)
        {
            isAttacking = false;
        }
        //判断是否正在攀爬（判断能否攀爬在碰撞函数内）
        if (canClimb && Input.GetAxisRaw("Vertical") > 0)
        {
            isClimbing = true;
            //跳跃时退出攀爬状态
        }
        //判断能否蹲下
        if (isOnGround&&!isAttacking&&!isRunning&&!isClimbing)
        {
            canCrouch = true;
        } else
        {
            canCrouch = false;
        }
        //正在蹲下时
        if (isCrouching)
        {
            canSlide = true;
            canJump = false;
        } else
        {
            canSlide= false;
            canJump = true;
        }
        //当可以蹲并且发出了蹲下的指令时，蹲下；否则的话如果可以站起来，就站起来
        if (canCrouch&&Input.GetAxis("Vertical") < 0)
        {
            isCrouching = true;
        } else 
        {
            isCrouching = false;
        }
        //攀爬和蹲下时
        //判断能否攻击
        if (isClimbing || isCrouching||isDashing||isDashAttack||isHanging)
        {
            canAttack = false;
        } else
        {
            canAttack = true;
        }
        //滑行时
        //无敌判定
        if (isSliding)
        {
            playerRb.velocity = new Vector2( slidSpeed*transform.localScale.x, playerRb.velocity.y);
            //if (!canStand)
            //{
            //    isSliding = true;
            //}
            //如果可以站立，就动画执行完站立
        } 
        //头是否trigger
        //蹲下或者滑行时
        if (isCrouching || isSliding)
        {
            Head.isTrigger = true;
        } else
        {
            Head.isTrigger = false;
        }
        //扒墙：脚离地，身贴墙，有按键/下落状态
        //修正：不要下落状态，因为下落状态进入扒墙时就不是下落了，下一帧就不会进入扒墙
        //二次修正：下面的代码使扒墙时一定在下落
        if (isWallSliding)
        {
            isFalling = true;
        }
        if (!isOnGround&&isWall&&isFalling&&!isHanging)
        {
            isWallSliding = true;
        } else
        {
            isWallSliding = false;
        }
        //扒墙时
        if (isWallSliding)
        {
            jumpTimes = jumpNum-1;
            playerRb.velocity = new Vector2(playerRb.velocity.x,0);
        }
        if (isHanging)
        {
            playerRb.velocity = new Vector2(0, 0);
            playerRb.GetComponent<BoxCollider2D>().enabled = false;
        } else
        {
            //为了保证这个瞬间人物已经跳上去了
            Invoke("hangRefresh", 0.2f);
        }
        //攀爬时
        //重力调节
        if(isClimbing)
        {
            playerRb.gravityScale = 0;
        } else if (isWallSliding)
        {
            playerRb.gravityScale = 3;
        }else if (isDashing)
        {
            playerRb.gravityScale = 0;
        }else if (isHanging)
        {
            playerRb.gravityScale = 0;
        }
        else
        {
            playerRb.gravityScale = 10;
        }
        //冲刺判定
        if (isDashing)
        {
            playerRb.velocity = new Vector2(slidSpeed * transform.localScale.x, 0);
        }
        //受伤时
        if (isHurting)
        {
            canClimb = false;//能否攀爬
            canAttack = false;//能否攻击
            canCrouch = false;//能否下蹲
            canStand = false;//能否移动
            canSlide = false;//能否下滑
            canJump = false;//能否跳
            isDashAttack = false;
            isDashing = false;
            playerRb.velocity = new Vector2(0,playerRb.velocity.y);
        }
        //无敌判定
        if (isSliding || isHurting)
        {
            InvincibleEnemy = true;
        }
        else
        {
            InvincibleEnemy = false;
        }
        //死亡判定
        if(healthValue <= 0)
        {
            death = true;
        }
    }
    //攻击动画执行到一定帧时hitbox出现
    void beginAttack()
    {
        HitBox.GetComponent<PolygonCollider2D>().enabled = true;
        AttackSound.Play();
    }
    void beginDashAttack()
    {
        DashHitBox.GetComponent<PolygonCollider2D>().enabled = true;
        DashAttackSound.Play();
    }
    //攻击结束，用于攻击动画事件
    void finishAttack()
    {
        isAttacking = false;
        HitBox.GetComponent<PolygonCollider2D>().enabled = false;
    }
    void finishDashAttack()
    {
        isDashAttack = false;
        DashHitBox.GetComponent<PolygonCollider2D>().enabled = false;
    }

    void finishSlid()
    {
        if (!canStand)
        {
            isSliding = true;   
        } else
        {
            isSliding = false;
        }
    }

    void finishHurting()
    {
        isHurting = false;
    }

    void finishDash()
    {
        isDashing = false;
        dashCd = false;
        Invoke("dashRefresh", ChangeDashCd);
    }

    void AudioHurtPlay()
    {
        Hurt.Play();
    }
    //碰撞体离开
    private void OnTriggerExit2D(Collider2D collision)
    {
        //判断能否攀爬
        if (collision.tag == "Climbable")
        {
            canClimb = false;
            isClimbing = false;
            playerRb.gravityScale = 10;
        }
    }
    //碰撞体进入
    private void OnTriggerEnter2D(Collider2D collision)
    {   //判断能否攀爬
        if (collision.tag == "Climbable")
        {
            canClimb = true;
        }
    }

    //碰撞体保持
    private void OnCollisionStay2D(Collision2D collision)
    {
        //碰到敌人（的hitbox）并且不无敌
        if (collision.gameObject.tag  == "Enemy" && !InvincibleEnemy)
        {
            //isHurting = true;
            float xHit = collision.contacts[0].normal.x;
            float yHit = collision.contacts[0].normal.y;
            playerRb.gravityScale = 0;
            playerRb.velocity = new Vector2(xHit*xHitSpeed, yHit*yHitSpeed);
            //if (collision.contacts[0].normal.x<0)//左边碰撞
            //{
            //    playerRb.velocity = new Vector2(-speed, speed);
            //} else if(collision.contacts[0].normal.x > 0)//右边碰撞
            //{
            //    playerRb.velocity = new Vector2(speed, speed);
            //}
        }
        if(collision.gameObject.tag == "Confiner")
        {
            healthValue = 0;
        }
    }
    //用于改变脸的朝向，专门写一个函数出来是因为爬梯子时不需要调用这个函数(在解决了人物朝向的旋转轴时即可弃用）
    void changeFace()
    {
        float faceDirection = Input.GetAxisRaw("Horizontal");
        //脸的朝向
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, transform.localScale.y, transform.localScale.z);
        }
    }

    //射线检测
    RaycastHit2D Raycast(Vector2 offset,Vector2 rayDirection,float length ,LayerMask layer)
    {
        Vector2 postion = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(postion + offset, rayDirection,length, layer);
        Color color  =hit?Color.red:Color.green;
        Debug.DrawRay(postion+offset,rayDirection*length,color);
        return hit;
    }

    //碰撞检测
    void PhysicsCheck()
    {
        halfWide = Body.size.x/2;
        halfHeigh = Body.size.y/ 2;
        RaycastHit2D leftcheck = Raycast(new Vector2(footOffset, -halfHeigh), Vector2.down, groundDistance, groundLayer);//检测左脚是否在地面上
        RaycastHit2D rightcheck = Raycast(new Vector2(-footOffset, -halfHeigh), Vector2.down, groundDistance, groundLayer);//检测右脚是否在地面上
        RaycastHit2D leftHeadcheck = Raycast(new Vector2(-topAheadDistance, 0), Vector2.down, headDistance, groundLayer);//检测左上
        RaycastHit2D rightHeadcheck = Raycast(new Vector2(topAheadDistance, 0), Vector2.down, headDistance, groundLayer);//检测右上
        RaycastHit2D leftAhead = Raycast(new Vector2(0, 0), Vector2.left, aheadDistance, groundLayer);//检测左前方
        RaycastHit2D rightAhead = Raycast(new Vector2(0, 0), Vector2.right, aheadDistance, groundLayer);//检测右前方
        RaycastHit2D leftHang = Raycast(new Vector2(0, hang), Vector2.left, aheadDistance, groundLayer);//检测左侧悬挂
        RaycastHit2D rightHang = Raycast(new Vector2(0, hang), Vector2.right, aheadDistance, groundLayer);//检测右侧悬挂
        RaycastHit2D leftHang2 = Raycast(new Vector2(0, hang2), Vector2.left, aheadDistance, groundLayer);//检测左侧悬挂
        RaycastHit2D rightHang2 = Raycast(new Vector2(0, hang2), Vector2.right, aheadDistance, groundLayer);//检测右侧悬挂
        //脚下面有物体时就算站在地上了
        if (leftcheck || rightcheck)
        {
            isOnGround = true;
        } else
        {
            isOnGround = false;
        }
        //缝内
        if((leftcheck || rightcheck) && (leftHeadcheck || rightHeadcheck))
        {
            isIn = true;
        } else
        {
            isIn = false;
        }
        //如果头上和脚下都有物体，或者不在地上就不能站立
        if(((leftcheck || rightcheck)&&(leftHeadcheck || rightHeadcheck))||!isOnGround)
        {
            canStand = false;
        } else
        {
            canStand = true;
        }
        if (leftAhead || rightAhead)
        {
            isWall = true;
        } else
        {
            isWall = false;
        }
        if (isWall && (!leftHang && leftHang2) || (!rightHang && rightHang2))
        {
            isHanging = true;
        } else
        {
            isHanging = false;
        }
    }
    //监听数据变化
    void checkData()
    {
        //生命值监听
        if (myhealthValue != healthValue)
        {
            DataSave.Instance.Update(playerRb.gameObject);

            if (myhealthValue > healthValue)
            {
                isHurting = true;
                if (!death)
                {
                    Instantiate(Blood, transform.position, Quaternion.identity);
                    GameObject gb = Instantiate(Float, transform.position, Quaternion.identity);
                    gb.transform.GetChild(0).GetComponent<TextMesh>().text = (healthValue - myhealthValue).ToString();
                }
            }
            if (!death)
            {
                myhealthValue = healthValue;
            }
        }

        //滑行监听
        //平地滑行，缝内滑行，入缝滑行，出缝滑行，下落滑行(下落滑行的判断需要对isonground进行监听）
        if (myisIn != isIn)//出入缝时
        {
            //缝内：(leftcheck || rightcheck)&&(leftHeadcheck || rightHeadcheck)
            if (myisIn && !isIn) //出缝滑行
            {
                isSliding = false;
            }
            myisIn = isIn;
        } else if (myisOnGround && !isOnGround)
        {
            isSliding = false;
        }
        //isonground监听
        if (myisOnGround != isOnGround)
        {
            myisOnGround = isOnGround;
        }
    }

    void jumpRefresh()
    {
        jumpTimes = jumpNum-1;
    }
    void hangRefresh()
    {
        //由于box大于capsule，扒住墙往上跳的时候会被box卡住
        playerRb.GetComponent<BoxCollider2D>().enabled = true;
    }

    void dashRefresh()
    {
        dashCd = true;
    }

}
