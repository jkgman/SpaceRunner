using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawn hazards
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class HazardSpawner : MonoBehaviour
{

    #region Variables
    [Tooltip("Meters traveled before a spawn occurs")]
    public float distanceBetweenSpawn;
    private float lastSpawnDist = 0;
    private bool isSpawning;
    //Set the spawner pulls from
    private HazardGroup hazardSet;
    //Collider used to delete Hazards
    private BoxCollider garbageCollector;
    private LaneGenerator lane;
    private LevelController controller;
    #endregion

    #region Implementations
    void Start()
    {
        controller = LevelController.instance;
        lane = FindObjectOfType<LaneGenerator>();
        garbageCollector = GetComponent<BoxCollider>();
    }
    //Spawns if spawning and traveled distance
    void Update()
    {
        if(isSpawning && controller.Distance - lastSpawnDist >= distanceBetweenSpawn)
        {
            Spawn();
        }
    }
    //Garbage Collecting
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hazard")
        {
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Saves the Hazardset from the planet, and sets up the garbage collector at said planet
    /// </summary>
    /// <param name="planet">Said planet that is focus of this function</param>
    public void Generate(PlanetController planet)
    {
        hazardSet = planet.HazardSet;
        garbageCollector.transform.position = planet.transform.position + new Vector3(0, 0, -planet.planetRadius);
        garbageCollector.size = new Vector3(planet.GutterWidth * planet.planetRadius * 2, .1f, planet.planetRadius);
    }

    /// <summary>
    /// Calls an initial Spawn and activates further spawns
    /// </summary>
    public void Begin()
    {
        Spawn();
        isSpawning = true;
    }

    /// <summary>
    /// Deactivates further spawning
    /// </summary>
    public void Stop() {
        isSpawning = false;
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Monster function that spawns hazards while assuring an open path
    /// </summary>
    void Spawn(){
        lastSpawnDist = controller.Distance;
        //TODO: (Must) Some times a hazard will be null creating an empty lane
        //TODO: Spacing phase seems to fail sometimes
        //TODO: Discuss if this should be thrown away in favor of premade sets

        //Dont look at any of this code if you dont want to catch ligma
        int totalHazards = 0;
        int maxHazardLength = Random.Range(2, lane.CurrentLaneCount+1);
        int currentHazardLength = 0;
        bool traversible = false;
        bool[] usedOnce = new bool[hazardSet.hazards.Length];
        bool[] preferedLaneUsed = new bool[lane.CurrentLaneCount];
        Hazard[] hazardsToSpawn = new Hazard[maxHazardLength];
        if(maxHazardLength != lane.CurrentLaneCount)
        {
            traversible = true;
        }
        int loopcount = 0;
        while(currentHazardLength <= maxHazardLength){
            loopcount++;
            int biggestHazard = maxHazardLength - currentHazardLength;
            int tryHazard = Random.Range(0, hazardSet.hazards.Length);
            if(hazardSet.hazards[tryHazard].Length <= biggestHazard && !(hazardSet.hazards[tryHazard].UseOnlyOnce && usedOnce[tryHazard]))
            {
                if(hazardSet.hazards[tryHazard].Length == biggestHazard && !traversible && !hazardSet.hazards[tryHazard].Traversible())
                {
                    break;
                } else
                {
                    if(hazardSet.hazards[tryHazard].HasPreferedLane)
                    {
                        for(int j = 0; j < hazardSet.hazards[tryHazard].Length; j++)
                        {
                            if(preferedLaneUsed[hazardSet.hazards[tryHazard].PreferedLane + j])
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
                    if(hazardSet.hazards[tryHazard].HasPreferedLane)
                    {
                        for(int i = 0; i < hazardSet.hazards[tryHazard].Length; i++)
                        {
                            preferedLaneUsed[hazardSet.hazards[tryHazard].PreferedLane + i] = true;
                        }
                    }
                }
            }
            if(loopcount == 20)
            {
                break;
            }
        }
        int[] finalLocations = new int[lane.CurrentLaneCount];
        for(int i = 0; i < hazardsToSpawn.Length; i++)
        {
            if(hazardsToSpawn[i] && hazardsToSpawn[i].HasPreferedLane)
            {
                finalLocations[hazardsToSpawn[i].PreferedLane] = i + 1;
                for(int j = 1; j < hazardsToSpawn[i].Length; j++)
                {
                    finalLocations[hazardsToSpawn[i].PreferedLane + j] =  -1;
                }
            }
        }
        for(int i = 0; i < hazardsToSpawn.Length; i++)
        {
            if(hazardsToSpawn[i] &&  !hazardsToSpawn[i].HasPreferedLane)
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

        int freeLanes = lane.CurrentLaneCount - maxHazardLength;
        int temp = 0;
        for(int i = 0; i < freeLanes; i++)
        {
            int spaceLoc = Random.Range(0, totalHazards - 1);

            for(int j = 0; j < lane.CurrentLaneCount; j++)
            {
                if(finalLocations[j] == spaceLoc + 2)
                {
                    temp = finalLocations[j];
                    finalLocations[j] = 0;
                    int temp2;
                    for(int k = j+1; k < lane.CurrentLaneCount; k++)
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
    #endregion

}
