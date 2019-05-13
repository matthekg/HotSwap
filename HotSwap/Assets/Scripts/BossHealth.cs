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

    void Awake()
    {
        healthSlider = GameObject.Find("BossHealthSlider").GetComponent<Slider>();
        currentHealth = maxHealth;
        InvokeRepeating("DamageSelf", 0.0f, 1.0f);
    }

    void Update()
    {      
    }

    public void DamageSelf()
    {
        currentHealth -= fixedDamage;
        UpdateHealthBar();

        if( currentHealth == 0 )
        {
            Debug.Log("BOSS DEAD. SWAP SIDES");
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }
}
