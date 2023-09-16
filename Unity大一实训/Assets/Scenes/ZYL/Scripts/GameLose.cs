using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLose : MonoBehaviour
{
    public Player player;
    public GameObject LoseUI;

    private void Update()
    {
        if (player.healthValue <= 0)
        {
            Invoke("Dead", 3f);
        }
    }

    public void Dead()
    {
        LoseUI.SetActive(true);
    }
}
