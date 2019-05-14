using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletLogic : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float currentSpeed = 0f;
    private Vector2 direction;
    private bool pauseBeforeMoving = false;
    [SerializeField] float pauseTime = 0.25f;
    private bool move = true;
    private bool turning = false;

    private Rigidbody2D rb;

    void Awake()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0,0);
    }

    void Update()
    {
        if( pauseBeforeMoving )
        {
            StartCoroutine(PauseMovement());
        }

        rb.velocity = direction * currentSpeed;

        if( turning )
        {
          //  rb.AddForce(Vector2.down);
        }
    }

    public void SetDirection(Vector2 newDir)
    {
        direction = newDir;
    }

    public void SetPause( bool choice )
    {
        pauseBeforeMoving = choice;
    }

    public void SetSpeed( float newSpeed )
    {
        speed = newSpeed;
    }

    public void SetTurn( bool choice )
    {
        turning = choice;
    }

    IEnumerator PauseMovement()
    {
        currentSpeed = 0;
        yield return new WaitForSeconds(pauseTime);
        pauseBeforeMoving = false;
        currentSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boss") )
        {return;}


        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HeroHealth>().DamageSelf();
        }
        Destroy(gameObject);
    }
}
