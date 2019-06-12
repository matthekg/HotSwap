using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] int maxHearts = 3;
    public int currentHearts = 3;
    public Image hearts = null;
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
        hearts.color = heartColor;
        if( left )
            hearts.rectTransform.SetInsetAndSizeFromParentEdge(
            RectTransform.Edge.Left, 42, currentHearts * heartWidth);
        if( right )
            hearts.rectTransform.SetInsetAndSizeFromParentEdge(
            RectTransform.Edge.Right, 42, currentHearts * heartWidth);
    }

    public void ToggleGrayscale()
    {

    }
}
