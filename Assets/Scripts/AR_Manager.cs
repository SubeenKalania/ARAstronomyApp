using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class AR_Manager : MonoBehaviour
{
    public GameObject venus_Planet;
    public ARRaycastManager ar_Manager;
    public List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    public bool spawn = true;
    public GameObject spawnedPlanet;
    private float min_Limit = 0.5f;
    private float max_Limit = 2f;
    public float pinchSpeed = 0.5f;

    public void PinchToZoom()
    {
        //If planet has been spanwed AND the touch count on the screen is 2 
        if (spawn == false && Input.touchCount == 2)
        {

            //Get Touch 1 & Touch 2 Data
            var Touch_1 = Input.GetTouch(0);
            var Touch_2 = Input.GetTouch(1);

            //Get Touches previous position
            var Touch_1prevPos = Touch_1.position - Touch_1.deltaPosition;
            var Touch_2PrevPos = Touch_2.position - Touch_2.deltaPosition;


            //Get Touch New & Old positions
            var OldDistance = Vector2.Distance(Touch_1prevPos, Touch_2PrevPos);
            var NewDistance = Vector2.Distance(Touch_1.position, Touch_2.position);

            //Get Pinch Distance
            var MyDistance = OldDistance - NewDistance;


            //Scale the planet with pinch value.
            // Here we are stating the limits between min and max limit for a user to zoomin/out
            if (spawnedPlanet.transform.localScale.y <= max_Limit && spawnedPlanet.transform.localScale.y >= min_Limit)
            {
                spawnedPlanet.transform.localScale += (Vector3.one * MyDistance * Time.deltaTime * pinchSpeed);

                //Checking if the planet was scaled outside of the max limit.
                if(spawnedPlanet.transform.localScale.y > max_Limit)
                {
                    spawnedPlanet.transform.localScale = (Vector3.one * max_Limit);
                }

                //Lower limit
                else if (spawnedPlanet.transform.localScale.y < min_Limit)
                {
                    spawnedPlanet.transform.localScale = (Vector3.one * min_Limit);
                }

            }
        }
    }


    public void SpawnPlanet()
    {
        var touch = Input.GetTouch(0);

        //Track a surface plane when a user taps on screen.
        if (ar_Manager.Raycast(touch.position, Hits, TrackableType.PlaneWithinPolygon) && spawn == true)
        {
            //Spawn Planet
            spawnedPlanet = Instantiate(venus_Planet, Hits[0].pose.position, Quaternion.identity);

            //Turn OFF The flag.
            spawn = false;
        }
    }




    public void Update()
    {

        SpawnPlanet();
        PinchToZoom();
       
    }



}