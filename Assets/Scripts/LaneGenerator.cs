using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneGenerator : MonoBehaviour {

    public Vector3[] LanePositions;
    public int maxLanes;
    public float angleAroundPlanet;
    public int currentLaneCount;
   
    private void Awake()
    {
        LanePositions = new Vector3[maxLanes];
    }

    public void Generate(PlanetController planet)
    {
        
        currentLaneCount = Mathf.Clamp(planet.laneCount, 1, maxLanes);
        for(int i = 0; i < currentLaneCount; i++)
        {
            float x = ((-planet.gutterWidth / 2) + (planet.gutterWidth / (currentLaneCount + 1)) * (i + 1)) * 2;
            float y = Mathf.Sqrt(1 * 1 - x * x);
            Quaternion quat = Quaternion.Euler(angleAroundPlanet, 0, 0);
            Vector3 vec = quat * (new Vector3(x, y, 0)) * planet.planetRadius + planet.transform.position;
            LanePositions[i] = vec;
        }
    }
}
