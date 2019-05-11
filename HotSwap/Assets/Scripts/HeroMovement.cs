using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] float speed = 5f;
    [SerializeField] float dodgeSpeed = 0f;
    [SerializeField] float dodgeDistance = 0f;
    [SerializeField] int dodgeIFrames = 0;


    private Rigidbody2D rb;

    [Header("Controls")]
    public string horizontalControl = "Horizontal";
    public string verticalControl = "Vertical";
    public string shootControl = "Fire1";
    public string dodgeControl = "Jump";
    [SerializeField] private bool rawInputOn = false;
    private float xInput = 0;
    private float yInput = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ( Input.GetAxisRaw(dodgeControl) > 0 )
            Dodge();
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
