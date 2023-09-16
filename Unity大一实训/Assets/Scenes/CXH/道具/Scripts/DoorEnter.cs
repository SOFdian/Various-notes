using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnter : MonoBehaviour
{
    //public Transform
    //;
    public int level;

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
                SceneManager.LoadScene(level);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Enter");
            isDoor = true;
            //playerTransform.position = backDoor.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
           /* && collision.GetType().ToString() == "UnityEngine.BoxCollider2D"*/)
        {
            //Debug.Log("Player Back");
            isDoor = false;
        }
    }

}
