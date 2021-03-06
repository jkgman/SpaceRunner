﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawn hazards
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class HazardSpawner : MonoBehaviour
{

    #region Variables
    private bool isSpawning;
    private HazardChunk currentchunk;
    private BoxCollider garbageCollector;
    private LaneGenerator lane;
    private LevelController controller;
    private HazardChunk lastChunck;
    #endregion

    #region Implementations
    void Start()
    {
        controller = LevelController.instance;
        lane = FindObjectOfType<LaneGenerator>();
        garbageCollector = GetComponent<BoxCollider>();
    }
    //Garbage Collecting
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hazard" || other.tag == "Coin" || other.tag == "Collectable")
        {
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// sets up the garbage collector at planet
    /// </summary>
    /// <param name="planet">Said planet that is focus of this function</param>
    public void Generate(PlanetController planet)
    {
        garbageCollector.transform.position = planet.transform.position + new Vector3(0, 0, -planet.planetRadius);
        garbageCollector.size = new Vector3(planet.GutterWidth * planet.planetRadius * 2, .1f, planet.planetRadius);
    }

    /// <summary>
    /// Sets spawning to true and calls newchunk if there isnt a current one
    /// </summary>
    public void Begin()
    {
        isSpawning = true;
        if(currentchunk == null)
        {
            NewChunck();
        }
    }

    /// <summary>
    /// Deactivates further spawning
    /// </summary>
    public void Stop() {
        isSpawning = false;
        currentchunk.DestroyChunck();
    }

    /// <summary>
    /// If spawning it sets and instantiates a chunck from planets sets
    /// </summary>
    public void NewChunck() {
        if(isSpawning)
        {
            for(int i = 0; i < 4; i++)
            {
                currentchunk = controller.GetCurrentPlanet().chunks[Random.Range(0, controller.GetCurrentPlanet().chunks.Length)];
                if(lastChunck == null || currentchunk.gameObject.name != lastChunck.gameObject.name)
                {
                    break;
                }
            }
            currentchunk = Instantiate(currentchunk);
            currentchunk.Spawner = this;
            lastChunck = currentchunk;
        }
    }

    /// <summary>
    /// takes a hazard row and spawns them around current planet lanes
    /// </summary>
    /// <param name="group"></param>
    public void Spawn(HazardRow group) {
        for(int i = 0; i < group.hazards.Length; i++)
        {
            Hazard hazard = Instantiate(group.hazards[i]);
            hazard.transform.position = lane.LanePositions[group.hazards[i].Lane];
            float angle = Vector3.SignedAngle(
                lane.LanePositions[2] - LevelController.instance.GetCurrentPlanet().transform.position,
                lane.LanePositions[LevelController.instance.currentLane] - LevelController.instance.GetCurrentPlanet().transform.position,
                new Vector3(0, -1, 0));
            hazard.transform.RotateAround(LevelController.instance.GetCurrentPlanet().transform.position, new Vector3(0, 0, 1), angle);
            float x = 0, y = 1;
            x = Mathf.Sin(Mathf.Deg2Rad * -angle);
            y = Mathf.Cos(Mathf.Deg2Rad * -angle);
            Vector3 forward = new Vector3(x, y, 0);
            Vector3 up = (hazard.transform.position - controller.GetCurrentPlanet().transform.position).normalized;
            hazard.transform.rotation = Quaternion.LookRotation(forward, up);
            hazard.transform.parent = controller.GetCurrentPlanet().transform;
        }
        
    }
    #endregion

}
