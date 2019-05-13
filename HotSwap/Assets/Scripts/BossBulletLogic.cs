using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletLogic : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private Vector2 direction;
    private bool pauseBeforeMoving = false;
    [SerializeField] float pauseTime = 1f;
    private bool move = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if( pauseBeforeMoving )
        {
            StartCoroutine(PauseMovement());
        }

        rb.velocity = direction * speed;
    }

    public void SetDirection(Vector2 newDir)
    {
        direction = newDir;
    }

    public void SetPause( bool choice )
    {
        pauseBeforeMoving = choice;
    }

    IEnumerator PauseMovement()
    {
        float oldSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(pauseTime);
        pauseBeforeMoving = false;
        speed = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HeroHealth>().DamageSelf();
        }
        Destroy(gameObject);
    }
}
