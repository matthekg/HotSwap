using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private Vector2 direction;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void SetDirection( Vector2 newDir )
    {
        direction = newDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossHealth>().DamageSelf();
        }
        Destroy( gameObject );
    }
}
