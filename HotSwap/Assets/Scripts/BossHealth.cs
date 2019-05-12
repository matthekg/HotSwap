using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 60;
    [SerializeField] int currentHealth = 0;

    private int fixedDamage = 1;

    void Awake()
    {
        currentHealth = maxHealth;
        InvokeRepeating("DamageSelf", 0.0f, 1.0f);
    }

    void Update()
    {
        
    }

    public void DamageSelf()
    {
        currentHealth -= fixedDamage;
    }
}
