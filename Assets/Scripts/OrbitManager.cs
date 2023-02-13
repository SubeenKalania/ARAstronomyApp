using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    public GameObject Sun;
    public float orbit_tiltAngle;
    public void Update()
    {
        transform.RotateAround(Sun.transform.position,Vector3.up , orbit_tiltAngle *Time.deltaTime);
    }


}
