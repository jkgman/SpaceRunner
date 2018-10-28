using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Track lanes, move gutters, spawn hazards
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class HazardSpawner : MonoBehaviour
{


    [SerializeField]
    private HazardGroup hazardSet;
    private BoxCollider garbageCollector;
    private LaneGenerator lane;
    private bool isSpawning;
    private LevelController controller;
    public float distanceBetweenSpawn;
    private float LastSpawnDist = 0;
    private void Start()
    {
        controller = LevelController.instance;
        lane = FindObjectOfType<LaneGenerator>();
        garbageCollector = GetComponent<BoxCollider>();
    }

    void Update()
    {

        if(isSpawning && controller.distance - LastSpawnDist >= distanceBetweenSpawn)
        {
            Spawn();
            LastSpawnDist = controller.distance;
        }
    }


    public void Generate(PlanetController planet)
    {
        hazardSet = planet.hazardSet;
        garbageCollector.transform.position = planet.transform.position + new Vector3(0, 0, -planet.planetRadius);
        garbageCollector.size = new Vector3(planet.gutterWidth * planet.planetRadius * 2, .1f, planet.planetRadius);
    }

    public void Begin()
    {
        Spawn();
        isSpawning = true;
    }

    /*Stop(){
      *End auto Spawning
      */


    void Spawn(){
        int totalHazards = 0;
        int maxHazardLength = Random.Range(2, lane.currentLaneCount+1);

        int currentHazardLength = 0;
        bool traversible = false;
        bool[] usedOnce = new bool[hazardSet.hazards.Length];
        bool[] preferedLaneUsed = new bool[lane.currentLaneCount];
        Hazard[] hazardsToSpawn = new Hazard[maxHazardLength];
        if(maxHazardLength != lane.currentLaneCount)
        {
            traversible = true;
        }
        int loopcount = 0;
        while(currentHazardLength <= maxHazardLength){
            loopcount++;
            int biggestHazard = maxHazardLength - currentHazardLength;
            int tryHazard = Random.Range(0, hazardSet.hazards.Length);
            if(hazardSet.hazards[tryHazard].Length <= biggestHazard && !(hazardSet.hazards[tryHazard].useOnlyOnce && usedOnce[tryHazard]))
            {
                if(hazardSet.hazards[tryHazard].Length == biggestHazard && !traversible && !hazardSet.hazards[tryHazard].Traversible())
                {
                    break;
                } else
                {
                    if(hazardSet.hazards[tryHazard].hasPreferedLane)
                    {
                        for(int j = 0; j < hazardSet.hazards[tryHazard].Length; j++)
                        {
                            if(preferedLaneUsed[hazardSet.hazards[tryHazard].preferedLane + j])
                            {
                                break;
                            }
                        }
                    }
                    hazardsToSpawn[currentHazardLength] = hazardSet.hazards[tryHazard];
                    currentHazardLength += hazardSet.hazards[tryHazard].Length;
                    totalHazards++;
                    usedOnce[tryHazard] = true;
                    if(hazardSet.hazards[tryHazard].Traversible())
                    {
                        traversible = true;
                    }
                    if(hazardSet.hazards[tryHazard].hasPreferedLane)
                    {
                        for(int i = 0; i < hazardSet.hazards[tryHazard].Length; i++)
                        {
                            preferedLaneUsed[hazardSet.hazards[tryHazard].preferedLane + i] = true;
                        }
                    }
                }
            }
            if(loopcount == 20)
            {
                break;
            }
        }
        int[] finalLocations = new int[lane.currentLaneCount];
        for(int i = 0; i < hazardsToSpawn.Length; i++)
        {
            if(hazardsToSpawn[i] && hazardsToSpawn[i].hasPreferedLane)
            {
                finalLocations[hazardsToSpawn[i].preferedLane] = i + 1;
                for(int j = 1; j < hazardsToSpawn[i].Length; j++)
                {
                    finalLocations[hazardsToSpawn[i].preferedLane + j] =  -1;
                }
            }
        }
        for(int i = 0; i < hazardsToSpawn.Length; i++)
        {
            if(hazardsToSpawn[i] &&  !hazardsToSpawn[i].hasPreferedLane)
            {
                for(int j = 0; j < finalLocations.Length; j++)
                {
                    if(finalLocations[j] == 0)
                    {
                        finalLocations[j] = i + 1;
                        break;
                    }
                }
            }
        }

        int freeLanes = lane.currentLaneCount - maxHazardLength;
        int temp = 0;
        for(int i = 0; i < freeLanes; i++)
        {
            int spaceLoc = Random.Range(0, totalHazards - 1);

            for(int j = 0; j < lane.currentLaneCount; j++)
            {
                if(finalLocations[j] == spaceLoc + 2)
                {
                    temp = finalLocations[j];
                    finalLocations[j] = 0;
                    int temp2;
                    for(int k = j+1; k < lane.currentLaneCount; k++)
                    {
                        temp2 = finalLocations[j];
                        finalLocations[j] = temp;
                        temp = temp2;
                    }
                    break;
                }

            }
        }
        for(int i = 0; i < finalLocations.Length; i++)
        {
            if(finalLocations[i] > 0)
            {
                Hazard hazard = Instantiate(hazardsToSpawn[finalLocations[i]-1]);
                hazard.transform.parent = controller.GetCurrentPlanet().transform;
                hazard.transform.position = lane.LanePositions[i];
                hazard.transform.rotation = Quaternion.LookRotation(Vector3.up, hazard.transform.position - controller.GetCurrentPlanet().transform.position);
            }
        }


    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hazard")
        {
            Destroy(other.gameObject);
        }
    }


}
