using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehavioura
{
    [Header("GunMovement")]
    public float armLength = 1f;
    private Transform rotationPoint;


    void Start()
    {
        rotationPoint = transform.parent.transform;    
    }

    void Update()
    {
        // Get direction from player to mouse
        Vector3 centerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotationPoint.position;


        // This puts the gun right above the player on the z-axis
        centerToMouseDir.z = -.43f;

        // Put the gun in that direction, as far as the armlength
        transform.position = rotationPoint.position + (armLength * centerToMouseDir.normalized);
    }
}
