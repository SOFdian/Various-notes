using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingBehaviour : MonoBehaviour
{
    [Header("��������")]
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

    [Header("����״̬")]
    public bool canAttack=false;
    public bool isAttack;
    public bool canMove=true;
    public float currentTime = 3;//����Ƶ��Ϊһ��һ��
    private float invokeTime;//
    public int damage = 5;

    [Header("�ܻ�������")]
    public bool isHitted = false;
    public bool isDead = false;

    [Header("��ײ���")]
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
        {//�Զ�Ѱ·�Ľű�
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
            //ת��
            if (force.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(7.346599f, 7.346599f, 7.346599f);
            }
            else if (force.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(-7.346599f, 7.346599f, 7.346599f);
            }//ת��Ҫ�ٵ���������������
        }

        //����ʱֹͣ�ƶ�
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

    //���߼��
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 postion = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(postion + offset, rayDirection, length, layer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(postion + offset, rayDirection * length, color);
        return hit;
    }

    //��ײ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            this.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<CircleCollider2D>().isTrigger = false;//������ȥ֮��isTrigger��Ϊfalse
        if (collision.gameObject.tag == "Player")
        {
            canAttack = false;
        }//�뿪��ײ���ٲ��Ź�������
    }

    //����������Ϊ��ʵ�ֻ��д�������
    //��������������޸����ﻬ�е�������ײ����ʱ���ᱻ���������⣬ͬʱ���ù�������

    private void OnTriggerStay2D(Collider2D collision)//����Ҳ�������ù�������
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
                //��ѪЧ��
                Player player = collision.gameObject.GetComponent<Player>();//��ȡ��ҵĽű�
                player.healthValue -= damage;//

                //�������ִ����һ�κ�invokeTime����
                invokeTime = 0;
            }
               
        }
        //if (collision.tag == "Player")
        //{
        //    canAttack = true;
        //}
    }

    //���������˺���,ͬʱ�ж��ܻ���������������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�����ܻ�������");
        if (collision.gameObject.tag == "HitBox")
        {
            Debug.Log("������");
            isHitted = true;    
            health -= 100;
        }
        else if (collision.gameObject.tag == "DashHitBox")
        {
            Debug.Log("�ػ�������");
            isHitted=true;
            health -= 200;
        }

        //�ж���������
        if (health<=0)
        {
            Debug.Log("��");
            isDead = true;
        }
    }

    //���Ŷ���
    void Animation()
    {
        //��������
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

        //�ܻ�����
        if(isHitted)
        {
            FlyingAnimator.SetBool("isHitted", true);
            FlyingAnimator.SetBool("isAttack",false );
            FlyingAnimator.SetBool("canMove",false);
        }
        else
        {
            //ʲôҲ����
        }
        //��������
        if(isDead)
        {
            FlyingAnimator.SetBool("isHitted", true);
            FlyingAnimator.SetBool("isDead",true );
            FlyingAnimator.SetBool("isAttack", false);
            FlyingAnimator.SetBool("canMove",false);
        }
        else
        {
            //ʲôҲ����
        }

    }

    void doDestroy()
    {
        Debug.Log("���ٵ��˶���");
        Destroy(this.gameObject,1);
    }
}
