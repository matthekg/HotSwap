using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] int maxHearts = 3;
    [SerializeField] int currentHearts = 0;

    void Awake()
    {
        currentHearts = maxHearts;
    }

    void Update()
    {       
    }

    public void DamageSelf()
    {
        --currentHearts;

        if (currentHearts == 0)
            Debug.Log("HERO DEAD. GAME OVER");

        Debug.Log("HERO HIT. SWAP SIDES");
    }
}
