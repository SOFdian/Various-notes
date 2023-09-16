using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    
    public AudioSource getAudio;
    public AudioClip getClip;

    // Start is called before the first frame update
    void Awake()
    {

        getAudio = GetComponent<AudioSource>();
        
    }

    //判定碰撞类型＋后续反应
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider== )
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"
            &&collision.GetType().ToString()=="UnityEngine.BoxCollider2D")//根据tag修改！！！
        {
            //getAudio.Play();
            AudioSource.PlayClipAtPoint(getClip, transform.position);
            //Invoke("Death", 0.35f);
            Destroy(gameObject);

            //gameObject.SetActive(false);
        }
        else
        {
            //考虑是否还有其他可移动
            return;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        
    }

}
