using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingBehaviour : MonoBehaviour
{
    [Header("基础属性")]
    public LayerMask Player;
    public Transform target;
    public Transform enemyGFX;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float health = 500;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    PolygonCollider2D poco2d;
    Transform Target;

    [Header("攻击状态")]
    public bool canAttack=false;
    public bool isAttack;
    public bool canMove=true;
    public float currentTime = 3;//攻击频率为一秒一次
    private float invokeTime;//
    public int damage = 5;

    [Header("受击与死亡")]
    public bool isHitted = false;
    public bool isDead = false;

    [Header("碰撞检测")]
    public Transform FlyingHitBox;
    FlyingHitbox hitbox;
    private Animator FlyingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        poco2d = GetComponent<PolygonCollider2D>();
        hitbox = GetComponent<FlyingHitbox>();
        FlyingAnimator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    //private void Update()
    //{
    //    Debug.Log("jinruDebug");
    //}

    // Update is called once per frame
    void Update()
    {
        
        if (canMove)
        {//自动寻路的脚本
            if (path == null)
                return;

            if (path.vectorPath == null || currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            //转向
            if (force.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(7.346599f, 7.346599f, 7.346599f);
            }
            else if (force.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(-7.346599f, 7.346599f, 7.346599f);
            }//转向还要再调，先做攻击动画
        }

        //攻击时停止移动
        if (canAttack)
        {
            this.GetComponent<AIPath>().canMove = false;
        }
        else
        {
            this.GetComponent<AIPath>().canMove = true;
        }

        Animation();
        
        if(isDead)
        {
            doDestroy();
        }
    }


    void beginAttack()
    {
        hitbox.GetComponent<PolygonCollider2D>().enabled = true;
    }

    void finishAttack()
    {
        hitbox.GetComponent<PolygonCollider2D>().enabled = false;
        Invoke("RefreshAttack", 2);
        canAttack = false;
    }

    //射线检测
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 postion = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(postion + offset, rayDirection, length, layer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(postion + offset, rayDirection * length, color);
        return hit;
    }

    //碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            this.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<CircleCollider2D>().isTrigger = false;//滑铲出去之后将isTrigger设为false
        if (collision.gameObject.tag == "Player")
        {
            canAttack = false;
        }//离开碰撞后不再播放攻击动画
    }

    //上面两个是为了实现滑行穿过敌人
    //下面这个是用来修复人物滑行到怪物碰撞体内时不会被弹出的问题，同时设置攻击条件

    private void OnTriggerStay2D(Collider2D collision)//这里也可以设置攻击条件
    {
        string test01 = collision.gameObject.name;
        Player test02 = collision.gameObject.GetComponent<Player>();
        Debug.Log(test01);
        Debug.Log(test02);
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            Debug.Log("jinru");
            //this.GetComponent<CircleCollider2D>().isTrigger = false;
            invokeTime += Time.deltaTime;
            if (invokeTime>=currentTime)
            {
                canAttack = true;
                //扣血效果
                Player player = collision.gameObject.GetComponent<Player>();//获取玩家的脚本
                player.healthValue -= damage;//

                //上面语句执行完一次后，invokeTime归零
                invokeTime = 0;
            }
               
        }
        //if (collision.tag == "Player")
        //{
        //    canAttack = true;
        //}
    }

    //下面是受伤函数,同时判定受击条件及死亡条件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("进入受击函数了");
        if (collision.gameObject.tag == "HitBox")
        {
            Debug.Log("打中了");
            isHitted = true;    
            health -= 100;
        }
        else if (collision.gameObject.tag == "DashHitBox")
        {
            Debug.Log("重击打中了");
            isHitted=true;
            health -= 200;
        }

        //判定死亡条件
        if (health<=0)
        {
            Debug.Log("寄");
            isDead = true;
        }
    }

    //播放动画
    void Animation()
    {
        //攻击动画
        if (canAttack)
        {
            FlyingAnimator.SetBool("isAttack", true);
            FlyingAnimator.SetBool("canMove",false);
        }
        else
        {
            FlyingAnimator.SetBool("isAttack",false);
            FlyingAnimator.SetBool("canMove",true);
        }

        //受击动画
        if(isHitted)
        {
            FlyingAnimator.SetBool("isHitted", true);
            FlyingAnimator.SetBool("isAttack",false );
            FlyingAnimator.SetBool("canMove",false);
        }
        else
        {
            //什么也不做
        }
        //死亡动画
        if(isDead)
        {
            FlyingAnimator.SetBool("isHitted", true);
            FlyingAnimator.SetBool("isDead",true );
            FlyingAnimator.SetBool("isAttack", false);
            FlyingAnimator.SetBool("canMove",false);
        }
        else
        {
            //什么也不做
        }

    }

    void doDestroy()
    {
        Debug.Log("销毁敌人对象");
        Destroy(this.gameObject,1);
    }
}
