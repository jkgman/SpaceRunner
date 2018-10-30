using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates the lanes to use for spawning and player controls
/// </summary>
public class LaneGenerator : MonoBehaviour {

    #region Variables
    private Vector3[] lanePositions;
    [SerializeField, Tooltip("Establishes the max lanes a planet can have")]
    private int maxLanes;
    [SerializeField, Tooltip("Angle offset for were lanes start, this is used for spawning")]
    private float angleAroundPlanet;
    private int currentLaneCount;

    #region Getters and Setters
    public Vector3[] LanePositions
    {
        get {
            return lanePositions;
        }

        private set {
            lanePositions = value;
        }
    }

    public int MaxLanes
    {
        get {
            return maxLanes;
        }

        private set {
            maxLanes = value;
        }
    }

    public float AngleAroundPlanet
    {
        get {
            return angleAroundPlanet;
        }
    }

    public int CurrentLaneCount
    {
        get {
            return currentLaneCount;
        }
        private set {
            currentLaneCount = value;
        }
    }
    #endregion

    #endregion

    #region Singleton
    public static LaneGenerator instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of LevelController found");
            return;
        }
        instance = this;
        LanePositions = new Vector3[MaxLanes];
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Sets the lane points for how many lanes the planet wants
    /// </summary>
    /// <param name="planet"></param>
    public void Generate(PlanetController planet)
    {
        CurrentLaneCount = Mathf.Clamp(planet.LaneCount, 1, MaxLanes);
        for(int i = 0; i < CurrentLaneCount; i++)
        {
            float x = ((-planet.GutterWidth / 2) + (planet.GutterWidth / (CurrentLaneCount + 1)) * (i + 1)) * 2;
            float y = Mathf.Sqrt(1 * 1 - x * x);
            Quaternion quat = Quaternion.Euler(AngleAroundPlanet, 0, 0);
            Vector3 vec = quat * (new Vector3(x, y, 0)) * planet.planetRadius + planet.transform.position;
            LanePositions[i] = vec;
        }
    }
    #endregion

}
