﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotates the planet and trakcs the distance, also holds the planets state information
/// </summary>
public class PlanetController : MonoBehaviour {

    #region Variables
    [SerializeField, Tooltip("amount of playable lanes to use")]
    private int laneCount;
    [SerializeField, Tooltip("Distance to use for spawning hazards, this variable will be affected by scale")]
    private float gutterWidth;
    [SerializeField, Tooltip("The axis that the planet will rotate on")]
    private Vector3 rotVec;
    [SerializeField, Tooltip("Rotation per second in degrees")]
    private float speed;
    [Tooltip("Radius of planet in unity doesn't account for scale")]
    public float planetRadius;
    [SerializeField, Tooltip("Hazard set with hazards that will spawn on this planet")]
    private HazardGroup hazardSet;
    [Tooltip("Were the player will be sent to on world start")]
    public Vector3 playerPoint;
    [Tooltip("How much the planet will speed up with distance")]
    public float modifier = .1f;
    [Tooltip("Prefab spawned for the level ending")]
    public PlanetExit exit;

    Vector3 planetPosition;
    private bool track;
    private float circumfrence;
    private LevelController controller;

    #region Getters and Setters
    public int LaneCount
    {
        get {
            return laneCount;
        }
    }

    public float GutterWidth
    {
        get {
            return gutterWidth;
        }
    }

    public float Speed
    {
        get {
            return speed;
        }
    }

    public HazardGroup HazardSet
    {
        get {
            return hazardSet;
        }
    }
    #endregion

    #endregion

    #region Implementations
    //Shows position the player will transition to
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + playerPoint * transform.localScale.x, .1f);
    }

    //Calculates circumfrence of planet
    void Start () {
        controller = LevelController.instance;
        circumfrence = 2 * Mathf.PI * planetRadius;
    }
	
    //Rotates planet and tracks rotation when track is ture
	void Update () {
        float currentSpeed = Speed + (controller.Distance * modifier);
        if(track)
        {
            controller.Distance += circumfrence * (currentSpeed * Time.deltaTime / 360);
        }
        transform.Rotate(rotVec * currentSpeed * Time.deltaTime);
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Activates tracking of rotation distance
    /// </summary>
    public void Begin(){
        track = true;
    }

    /// <summary>
    /// Stops tracking and spawns the exit prefab 
    /// </summary>
    public void End(){
        track = false;
        PlanetExit exitPrefab = Instantiate(exit);
        exitPrefab.transform.parent = controller.GetCurrentPlanet().transform;
        exitPrefab.transform.position = LaneGenerator.instance.LanePositions[2];
        exitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.up, exitPrefab.transform.position - controller.GetCurrentPlanet().transform.position);
    }
    #endregion

}
