using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D bo2d;
    private bool collapse;
    public float sleepingTime;
    public float reviveTime;

    // Start is called before the first frame update
    void Start()
    {
        bo2d = GetComponent<BoxCollider2D>();

    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.contacts[0].normal.y == -1)
        {
            Debug.Log("Player on Platform");
            collapse = true;
        }
        else
        {
            collapse = false;
            return;
        }
        //if (other.gameObject.CompareTag("Player")
        //    && other.GetType().ToString() == "UnityEngine.Collision2D")
        //{
        //    //Debug.Log("Player on Platform");
        //    collapse = true;
        //    //Invoke("Death", 1f);
        //    //Debug.Log(other.GetType().ToString());
        //    if(other.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        //        Debug.Log("Player on Platform");
        //}
    }


    private void Sleeping()
    {
        bo2d.enabled = false;
        gameObject.SetActive(false);
    }
    
    private void Revive()
    {
        collapse = false;
        bo2d.enabled = true;
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (collapse)
        {
            Invoke("Sleeping", sleepingTime);
            Invoke("Revive", reviveTime);
        }   
    }
}
