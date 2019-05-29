using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public bool isPlayer1 = false;

    [Header("Movement Stats")]
    [SerializeField] float speed = 5f;

    private Rigidbody2D rb;

    [SerializeField] string horizontalControl;
    [SerializeField] string verticalControl;
    private bool rawInputOn = true;
    private float xInput = 0;
    private float yInput = 0;

    GameObject gm = null;
    GameManager gmScript = null;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();
        isPlayer1 = !gmScript.currentHero;

        if (isPlayer1)
        {
            horizontalControl = gmScript.horizontalControl;
            verticalControl = gmScript.verticalControl;
        }
        else
        {
            horizontalControl = gmScript.horizontalControlP2;
            verticalControl = gmScript.verticalControlP2;
        }
        rawInputOn = gmScript.rawInputOn;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayer1 = gmScript.currentBoss;
    }
    void FixedUpdate()
    {
        if (rawInputOn)
        {
            xInput = Input.GetAxisRaw(horizontalControl);
            yInput = Input.GetAxisRaw(verticalControl);
        }
        else
        {
            xInput = Input.GetAxis(horizontalControl);
            yInput = Input.GetAxis(verticalControl);
        }

        rb.velocity = new Vector2(xInput * speed, yInput * speed);
    }

    public void UpdateState()
    {
        isPlayer1 = isPlayer1 ? false : true;
        if (isPlayer1)
        {
            horizontalControl = gmScript.horizontalControl;
            verticalControl = gmScript.verticalControl;
        }
        else
        {
            horizontalControl = gmScript.horizontalControlP2;
            verticalControl = gmScript.verticalControlP2;
        }
    }

}
