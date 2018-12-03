﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotates the planet and trakcs the distance, also holds the planets state information
/// </summary>
public class PlanetController : MonoBehaviour {

    #region Variables
    public HazardChunk[] chunks;
    [SerializeField, Tooltip("amount of playable lanes to use")]
    private int laneCount;
    [SerializeField, Tooltip("Distance to use for spawning hazards, this variable will be affected by scale")]
    private float gutterWidth;
    [SerializeField, Tooltip("Rotation per second in degrees")]
    private float speed;
    private float laneChangeSpeed;
    [Tooltip("Radius of planet in unity doesn't account for scale")]
    public float planetRadius;
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
    private Vector3 targetLaneRot = Vector3.zero;

    public delegate void OnPlanetRot(Vector3 eul, float angle, Transform planet);
    public OnPlanetRot onPlanetRot;
    
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
        laneChangeSpeed = FindObjectOfType<PlayerHandle>().speed;
    }
	
    //Calculates the current target speed and calls rotate
	void Update () {
        float currentSpeed = Speed + (controller.Distance * modifier);
        if(track)
        {
            controller.Distance += circumfrence * (currentSpeed * Time.deltaTime / 360);
        }
        Rotate(currentSpeed);
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// eul rot around x, tells all subscribed objects about the rotation, then rotates over world z towards target lane
    /// </summary>
    /// <param name="currentSpeed"></param>
    private void Rotate(float currentSpeed) {
        Vector3 eulerrot = new Vector3(-1 * currentSpeed * Time.deltaTime, 0, 0);
        transform.Rotate(eulerrot);
        if(onPlanetRot != null)
        {
            onPlanetRot.Invoke(eulerrot, (eulerrot.x / circumfrence) * 360, transform);
        }
        if(targetLaneRot.z != 0)
        {
            float target = targetLaneRot.z * Time.deltaTime * laneChangeSpeed;
            if(targetLaneRot.z >= 0)
            {
                target = Mathf.Min(targetLaneRot.z, target);
            } else if(targetLaneRot.z <= 0)
            {
                target = Mathf.Max(targetLaneRot.z, target);
            }
            targetLaneRot = new Vector3(0,0, targetLaneRot.z - target);
            transform.Rotate(0, 0, target, Space.World);
        }
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
        exitPrefab.transform.position = LaneGenerator.instance.LanePositions[2];
        exitPrefab.transform.parent = controller.GetCurrentPlanet().transform;
        exitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.up, exitPrefab.transform.position - controller.GetCurrentPlanet().transform.position);
    }

    /// <summary>
    /// sets the target z rotation of the planet by the difference between from - to
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void RotateLane(Vector3 from, Vector3 to) {
        targetLaneRot += new Vector3(0, 0, Vector3.SignedAngle(from - transform.position, to - transform.position, Vector3.forward));
    }
    #endregion

}