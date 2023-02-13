using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolveManager : MonoBehaviour
{
    public GameObject planet;
    public float rotate_Speed;
    public Vector3 tilt_Angle;

    public void Start()
    {
        planet.transform.eulerAngles = tilt_Angle;
    }

    public void Update()
    {
        //aceesing the roation propery of Planet game object to manage speed and rotation along y axis
        planet.transform.Rotate(rotate_Speed * Time.deltaTime * Vector3.up);
          
    }
}

