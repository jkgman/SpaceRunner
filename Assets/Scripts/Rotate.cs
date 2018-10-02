using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotate the planet, increasing speed with distance.
/// Also tracks the distance rotated 
/// </summary>
public class Rotate : MonoBehaviour {

    #region Variables
    [SerializeField]
    private Vector3 rotVec;
    [SerializeField]
    private HazardSpawner hazardSpawner;
    [SerializeField, Range(0,360)]
    private float spawnAngle;
    [SerializeField]
    private float rate;
    [SerializeField, Range(0, 360)]
    private float maxSpeed;
    [SerializeField, Range(0, 360)]
    private float minSpeed;

    private float scale;
    private float angle;
    private float distance;
    private float circumfrence;
    #endregion

    #region Getters and Setters
    public float Scale
    {
        get {
            return scale;
        }

        private set {
            scale = value;
        }
    }

    public float Distance
    {
        get {
            return distance;
        }

        private set {
            distance = value;
        }
    }
    #endregion

    #region Implementations
    /// <summary>
    /// Store scale, and calculate circumfrence
    /// </summary>
    private void Awake()
    {
        Scale = transform.localScale.x/2;
        circumfrence = 2 * Mathf.PI * Scale;
    }
    
    /// <summary>
    /// Rotate the sphere, and spawn hazard rows
    /// </summary>
    void Update () {
        float speed = Mathf.Min(maxSpeed, minSpeed + (Distance/rate));
        Distance += circumfrence * (speed * Time.deltaTime / 360);
        angle += speed * Time.deltaTime;
        transform.Rotate(rotVec * speed * Time.deltaTime);
        if(angle >= spawnAngle)
        {
            hazardSpawner.Spawn();
            angle = 0;
        }
	}
    #endregion

}
