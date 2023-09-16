using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    //public float waitTime;

    private BoxCollider2D boxcol;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)
            ||Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.W) || Input.GetButtonUp("Jump"))
        {
            //waitTime = 0.5f;
            boxcol.enabled = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            effector.rotationalOffset = 90f;
            boxcol.enabled = false;
            //if (waitTime <= 0)
            //{
            //    boxcol.enabled = false;
            //    //player.gameObject.GetComponent<Player>().
            //    effector.rotationalOffset = 90f;
            //    //waitTime = 0.5f;
            //}
            //else
            //{
            //    waitTime -= Time.deltaTime;
            //    boxcol.enabled = true;
            //}
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)||Input.GetButton("Jump"))
        {
            effector.rotationalOffset = -90f;
            boxcol.enabled = false;
        }
    }
}
