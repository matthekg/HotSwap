using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("HeroControls | Player1")]
    public string horizontalControl = "Horizontal";
    public string verticalControl = "Vertical";
    public string shootControl = "Fire1";
    public bool rawInputOn = true;

    [Header("HeroControls | Player2")]
    public string horizontalControlP2 = "HorizontalP2";
    public string verticalControlP2 = "VerticalP2";

    [Header("BossControls")]
    public string Pattern1Input = "Pattern1";
    public string Pattern2Input = "Pattern2";
    public string clockwise = "Clockwise";


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
