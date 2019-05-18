using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Courtesy of https://answers.unity.com/questions/714835/best-way-to-spawn-prefabs-in-a-circle.html
 */

public class BulletSpawner : MonoBehaviour
{
    public GameObject bossGameObject = null;
    public GameObject bulletPrefab;

    private Slider spiralSlider = null;

    [Header("[1] : BulletRing")]
    [SerializeField] int numBullets_ring = 12;
    [SerializeField] float circleSpawnRadius = 3f;

    [Header("[2] : BulletSpiral")]
    [SerializeField] int numBullets_spiral = 20;
    [SerializeField] int skipEvery = 1;
    [SerializeField] float spiralPauseTime = 1f;
    [SerializeField] bool counterClockwise = false;
    [SerializeField] int spiralMeterCapacity = 50;
    [SerializeField] int meterLeft = 0;
    private Coroutine spiral = null;
    

    void Awake()
    {
        spiralSlider = GameObject.Find("SpiralMeterSlider").GetComponent<Slider>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SpawnBulletRing();
        }

        if( meterLeft < spiralMeterCapacity && !Input.GetKey(KeyCode.Keypad2) )
            ++meterLeft;
        UpdateSpiralMeter();

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("BulletSpiral!");
            spiral = StartCoroutine(SpawnSpiral());
        }    
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            StopCoroutine(spiral);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            counterClockwise = ToggleBool(counterClockwise);
        }

    }

    public void SpawnBulletRing()
    {
        Debug.Log("BulletRing!");
        Vector3 center = bossGameObject.transform.position;
        for (int i = 1; i <= numBullets_ring; ++i)
        {
            Vector3 pos = Circle(center, i, circleSpawnRadius, numBullets_ring);
            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetDirection(pos);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetPause(true);
        }
    }
    // set angles to high and skip angles
    IEnumerator SpawnSpiral()
    {
        int counter = 0;
        while (true)
        {
            --meterLeft;
            if (meterLeft <= 0)
                break;
            if (counterClockwise)
                counter -= skipEvery;
            else
                counter += skipEvery;

            Vector3 center = bossGameObject.transform.position;
            Vector3 pos = Circle(center, counter, circleSpawnRadius, numBullets_spiral);

            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetDirection(pos);
            yield return new WaitForSeconds(spiralPauseTime);
        }
    }
    private void UpdateSpiralMeter()
    {
        spiralSlider.value = meterLeft;
    }

    Vector3 Circle(Vector3 center, float angleModifier, float radius, float numBullets)
    {
        angleModifier = angleModifier / numBullets;
        float ang = angleModifier * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    private bool ToggleBool(bool target)
    {
        if (target)
            return false;
        else
            return true;
    }
}