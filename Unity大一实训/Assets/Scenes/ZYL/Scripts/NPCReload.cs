using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NPCReload : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        if (player.GetComponent<Transform>().position.y <= -40)
        {
            SceneManager.LoadScene("NPC");
        }
    }
}
