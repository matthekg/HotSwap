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
    public string Pattern1Input = "Pattern1";
    private bool shootingPattern1 = false;
    [SerializeField] int numBullets_ring = 12;
    [SerializeField] float circleSpawnRadius = 3f;

    [Header("[2] : BulletSpiral")]
    public string Pattern2Input = "Pattern2";
    public string clockwise = "Clockwise";
    private bool shootingPattern2 = false;
    private bool shootingAnti = false;
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
        if (Input.GetAxisRaw(Pattern1Input) != 0 && !shootingPattern1)
        {
            shootingPattern1 = true;
            SpawnBulletRing();
        }
        if (Input.GetAxisRaw(Pattern1Input) == 0)
            shootingPattern1 = false;

        if ( meterLeft < spiralMeterCapacity && !Input.GetKey(KeyCode.Keypad2) )
            ++meterLeft;
        UpdateSpiralMeter();

        if (Input.GetAxisRaw(Pattern2Input) != 0 && !shootingPattern2)
        {
            shootingPattern2 = true;
            spiral = StartCoroutine(SpawnSpiral());
        }    
        if (Input.GetAxisRaw(Pattern2Input) == 0)
        {
            StopCoroutine(spiral);
            shootingPattern2 = false;
        }

        if (Input.GetAxisRaw(clockwise) != 0 && !shootingAnti)
        {
            shootingAnti = true;
            counterClockwise = ToggleBool(counterClockwise);
        }
        if (Input.GetAxisRaw(clockwise) == 0)
            shootingAnti = false;

    }

    public void SpawnBulletRing()
    {
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
            meterLeft -= 2;
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
        return true;
    }
}