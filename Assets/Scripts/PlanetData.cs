using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Planets_Data", menuName = "Tools/Datafiles", order = 1)]
public class PlanetData : ScriptableObject
{
    public string name;
    public float avgTemp;
    public float gravity;
    public int noOfMoons;
    public long days;
    public long size;
    public long distance;

}
