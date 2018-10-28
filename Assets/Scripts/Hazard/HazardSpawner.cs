using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Track lanes, move gutters, spawn hazards
/// </summary>
[RequireComponent(typeof(Rotate))]
public class HazardSpawner : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private float spawnFrequencyinUnits = 2;
    [SerializeField, Range(1, 7)]
    private int lanes;
    [SerializeField, Range(0, 1)]
    private float gutterWidth;
    [SerializeField, Range(0, 360)]
    private int angle;
    [SerializeField]
    private Hazard[] Hazards;
    [SerializeField]
    private GameObject rightGutter;
    [SerializeField]
    private GameObject leftGutter;

    private Rotate rotater;
    private PlayerHandle player;
    private Vector3[] lanePoints;
    #endregion

    #region Implementations
    /// <summary>
    /// Get References and call lane calculation
    /// </summary>
    void Start()
    {
        player = FindObjectOfType<PlayerHandle>();
        rotater = GetComponent<Rotate>();
        PlaceLanes();
        Spawn();
        player.lanePoints = lanePoints;
        rightGutter.transform.localPosition = new Vector3(-gutterWidth / 2, 0, 0);
        leftGutter.transform.localPosition = new Vector3(gutterWidth / 2, 0, 0);
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Calculate the points were the lanes are
    /// </summary>
    void PlaceLanes()
    {
        lanePoints = new Vector3[lanes];
        for(int i = 0; i < lanes; i++)
        {
            float x = ((-gutterWidth / 2) + (gutterWidth / (lanes + 1)) * (i + 1)) * 2;
            float y = Mathf.Sqrt(1 * 1 - x * x);
            lanePoints[i] = Quaternion.Euler(angle, 0, 0) * (new Vector3(x, y, 0)) * rotater.Scale;
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Spawn a random number of hazards in random lanes, always leaving atleast one open lane
    /// </summary>
    public void Spawn()
    {
        int hazardcount = Random.Range(1, lanes);
        bool[] laneHazard = new bool[lanes];
        for(int i = 0; i < hazardcount; i++)
        {
            while(true)
            {
                int lane = Random.Range(0, lanes);
                if(!laneHazard[lane])
                {
                    laneHazard[lane] = true;
                    break;
                }
            }
        }
        for(int i = 0; i < laneHazard.Length; i++)
        {
            if(laneHazard[i])
            {
                Hazard hazard = Instantiate(Hazards[Random.Range(0, Hazards.Length)]);
                hazard.transform.parent = rotater.transform;
                hazard.transform.position = lanePoints[i] + rotater.transform.position;
                hazard.transform.rotation = Quaternion.FromToRotation(Vector3.down, rotater.transform.position - hazard.transform.position);
                hazard.Player = player;
            }
        }
    }
    #endregion
    
}

