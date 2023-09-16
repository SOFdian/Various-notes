using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    //ui组件
    //public GameObject Notes;
    //public GameObject NotesText;
    public GameObject coin;
    public GameObject Notes;
    public float coinHeight;

    private bool canOpen;
    private bool IsOpened;

    private Animator animator;
    private string textContent;

    private GameObject enemy;//敌人死亡后才可以打开

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        IsOpened = false;
        Notes.SetActive(false);
        //enemy = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isDead)
        //{
        //    if (/*enemy.GetComponent<FlyingBehaviour>().health <= 0
        //    ||*/enemy.GetComponent<Enemy>().Health <= 0)
        //    {
        //        isDead = true;
        //    }
        //}
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canOpen && !IsOpened)
            {
                animator.SetTrigger("Opening");
                IsOpened = true;
                Invoke("Opening", 0.2f);
                Notes.SetActive(false);
                //Instantiate(coin, transform.position+new Vector3(0,1f,0), Quaternion.identity);
                //Debug.Log("Player 打开宝箱");
            }
        }
    }

    private void Opening()
    {
        Instantiate(coin, transform.position + new Vector3(0, coinHeight, 0), Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            canOpen = true;
            if (!IsOpened)
            {
                Notes.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = false;
            Notes.SetActive(false);
        }
    }

    private void EnemyIsDead()
    {
        
    }

}
