using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 60;
    [SerializeField] int currentHealth = 0;

    private Slider healthSlider;

    private int fixedDamage = 1;

    GameObject gm = null;
    GameManager gmScript = null;

    void Awake()
    {
        healthSlider = GameObject.Find("BossHealthSlider").GetComponent<Slider>();
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();

        ResetHealth();
        InvokeRepeating("DamageSelf", 0.0f, 1.0f);
    }

    void Update()
    {      
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void DamageSelf()
    {
        currentHealth -= fixedDamage;
        UpdateHealthBar();

        if( currentHealth == 0 )
        {
            gmScript.Swap();
        }
    }

    private void UpdateHealthBar()
    {
        if( gmScript.currentBoss )
            healthSlider.SetDirection(Slider.Direction.LeftToRight, false);
        else
            healthSlider.SetDirection(Slider.Direction.RightToLeft, false);

        healthSlider.value = currentHealth;
        GameObject.Find("BossHealthFill").GetComponent<Image>().color = gmScript.currentBossColor;
        GameObject.Find("BossHealthBackground").GetComponent<Image>().color = Color.Lerp(gmScript.currentHeroColor, gmScript.currentBossColor, healthSlider.value / 60);
    }
}
