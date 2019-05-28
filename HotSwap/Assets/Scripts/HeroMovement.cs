using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public bool isPlayer2 = false;

    [Header("Movement Stats")]
    [SerializeField] float speed = 5f;
    [SerializeField] float dodgeSpeed = 0f;
    [SerializeField] float dodgeDistance = 0f;
    [SerializeField] int dodgeIFrames = 0;


    private Rigidbody2D rb;

    private string horizontalControl;
    private string verticalControl;
    private string horizontalControlP2;
    private string verticalControlP2;
    private string shootControl;
    private bool rawInputOn = false;
    private float xInput = 0;
    private float yInput = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject gm = GameObject.Find("GameManager");
        GameManager gmScript = gm.GetComponent<GameManager>();
        if (isPlayer2)
        {
            horizontalControlP2 = gmScript.horizontalControlP2;
            verticalControlP2 = gmScript.verticalControlP2;
        }
        else
        {
            horizontalControl = gmScript.horizontalControl;
            verticalControl = gmScript.verticalControl;
        }
        shootControl = gmScript.shootControl;
        rawInputOn = gmScript.rawInputOn;
    }

    void Update()
    {
//        if ( Input.GetAxisRaw(dodgeControl) > 0 )
//            Dodge();
    }

    void FixedUpdate()
    {
        if (rawInputOn) {
            xInput = Input.GetAxisRaw(horizontalControl);
            yInput = Input.GetAxisRaw(verticalControl);
        }
        else {
            xInput = Input.GetAxis(horizontalControl);
            yInput = Input.GetAxis(verticalControl);
        }

        rb.velocity = new Vector2(xInput * speed, yInput * speed);
    }

    private void Dodge( /*direction?*/ )
    {

    }
}
