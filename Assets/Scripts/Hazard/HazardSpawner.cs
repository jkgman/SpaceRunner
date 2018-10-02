        //hazard.transform.position = rotater.radius * spawnVector.normalized+rotater.transform.position;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rotate))]
public class HazardSpawner : MonoBehaviour
{
    public float spawnFrequencyinUnits = 2;
    private Rotate rotater;
    [Range(1, 7)]
    public int lanes;
    [Range(0, 1)]
    public float gutterWidth;
    [Range(0, 360)]
    public int angle;
    public Hazard[] Hazards;
    public PlayerHandle player;
    public GameObject rightGutter;
    public GameObject leftGutter;

    private Vector3[] lanePoints;
    // Use this for initialization
    void Start()
    {
        rotater = GetComponent<Rotate>();
        PlaceLanes();
        Spawn();
        rightGutter.transform.localPosition = new Vector3(-gutterWidth / 2, 0, 0);
        leftGutter.transform.localPosition = new Vector3(gutterWidth / 2, 0, 0);
    }


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
                hazard.transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
                hazard.player = player;
            }
        }
        
    }
    void PlaceLanes()
    {
        lanePoints = new Vector3[lanes];
        for(int i = 0; i < lanes; i++)
        {
            float x = ((-gutterWidth / 2)  + (gutterWidth / (lanes + 1)) * (i + 1))*2;
            float y = Mathf.Sqrt(1 * 1 - x * x);
            lanePoints[i] = Quaternion.Euler(angle, 0, 0) * (new Vector3(x, y, 0))*rotater.scale;
        }
    }
}

