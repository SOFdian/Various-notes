using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject coin;
    public GameObject Notes;
    public float coinHeight;
    public bool isDead;

    public bool canOpen;
    public bool IsOpened;

    private Animator animator;
    private string textContent;
    private GameObject boss;
    private GameObject enemy1;
    private GameObject enemy2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        IsOpened = false;
        canOpen = false;
        isDead = false;

        Notes.SetActive(false);
        boss = GameObject.FindGameObjectWithTag("Boss");
        enemy1 = GameObject.FindGameObjectWithTag("Enemy");
        //enemy2 = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (boss.GetComponent<Enemy>().Health <= 0
                || enemy1.GetComponent<GoblinBehaviour>().health <= 0
                /*||enemy2.GetComponent<GoblinBehaviour>().health<=0*/)
            {
                isDead = true;
            }
        }
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
    //打开宝箱金币出现
    private void Opening()
    {
        Instantiate(coin, transform.position + new Vector3(0, coinHeight, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
}
