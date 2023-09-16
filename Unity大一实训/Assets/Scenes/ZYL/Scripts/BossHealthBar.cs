using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Text maxHp;
    public Text currentHP;
    public Image healthSlider;
    private void Update()
    {
        maxHp.text = "1000";
        currentHP.text = enemy.Health.ToString();
        healthSlider.fillAmount = enemy.Health / 1000.0f;
    }
}
