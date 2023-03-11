using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class AR_Manager : MonoBehaviour
{
    public List<GameObject> Planets;
    public ARRaycastManager ar_Manager;
    public List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    public bool spawn = true;
    private GameObject spawnedPlanet;
    private float min_Limit = 0.5f;
    private float max_Limit = 2f;
    public float pinchSpeed = 0.5f;
    public int index_planet = 0;
    public Camera cam =null;
    public TextMeshProUGUI trackingText;
    public TextMeshProUGUI tempText;
    public TextMeshProUGUI diameterText;
    public TextMeshProUGUI gravityText;
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI distanceText;


    public List<PlanetData> planetsData;

    public void PlanetStats()
    {
        if (tempText != null)
        {
            tempText.text = planetsData[index_planet].avgTemp.ToString() + "°C";
        }
        if (diameterText!= null)
        {
            diameterText.text = planetsData[index_planet].size.ToString() + "\nkm";

        }
        if (daysText != null)
        {
            daysText.text = "1 Year = \n" + planetsData[index_planet].days.ToString() + "\nDays";
        }
        if (gravityText != null)
        {
            gravityText.text = planetsData[index_planet].gravity.ToString() + "\nm/s²";
        }
        if (distanceText != null)
        {
            distanceText.text = planetsData[index_planet].distance.ToString() + "\nkm";

        }
  
    }
    public void PinchToZoom()
    {
        //If planet has been spanwed AND the touch count on the screen is 2 
        if (spawnedPlanet.transform.childCount > 0 && Input.touchCount == 2)
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
                spawnedPlanet.transform.localScale += (MyDistance * pinchSpeed * Time.deltaTime * Vector3.one);

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
        var screen_point = cam.ScreenToViewportPoint(new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0) );

        //Track a surface plane when a user taps on screen.
        if (ar_Manager.Raycast(screen_point, Hits, TrackableType.PlaneWithinPolygon) && spawn == true)
        {
            var touch = Input.GetTouch(0);

            if(Hits.Count > 0 )
                trackingText.text = "Tracking Found! Tap to spawn thr planet";


            //When tracking is available & User has just Touched.
            if (Hits.Count > 0 && touch.phase == TouchPhase.Began)
            {

                //IF a planet is alrdy spawned destroy it and Create new
                if(spawnedPlanet != null)
                {
                    Destroy(spawnedPlanet);
                }

                PlanetStats();
                //Spawn Planet
                spawnedPlanet = new GameObject("PlanetHolder");
                Instantiate(Planets[index_planet], Vector3.zero, Quaternion.identity, spawnedPlanet.transform);
                spawnedPlanet.transform.position = Hits[0].pose.position;

                trackingText.text = "Spawned Planet: " + Planets[index_planet].name;

                //Turn OFF The flag.
                spawn = false;
            }
        }
    }

    private void SpawnInPC()
    {
        //IF a planet is alrdy spawned destroy it and Create new

        if ( spawnedPlanet != null) 
        {
            Destroy(spawnedPlanet);
        }

        PlanetStats();


        //Spawn Planet
        spawnedPlanet = new GameObject("PlanetHolder");
        Instantiate(Planets[index_planet], Vector3.zero, Quaternion.identity, spawnedPlanet.transform);
        spawnedPlanet.transform.position = Vector3.zero;
        trackingText.text = "Spawned Planet: " + Planets[index_planet].name;
        spawn = false;
    }


    public void NextPlanet()
    {
        //Remove existing planet.
        if (spawnedPlanet.transform.childCount > 0)
            Destroy(spawnedPlanet.transform.GetChild(0).gameObject);

        index_planet++;
        if(index_planet >= Planets.Count)
        {
            index_planet = 0;
        }
        PlanetStats();
        //Spawn Next planet.
        var newPlanet = Instantiate(Planets[index_planet], Vector3.zero, Quaternion.identity, spawnedPlanet.transform);
        newPlanet.transform.localPosition = Vector3.zero;
        trackingText.text = "Spawned Planet: " + Planets[index_planet].name;
       

        spawnedPlanet.transform.localScale = Vector3.one;
    }

    public void Previous_Planet()
    {
        //Remove existing planet.
        if (spawnedPlanet.transform.childCount > 0)
            Destroy(spawnedPlanet.transform.GetChild(0).gameObject);

        index_planet--;
        if(index_planet < 0)
        {
            index_planet = Planets.Count - 1;
        }
        PlanetStats();
        //Spawn Next planet.
        var newPlanet = Instantiate(Planets[index_planet], Vector3.zero, Quaternion.identity, spawnedPlanet.transform);
        newPlanet.transform.localPosition = Vector3.zero;
        

        trackingText.text = "Spawned Planet: " + Planets[index_planet].name;
        spawnedPlanet.transform.localScale = Vector3.one;
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnInPC();

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPlanet();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Previous_Planet();
        }


#if !UNITY_EDITOR
        SpawnPlanet();
        PinchToZoom();
#endif
    }

}