using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationCreator : MonoBehaviour
{
    public LineRenderer lineCreator;
    public List<GameObject> cluster;

    /*
     * ALGO:
     * 1. For each and every star in the cluster
     * 2. Use Line Creator to set a point  ot that star's postion.
     */
    public void Update()
    {
        CreateCluster();
    }
    public void CreateCluster()
    {
        //Set LIne Creator's size.
        lineCreator.positionCount = cluster.Count;


        for(int i = 0; i < cluster.Count; i++)
        {
            //For every index, Set the line's vertex position to that star
            lineCreator.SetPosition(i, cluster[i].transform.position);
        }
    }


}

