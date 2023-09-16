using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public int hp;//血量，被攻击后爆炸
    public float destroyTime;//爆炸后多久被销毁
    public float hitTime;//攻击时间
    public Animator animator;
    public int damage;
    public AudioSource audiosrc;
    public AudioClip audioClip;

    public Transform explosionRange;//获取爆炸范围

    // Start is called before the first frame update
    void Start()
    {
        // animation = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        audiosrc = GetComponent<AudioSource>();
        //explosionRange = GetComponent<PolygonCollider2D>();
        explosionRange = transform.Find("ExplosionRange");
        explosionRange.GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //血量为零爆炸
        if (hp <= 0)
        {
            //audiosrc.Play();
            animator.SetTrigger("Explosion");//播放动画
            explosionRange.GetComponent<PolygonCollider2D>().enabled = true;
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            //audio.isReadyToPlay(true);
            //Invoke("GenExplosionRange", hitTime);
            Invoke("Death", destroyTime);
        }

    }





    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "HitBox")
        {
            hp -= damage;
        }
           
    }
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("HitBox"))
    //        hp -= damage;
    //}

    private void Death()
    {
        //gameObject.GetComponent<>
        explosionRange.GetComponent<PolygonCollider2D>().enabled = false;
        //Destroy(explosionRange);
        Destroy(gameObject);
        
    }

    private void GenExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }
  
}

