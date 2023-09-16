using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public GameObject player;
    public Text Number;

    /*private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }*/
    public void Update()
    {
        Number.text = player.GetComponent<Player>().CoinNumber.ToString();
    }
}
