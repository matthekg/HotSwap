using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : Bolt.EntityBehaviour<IPracticeState>
{



    public bool isPlayer1 = false;

    [Header("Movement Stats")]
    [SerializeField] float speed = 5f;
    [SerializeField] float dodgeSpeed = 0f;
    [SerializeField] float dodgeDistance = 0f;
    [SerializeField] int dodgeIFrames = 0;


    private Rigidbody2D rb;

    [SerializeField] string horizontalControl;
    [SerializeField] string verticalControl;
    [SerializeField] string shootControl;
    private bool rawInputOn = true;
    private float xInput = 0;
    private float yInput = 0;

    GameObject gm = null;
    GameManager gmScript = null;


    public override void Attached(){
        state.SetTransforms(state.PracticeTransform,transform);
  //  }
  //  void Awake()
  //  {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();
        if (isPlayer1) {
            horizontalControl = gmScript.horizontalControl;
            verticalControl = gmScript.verticalControl;
        }
        else {
            horizontalControl = gmScript.horizontalControlP2;
            verticalControl = gmScript.verticalControlP2;
        }
        shootControl = gmScript.shootControl;
        rawInputOn = gmScript.rawInputOn;

    }

    public override void SimulateOwner()
    {
        if (gmScript.paused)
            return;

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
