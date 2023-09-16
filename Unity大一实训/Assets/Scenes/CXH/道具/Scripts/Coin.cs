using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public AudioSource audiosrc;
    public AudioClip audioClip;
    private GameObject player;
    //public CoinUI coinUI; //UI的

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiosrc = GetComponent<AudioSource>();
        //coinUI = GetComponent<CoinUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"
            && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")//根据tag修改！！！
        {
            //Foodaudio.Play();
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            collision.gameObject.GetComponent<Player>().CoinNumber++;
            DataSave.Instance.Update(player);

            //coinUI.AddCoin();  //金币槽数量加1

            Destroy(gameObject);
            //Invoke("Death", 0.8f);

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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
