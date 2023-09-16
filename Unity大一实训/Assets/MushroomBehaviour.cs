using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MushroomBehaviour : MonoBehaviour
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
    public bool canAttack = false;
    public bool isAttack;
    public bool canMove = true;
    private float currentTime = 2;//攻击频率为两秒一次
    private float invokeTime;//间隔时间，实时更新
    public int damage = 20;//敌人每次攻击的伤害


    [Header("受击与死亡")]
    public bool isHitted = false;
    public bool isDead = false;

    [Header("碰撞检测")]
    public Transform GobHitbox;
    GobHitbox hitbox;
    private Animator MushAnimator;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        poco2d = GetComponent<PolygonCollider2D>();
        hitbox = GetComponent<GobHitbox>();
        MushAnimator = GetComponent<Animator>();
        invokeTime = currentTime;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    //寻路插件要用到的两个函数
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {

        //Debug.Log(p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(path);
        if (canMove)
        {//自动寻路的脚本
            if (path == null)
            {
                return;
            }


            if (path.vectorPath == null || currentWaypoint >= path.vectorPath.Count)//注意这里
            {
                //Debug.Log(8);
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            //Debug.Log(path.vectorPath);
            //Debug.Log(path.vectorPath[currentWaypoint]);
            //Debug.Log(rb.position);
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
                enemyGFX.localScale = new Vector3(7.88f, 7.88f, 7.88f);
            }
            else if (force.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(-7.88f, 7.88f, 7.88f);
            }//转向还要再调，先做攻击动画
        }

        //攻击时停止移动
        //if (canAttack)
        //{
        //    this.GetComponent<AIPath>().canMove = false;
        //}
        //else
        //{
        //    this.GetComponent<AIPath>().canMove = true;
        //}

        Animation();

        if (isDead)
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
            this.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<CapsuleCollider2D>().isTrigger = false;//滑铲出去之后将isTrigger设为false
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
            Debug.Log("和玩家碰上了");
            //this.GetComponent<CircleCollider2D>().isTrigger = false;
            invokeTime += Time.deltaTime;
            //Debug.Log(invokeTime);
            if (invokeTime >= currentTime)
            {
                canAttack = true;
                //Debug.Log("下面是canAttack值");
                //Debug.Log(canAttack);
                //Debug.Log("上面是canAttack值");
                //扣血效果
                Player player = collision.gameObject.GetComponent<Player>();//获取玩家的脚本
                player.healthValue -= damage;

                //上面的语句执行完一次之后，间隔时间invokeTime计时归零
                invokeTime = 0;
            }
            else
            {
                //什么也不做
            }

        }
        else
        {
            //什么也不做
        }

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
            isHitted = true;
            health -= 200;
        }

        //判定死亡条件
        if (health <= 0)
        {
            Debug.Log("寄");
            isDead = true;
        }
    }

    void Animation()
    {
        //攻击动画
        if (canAttack)
        {
            MushAnimator.SetBool("isAttack", true);
            MushAnimator.SetBool("canMove", false);
        }
        else
        {
            MushAnimator.SetBool("isAttack", false);
            MushAnimator.SetBool("canMove", true);
        }

        //受击动画
        if (isHitted)
        {
            MushAnimator.SetBool("isHitted", true);
            MushAnimator.SetBool("isAttack", false);
            MushAnimator.SetBool("canMove", false);
        }
        else
        {
            //什么也不做
        }
        //死亡动画
        if (isDead)
        {
            MushAnimator.SetBool("isHitted", true);
            MushAnimator.SetBool("isDead", true);
            MushAnimator.SetBool("isAttack", false);
            MushAnimator.SetBool("canMove", false);
        }
        else
        {
            //什么也不做
        }
    }

    void doDestroy()
    {
        Debug.Log("销毁敌人对象");
        Destroy(this.gameObject, 1);
    }
}
/*蘑菇怪也存在一个问题：
 另外蘑菇怪的脚为什么会陷到地面里面去*/