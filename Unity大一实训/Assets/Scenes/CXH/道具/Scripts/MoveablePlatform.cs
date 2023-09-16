using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] movePos;//移动起点和终点


    private int i;//定位上述两点取值
    private Transform playerDefTransform;//获取player初始层级关系

    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed *Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime < 0.0f)//cd时间已过
            {
                if (i == 0)
                    i = 1;
                else
                    i = 0;
                waitTime = 0.5f;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.gameObject.tag=="Player"
    //        /*&& other.GetType().ToString() == "UnityEngine.BoxCollider2D"*/)
    //    {
    //        Debug.Log("Player on platform");
    //        other.gameObject.transform.parent = this.gameObject.transform;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player on platform");
            other.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = playerDefTransform;
        }   
    }
}
