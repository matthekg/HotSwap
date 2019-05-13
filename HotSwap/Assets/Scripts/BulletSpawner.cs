using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Courtesy of https://answers.unity.com/questions/714835/best-way-to-spawn-prefabs-in-a-circle.html
 */

public class BulletSpawner : MonoBehaviour
{
    [Header("[1] : BulletRing")]
    [SerializeField] int numBullets = 12;
    [SerializeField] float circleSpawnRadius = 3f;
    public GameObject bulletPrefab;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SpawnBulletRing();
        }
    }

    public void SpawnBulletRing()
    {
        Debug.Log("BulletRing!");
        Vector3 center = transform.position;
        for (int i = 1; i <= numBullets; ++i)
        {
            Vector3 pos = RandomCircle(center, i, circleSpawnRadius);
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetDirection(pos);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetPause(true);
        }
    }

    Vector3 RandomCircle(Vector3 center, float angleModifier, float radius)
    {
        angleModifier = angleModifier / numBullets;
        float ang = angleModifier * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}