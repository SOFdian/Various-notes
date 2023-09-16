using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour
{
    public Image healthPointImage;
    public Image healthPointEffect;
    public float maxHP = 100.0f;
    public Player player;
    /*private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPointControl>();
    }*/

    private void Update()
    {
        
        healthPointImage.fillAmount = player.healthValue / maxHP;
        Debug.Log("player: "+player.healthValue);
        if (healthPointEffect.fillAmount > healthPointImage.fillAmount)
        {
            healthPointEffect.fillAmount -= 0.001f;
        }
        else
        {
            healthPointEffect.fillAmount = healthPointImage.fillAmount;
        }
    }
}
