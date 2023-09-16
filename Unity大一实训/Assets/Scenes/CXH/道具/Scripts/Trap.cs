using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        other.GetComponent<Player>().healthValue -= damage;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player")
    //        /*&&other.GetType().ToString()=="UnityEngine.BoxCollider2D"*/)
    //    {
    //        //other.GetComponent<Player>().InvincibleEnemy = true;无敌状态
    //        //other.GetComponent<Player>().OnCollisionStay2D();
    //        other.GetComponent<Player>().healthValue -= damage;
    //        //other.GetComponent<Player>().GetComponent<BoxCollider2D>().enabled = false;


    //    }
    //}
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag=="Player")
        {
           // Debug.Log(other.GetType().ToString());
            other.gameObject.GetComponent<Player>().healthValue -= damage;
        }
    }
}
