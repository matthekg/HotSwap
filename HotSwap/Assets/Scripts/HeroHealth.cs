using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] int maxHearts = 3;
    public int currentHearts = 3;
    public Image hearts = null;
    public Image hearts2 = null;
    private int heartHeight, heartWidth = 32; //pixels
    public bool left, right;
    public Color heartColor;

    GameObject gm = null;
    GameManager gmScript = null;

    void Awake()
    {
        currentHearts = maxHearts;
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();
        heartColor = this.GetComponent<SpriteRenderer>().color;
        updateUI();
    }

    void Update()
    {

    }

    public void DamageSelf()
    {
        --currentHearts;

        if (currentHearts == 0)
            gmScript.EndGame();

        updateUI();
        gmScript.Swap();
    }

    public void updateUI()
    {
        hearts = GameObject.Find("Hero1 Hearts").GetComponent<Image>();
        hearts2 = GameObject.Find("Hero2 Hearts").GetComponent<Image>();
        hearts.color = heartColor;
        if( left )
            hearts.rectTransform.SetInsetAndSizeFromParentEdge(
            RectTransform.Edge.Left, 42, currentHearts * heartWidth);
        if( right )
            hearts2.rectTransform.SetInsetAndSizeFromParentEdge(
            RectTransform.Edge.Right, 42, currentHearts * heartWidth);
    }

    public void ToggleGrayscale()
    {

    }
}
