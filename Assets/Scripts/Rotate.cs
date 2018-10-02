using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public Vector3 rotVec;
    public HazardSpawner haz;
	public float scale;
    private float angle;
    [Range(0,360)]
    public float spawnAngle;
    private float circumfrence;
    public float distance;
    public float rate;
    [Range(0, 360)]
    public float maxSpeed;
    [Range(0, 360)]
    public float minSpeed;
    private void Awake()
    {
        scale = transform.localScale.x/2;
        circumfrence = 2 * Mathf.PI * scale;
    }
    // Update is called once per frame
    void Update () {
        float speed = Mathf.Min(maxSpeed, minSpeed + (distance/rate));
        distance += circumfrence * (speed * Time.deltaTime / 360);
        angle += speed * Time.deltaTime;
        transform.Rotate(rotVec * speed * Time.deltaTime);
        if(angle >= spawnAngle)
        {
            haz.Spawn();
            angle = 0;
        }
	}
}
