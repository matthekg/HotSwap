using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] int maxHearts = 3;
    [SerializeField] int currentHearts = 0;

    GameObject gm = null;
    GameManager gmScript = null;

    void Awake()
    {
        currentHearts = maxHearts;
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();

    }

    void Update()
    {       
    }

    public void DamageSelf()
    {
        --currentHearts;

        if (currentHearts == 0)
            Debug.Log("HERO DEAD. GAME OVER");

        gmScript.Swap();
    }
}
