using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonBehaviour : MonoBehaviour
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
    public bool canAttack = false;
    public bool isAttack;
    public bool canMove = true;
    private float currentTime = 2;//����Ƶ��Ϊ����һ��
    private float invokeTime;//���ʱ�䣬ʵʱ����
    public int damage = 20;//�����˺�

    [Header("�ܻ�������")]
    public bool isHitted = false;
    public bool isDead = false;

    [Header("��ײ���")]
    public Transform SkeHitbox;
    SkeHitbox hitbox;
    private Animator SkeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        poco2d = GetComponent<PolygonCollider2D>();
        hitbox = GetComponent<SkeHitbox>();
        SkeAnimator = GetComponent<Animator>();
        invokeTime = currentTime;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    //Ѱ·���Ҫ�õ�����������
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
        {//�Զ�Ѱ·�Ľű�
            if (path == null)
            {
                return;
            }


            if (path.vectorPath == null || currentWaypoint >= path.vectorPath.Count)//ע������
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
            //ת��
            if (force.x >= 0.01f)
            {
                //enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (force.x <= -0.01f)
            {
                //enemyGFX.localScale = new Vector3(1f, 1f, 1f);
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
            //this.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //this.GetComponent<CircleCollider2D>().isTrigger = false;
    }

    //����������Ϊ��ʵ�ֻ��д�������
    //��������������޸����ﻬ�е�������ײ����ʱ���ᱻ���������⣬ͬʱ���ù�������

    private void OnTriggerStay2D(Collider2D collision)//����Ҳ�������ù�������
    {
        //string test01 = collision.gameObject.name;
        //Player test02 = collision.gameObject.GetComponent<Player>();
        //Debug.Log(test01);
        //Debug.Log(test02);
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            Debug.Log("�����������");
            //this.GetComponent<CircleCollider2D>().isTrigger = false;
            invokeTime += Time.deltaTime;
            //Debug.Log(invokeTime);
            if (invokeTime >= currentTime)
            {
                canAttack = true;
                Debug.Log("������canAttackֵ");
                Debug.Log(canAttack);
                Debug.Log("������canAttackֵ");
                //��ѪЧ��
                Player player = collision.gameObject.GetComponent<Player>();//��ȡ��ҵĽű�
                player.healthValue -= damage;//�ܲ�������Ϊdamage?

                //��������ִ����һ��֮�󣬼��ʱ��invokeTime��ʱ����
                invokeTime = 0;
            }
            else
            {
                canAttack = false;
            }

        }
        else
        {
            canAttack = false;
        }

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
            isHitted = true;
            health -= 200;
        }

        //�ж���������
        if (health <= 0)
        {
            Debug.Log("��");
            isDead = true;
        }
    }

    void Animation()
    {
        //��������
        if (canAttack)
        {
            SkeAnimator.SetBool("isAttack", true);
            SkeAnimator.SetBool("canMove", false);
        }
        else
        {
            SkeAnimator.SetBool("isAttack", false);
            SkeAnimator.SetBool("canMove", true);
        }

        //�ܻ�����
        if (isHitted)
        {
            SkeAnimator.SetBool("isHitted", true);
            SkeAnimator.SetBool("isAttack", false);
            SkeAnimator.SetBool("canMove", false);
        }
        else
        {
            //ʲôҲ����
        }
        //��������
        if (isDead)
        {
            SkeAnimator.SetBool("isHitted", true);
            SkeAnimator.SetBool("isDead", true);
            SkeAnimator.SetBool("isAttack", false);
            SkeAnimator.SetBool("canMove", false);
        }
        else
        {
            //ʲôҲ����
        }
    }

    void doDestroy()
    {
        Debug.Log("���ٵ��˶���");
        Destroy(this.gameObject, 1);
    }

}
