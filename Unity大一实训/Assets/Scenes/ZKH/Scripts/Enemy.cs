using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("组件")]
    public GameObject npc;
    public LayerMask Player;
    Transform Target;
    public Transform BossHitBox;
    public Animator Animator;
    public GameObject blood;
    public GameObject Float;
    public AudioSource Dead;
    [Header("状态量")]
    public bool AttackCd=true;
    public bool MagicCd=true;
    public int Health = 5000;
    public bool canMove;
    public float xSpeed;
    public float ySpeed;
    public bool canAttack;
    public bool canMagic;
    public bool isAttacking;
    public bool isMagicing;
    public bool isHurting;
    public bool isDeath;
    Vector3 myTarget;
    [Header("碰撞检测")]
    public float attackLength;
    public float attackLength2;
    public float attackHigh;
    public float attackHigh2;
    public float biuLength;
    public float biuLength2;
    public float biuHigh;
    public float biuHigh2;
    // Start is called before the first frame update
    void Start()
    {
        npc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        xSpeed = this.GetComponent<AIPath>().velocity.x;
        ySpeed = this.GetComponent<AIPath>().velocity.y;
        canMove = this.GetComponent<AIPath>().canMove;
        Target = this.GetComponent<AIPath>().target.transform;
        checkPosition();
        State();
        Animation();
    }
    
    void Animation()
    {
        if (canAttack)
        {
            Animator.SetBool("isAttack",true);
        } else
        {
            Animator.SetBool("isAttack",false);
        }
        if (canMagic)
        {
            Animator.SetBool("isMagic", true);
        }
        else
        {
            Animator.SetBool("isMagic", false);
        }
        if (this.GetComponent<AIPath>().reachedEndOfPath)
        {
            
            Animator.SetBool("canMove", false);
        } else
        {
            Animator.SetBool("canMove", true);
        }
        if (isHurting)
        {
            Animator.SetBool("isHurt", true);
        } else
        {
            Animator.SetBool("isHurt", false);
        }
        if (isDeath)
        {
            Animator.SetBool("isDeath",true );
        }
    }

    void State()
    {
        //����
        //changeface���ڹ�����ɺ��ж�һ�������Ƿ�����ǰ����Ϊ����һֱ���ܹ�������������ʱcanmoveΪfalse
        if (canMove)
        {
            if(Target.transform.position.x > this.transform.position.x)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            } else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
        }
        //ֹͣ�ƶ�
        if (canAttack || canMagic)
        {
            this.GetComponent<AIPath>().canMove=false;
        } else
        {
            this.GetComponent<AIPath>().canMove=true;
        }
        if (Health <= 0)
        {
            isDeath=true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        
    }
    void beginAttack()
    {
        BossHitBox.GetComponent<PolygonCollider2D>().enabled = true;
    }
    void finishAttack()
    {
        BossHitBox.GetComponent<PolygonCollider2D>().enabled = false;
        AttackCd = false;
        Invoke("RefreshAttack", 2);
        canAttack = false;
    }
    void beginMagic()
    {
        myTarget = Target.transform.position;
    }
    void finishMagic()
    {
        MagicCd = false;
        Invoke("RefreshMagic", 5);
        canMagic = false;
        transform.localPosition = new Vector3(myTarget.x, transform.localPosition.y, transform.localPosition.z);
    }

    void finishHurt()
    {
        isHurting = false;
    }

    //���ƹ���Ƶ��
    void RefreshAttack()
    {
        AttackCd = true;
    }
    void RefreshMagic()
    {
        MagicCd = true;
    }

    void RefreshTime()
    {
        Time.timeScale = 1f;
    }
    void checkPosition()
    {
        bool a=Raycast(new Vector2(0, attackHigh), Vector2.left, attackLength, Player);//���
        bool b=Raycast(new Vector2(0, attackHigh), Vector2.right, attackLength, Player);//�Ҳ�
        bool c=Raycast(new Vector2(0, attackHigh2), Vector2.left, attackLength2, Player);//���
        bool d=Raycast(new Vector2(0, attackHigh2), Vector2.right, attackLength2, Player);//�Ҳ�

        bool aa=Raycast(new Vector2(0, biuHigh), Vector2.left, biuLength, Player);//ħ�����
        bool bb=Raycast(new Vector2(0, biuHigh), Vector2.right, biuLength, Player);//ħ���Ҳ�
        bool cc=Raycast(new Vector2(0, biuHigh2), Vector2.left, biuLength2, Player);//ħ�����,����
        bool dd=Raycast(new Vector2(0, biuHigh2), Vector2.right, biuLength2, Player);//ħ���Ҳ�
        //�����ж�
        
        if ((a && !c)||(b && !d))
        {
            if (AttackCd)
            {
                canAttack = true;
            }
        }
        //ħ���ж�
        if ((cc && !aa) || (dd && !bb))
        {
            if (MagicCd)
            {
                canMagic = true;
            }
        }
    }
    //�����������������Կ�boss�������������
    void changeFace()
    {
        if (Target.transform.position.x > this.transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(1); 
    //    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().InvincibleEnemy)
    //    {
    //        this.GetComponent<CircleCollider2D>().enabled = false;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log(2);
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        this.GetComponent<CircleCollider2D>().enabled = true;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            this.GetComponent<CircleCollider2D>().isTrigger=true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.GetComponent<CircleCollider2D>().isTrigger = false;
    }
    //�߼�Ŀǰ�����Ǻ�������ΪʲôҪ����ô�����ײ����
    //����������Ϊ��ʵ�ֻ��д�������
    //��������������޸����ﻬ�е�������ײ����ʱ���ᱻ����������
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    //���������˺���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitBox")
        {
            Health -= 100;
            if (!canAttack)//���壬ishurtingֻ�����������˶���
            {
                isHurting = true;
            }
            Instantiate(blood,transform.position, Quaternion.identity);
            GameObject gb = Instantiate(Float, transform.position, Quaternion.identity); 
            gb.transform.GetChild(0).GetComponent<TextMesh>().text = (-100).ToString();
            Time.timeScale = 0.5f;
            Invoke("RefreshTime", 0.2f);
        }
        else if (collision.gameObject.tag == "DashHitBox")
        {
            Health -= 2000;
            if (!canAttack)
            {
                isHurting = true;
            }
            Instantiate(blood, transform.position, Quaternion.identity);
            GameObject gb = Instantiate(Float, transform.position, Quaternion.identity);
            gb.transform.GetChild(0).GetComponent<TextMesh>().text = (-2000).ToString();
            Time.timeScale = 0.5f;
            Invoke("RefreshTime", 0.2f);
        } 
    }

    void doDestroy()
    {
        npc.SetActive(true);
        Destroy(this.gameObject,0.2f);
    }

    void AudioPlay()
    {
        Dead.Play();
    }
}
