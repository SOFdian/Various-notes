using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform othDoor;
    public float height;

    private bool isDoor;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoor)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                //Application.LoadLevel(2);
                playerTransform.position = othDoor.position+new Vector3(0,height,0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDoor = true;
            //playerTransform.position = backDoor.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
           /* && collision.GetType().ToString() == "UnityEngine.BoxCollider2D"*/)
        {
            isDoor = false;
        }
    }
}
