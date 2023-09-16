using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushHitbox : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<Player>().InvincibleEnemy)
        {
            collision.gameObject.GetComponent<Player>().healthValue -= damage;
        }
    }
}
