﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("HeroControls | Player1")]
    public string horizontalControl = "Horizontal";
    public string verticalControl = "Vertical";
    public string shootControl = "Fire1";
    public bool rawInputOn = true;

    public string currentShootControl;

    [Header("HeroControls | Player2")]
    public string horizontalControlP2 = "HorizontalP2";
    public string verticalControlP2 = "VerticalP2";
    public string shootControlP2 = "Fire1P2";


    [Header("BossControls")]
    public string Pattern1Input = "Pattern1";
    public string Pattern2Input = "Pattern2";
    public string clockwise = "Clockwise";

    [Header("BossControls | Player2")]
    public string Pattern1InputP2 = "Pattern1P2";
    public string Pattern2InputP2 = "Pattern2P2";
    public string clockwiseP2 = "ClockwiseP2";


    [Header("Game State")]
    /* bool to represent who is the current hero. True:P1::False:P2 */
    public bool currentHero = true;
    public bool currentBoss = false;
    public Color currentHeroColor, currentBossColor, player1Color, player2Color, winnerColor;


    GameObject player1 = null;
    GameObject player2 = null;
    GameObject boss = null;
    GameObject winScreen = null;

    public bool paused;

    void Start()
    {
        player1 = GameObject.Find("Hero 1");
        player2 = GameObject.Find("Hero 2");
        boss = GameObject.Find("Boss");
        winScreen = GameObject.Find("WinScreen");

        winScreen.SetActive(false);

        player1Color = player1.GetComponent<SpriteRenderer>().color;
        player2Color = player2.GetComponent<SpriteRenderer>().color;

        if (currentHero)
        {
            currentBossColor = player2Color;
            currentHeroColor = player1Color;
        }
        else
        {
            currentBossColor = player1Color;
            currentHeroColor = player2Color;
        }
        currentShootControl = shootControl;
        boss.GetComponent<SpriteRenderer>().color = currentBossColor;

        // Disable the hero that isnt the current one
        if (currentHero ^ player1.GetComponent<HeroMovement>().isPlayer1)
            player1.gameObject.SetActive(false);
        if (currentHero ^ player2.GetComponent<HeroMovement>().isPlayer1)
            player2.gameObject.SetActive(false);

    }

    public void EndGame()
    {
        Time.timeScale = 0;

        if (player1.GetComponent<HeroHealth>().currentHearts == 0)
            winnerColor = player2Color;
        else if (player2.GetComponent<HeroHealth>().currentHearts == 0)
            winnerColor = player1Color;

        winScreen.SetActive(true);
        Text winText = GameObject.Find("WinText").GetComponent<Text>();
        winText.color = winnerColor;
    }
    public void Swap()
    {
        Debug.Log("Swap!");
        ClearScreen();
        currentHero = currentHero ? false : true;
        currentBoss = !currentHero;
        SwapColors();

        boss.GetComponent<BossHealth>().ResetHealth();
        // Swap the Hero prefabs
        player1.gameObject.SetActive( !(currentHero ^ player1.GetComponent<HeroMovement>().isPlayer1) );
        player2.gameObject.SetActive( !(currentHero ^ player2.GetComponent<HeroMovement>().isPlayer1) );

        if (currentHero)
            currentShootControl = shootControl;
        else
            currentShootControl = shootControlP2;

        // Swap the Boss controls
        boss.GetComponent<BossMovement>().UpdateState();
        GameObject.Find("Spawner").GetComponent<BulletSpawner>().SwapControls();
    }

    public void ClearScreen()
    {
        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("Projectile");

        foreach( GameObject bullet in bullets )
        {
            Destroy(bullet);
        }
    }

    public void SwapColors()
    {
        if (currentHero)
        {
            currentBossColor = player2Color;
            currentHeroColor = player1Color;
        }
        else
        {
            currentBossColor = player1Color;
            currentHeroColor = player2Color;
        }
        boss.GetComponent<SpriteRenderer>().color = currentBossColor;

    }
}
