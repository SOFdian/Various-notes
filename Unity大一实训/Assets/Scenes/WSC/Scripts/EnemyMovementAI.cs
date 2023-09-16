using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovementAI : MonoBehaviour
{
    [Header("���")]
    public LayerMask Player;
    Transform Target;
    public Transform BossHitBox;
    public Animator Animator;
    [Header("״̬��")]
    public bool AttackCd = true;
    public bool MagicCd = true;
    public int Health = 1000;
    public bool canMove;
    public float xSpeed;
    public float ySpeed;
    public bool canAttack;
    public bool canMagic;
    public bool isAttacking;
    public bool isMagicing;
    Vector3 myTarget;
    [Header("��ײ���")]
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            this.GetComponent<CircleCollider2D>().isTrigger = true;
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
        }
        else if (collision.gameObject.tag == "DashHitBox")
        {
            Health -= 200;
        }
    }
}
