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
    [SerializeField] GameObject bullet;

    void Start()
    {
        shootControl = transform.parent.GetComponent<HeroMovement>().shootControl;
        rotationPoint = transform.parent.transform;    
    }

    void Update()
    {
        // Get direction from player to mouse
        Vector3 centerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotationPoint.position;


        // This puts the gun right above the player on the z-axis
        centerToMouseDir.z = -.43f;

        // Put the gun in that direction, as far as the armlength
        Vector3 gunDistanceFromSelf = armLength * centerToMouseDir.normalized;
        transform.position = rotationPoint.position + gunDistanceFromSelf;

        if (Input.GetAxisRaw(shootControl) != 0) {
            if (currentlyShooting == false) {
                currentlyShooting = true;
                Shoot(centerToMouseDir);
            }
        }
        if (Input.GetAxisRaw(shootControl) == 0) {
            currentlyShooting = false;
        }

    }

    public void Shoot( Vector2 direction )
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity );
        newBullet.GetComponent<BulletLogic>().SetDirection( direction.normalized );
    }
}
