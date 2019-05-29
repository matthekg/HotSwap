using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Courtesy of https://answers.unity.com/questions/714835/best-way-to-spawn-prefabs-in-a-circle.html
 */

public class BulletSpawner : MonoBehaviour
{
    public bool isPlayer1;

    public GameObject bossGameObject = null;
    public GameObject bulletPrefab;

    private Slider spiralSlider = null;

    [Header("[1] : BulletRing")]
    public string Pattern1Input = null;
    private bool shootingPattern1 = false;
    [SerializeField] int numBullets_ring = 12;
    [SerializeField] float circleSpawnRadius = 3f;

    [Header("[2] : BulletSpiral")]
    public string Pattern2Input = null;
    public string clockwise = null;
    private bool shootingPattern2 = false;
    private bool shootingAnti = false;
    [SerializeField] int numBullets_spiral = 20;
    [SerializeField] int skipEvery = 1;
    [SerializeField] float spiralPauseTime = 1f;
    [SerializeField] bool counterClockwise = false;
    [SerializeField] int spiralMeterCapacity = 50;
    [SerializeField] int meterLeft = 0;
    [SerializeField] int meterGain = 1;
    [SerializeField] int meterDrain = 1;
    private Coroutine spiral = null;

    GameObject gm = null;
    GameManager gmScript = null;

    void Awake()
    {
        spiralSlider = GameObject.Find("SpiralMeterSlider").GetComponent<Slider>();
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();
        isPlayer1 = bossGameObject.GetComponent<BossMovement>().isPlayer1;
        if (isPlayer1)
        {
            Pattern1Input = gmScript.Pattern1Input;
            Pattern2Input = gmScript.Pattern2Input;
            clockwise = gmScript.clockwise;
        }
        else
        {
            Pattern1Input = gmScript.Pattern1InputP2;
            Pattern2Input = gmScript.Pattern2InputP2;
            clockwise = gmScript.clockwiseP2;
        }

    }

    void Update()
    {
        /**********PATTERN 1 | RING************/
        if (Input.GetAxisRaw(Pattern1Input) != 0 && !shootingPattern1)
        {
            shootingPattern1 = true;
            SpawnBulletRing();
        }
        if (Input.GetAxisRaw(Pattern1Input) == 0)
            shootingPattern1 = false;
        /**************************************/

        /**********PATTERN 2 | SPIRAL************/
        if ( meterLeft < spiralMeterCapacity && !shootingPattern2 )
            meterLeft += meterGain;
        UpdateSpiralMeter();

        if (Input.GetAxisRaw(Pattern2Input) != 0 && !shootingPattern2)
        {
            shootingPattern2 = true;
            spiral = StartCoroutine(SpawnSpiral());
        }    
        if (Input.GetAxisRaw(Pattern2Input) == 0 && spiral != null)
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
        /****************************************/
    }

    public void SpawnBulletRing()
    {
        Vector3 center = bossGameObject.transform.position;
        for (int i = 1; i <= numBullets_ring; ++i)
        {
            Vector3 pos = Circle(center, i, circleSpawnRadius, numBullets_ring);
            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetDirection(pos - center);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetPause(true);
        }
    }

    IEnumerator SpawnSpiral()
    {
        int counter = 0;
        while (true)
        {
            meterLeft -= meterDrain;
            if (meterLeft <= 0)
                break;
            if (counterClockwise)
                counter -= skipEvery;
            else
                counter += skipEvery;

            Vector3 center = bossGameObject.transform.position;
            Vector3 pos = Circle(center, counter, circleSpawnRadius, numBullets_spiral);

            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.gameObject.GetComponent<BossBulletLogic>().SetDirection(pos - center);
            yield return new WaitForSeconds(spiralPauseTime);
        }
    }
    private void UpdateSpiralMeter()
    {
        GameObject.Find("SpiralFill").GetComponent<Image>().color = gmScript.currentBossColor;
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
        return target ? false : true;
    }

    public void SwapControls()
    {
        isPlayer1 = isPlayer1 ? false : true;
        if (isPlayer1)
        {
            Pattern1Input = gmScript.Pattern1Input;
            Pattern2Input = gmScript.Pattern2Input;
            clockwise = gmScript.clockwise;
        }
        else
        {
            Pattern1Input = gmScript.Pattern1InputP2;
            Pattern2Input = gmScript.Pattern2InputP2;
            clockwise = gmScript.clockwiseP2;
        }
    }
}