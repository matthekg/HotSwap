using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    [Header("GunMovement")]
    public float armLength = 1f;
    private Transform rotationPoint;
    private string shootControl;
    private bool currentlyShooting = false;
    private float nextFire = 0f;
    [SerializeField] float autoFireRate = 1f;
    [SerializeField] GameObject bullet;

    GameObject gm = null;
    GameManager gmScript = null;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();

        shootControl = GameObject.Find("GameManager").GetComponent<GameManager>().currentShootControl;
        rotationPoint = transform.parent.transform; 
    }

    void Update()
    {
        if (gmScript.paused)
            return;

        // Get direction from player to mouse
        Vector3 centerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotationPoint.position;


        // This puts the gun right above the player on the z-axis
        centerToMouseDir.z = -.43f;

        // Put the gun in that direction, as far as the armlength
        Vector3 gunDistanceFromSelf = armLength * centerToMouseDir.normalized;
        transform.position = rotationPoint.position + gunDistanceFromSelf;

        shootControl = gmScript.currentShootControl;
        if (Input.GetAxisRaw(shootControl) != 0) {
            if (!currentlyShooting || Time.time > nextFire) {
                Shoot(centerToMouseDir);
            }
        }
        if (Input.GetAxisRaw(shootControl) == 0) {
            currentlyShooting = false;
        }

    }

    public void Shoot( Vector2 direction )
    {
        currentlyShooting = true;

        nextFire = Time.time + autoFireRate;
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity );
        newBullet.GetComponent<BulletLogic>().SetDirection( direction.normalized );

    }
}
