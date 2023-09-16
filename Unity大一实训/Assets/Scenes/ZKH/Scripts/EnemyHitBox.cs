using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
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

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log(1);
    //        collision.gameObject.GetComponent<Player>().healthValue -= damage;
            
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&& !collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            collision.gameObject.GetComponent<Player>().healthValue -= damage;
        }
    }
}
