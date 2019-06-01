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
    [SerializeField] int numBullets_ring = 12;
    [SerializeField] float circleSpawnRadius = 3f;

    [Header("[2] : BulletSpiral")]
    public string Pattern2Input = null;
    public string clockwise = null;
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
        if (gmScript.paused)
            return;
        /**********PATTERN 1 | RING************/
        if (Input.GetButtonDown(Pattern1Input))
            SpawnBulletRing();
        /**************************************/

        /**********PATTERN 2 | SPIRAL************/
        if ( meterLeft < spiralMeterCapacity && Input.GetAxisRaw(Pattern2Input) == 0 )
            meterLeft += meterGain;
        UpdateSpiralMeter();

        if (Input.GetButtonDown(Pattern2Input))
        {
            spiral = StartCoroutine(SpawnSpiral());
        }    
        if (Input.GetAxisRaw(Pattern2Input) == 0)
        {
            StopCoroutine(spiral);
        }

        if (Input.GetButtonDown(clockwise))
            counterClockwise = ToggleBool(counterClockwise);
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
        // make it so cooldowns switch sides as well

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