using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRange : MonoBehaviour
{
    public int damage;
   
    //private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //销毁 ？待定？
    void Death()
    {
        Destroy(gameObject);
    }

    //    private void OnCollisionEnter2D(Collision2D other)
    //    {
    //        if (other.gameObject.CompareTag("Player"))
    //        {
    //            //调用Player的被攻击函数
    //            Debug.Log("Player Attacked");
    //            other.GetComponent<Player>().healthValue -= damage;
    //            //other.GetComponent<Player>().d
    //        }
    //        if (other.tag == "Enemy")
    //        {
    //            //调用enemy被攻击的函数
    //        }
    //        if (other.tag == "Bucket")
    //        {
    //            other.GetComponent<Bucket>().hp--;
    //        }
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"
            &&other.GetType().ToString()== "UnityEngine.BoxCollider2D")
        {
            //调用Player的被攻击函数
            Debug.Log("Player Attacked");
            other.GetComponent<Player>().healthValue -= damage;
            //other.GetComponent<Player>().d
        }
        if (other.tag == "Enemy")
        {
            //调用enemy被攻击的函数
        }
        if (other.tag == "Bucket")
        {
            Debug.Log("Bucket Attacked");
            other.GetComponent<Bucket>().hp--;
        }
    }
}
