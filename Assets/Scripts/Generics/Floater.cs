using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {
    public Vector3 amplitude;
    public Vector3 frequency;
    private Vector3 start;
	// Use this for initialization
	void Start () {
        start = transform.position;
        //amp sin(frequency t)
        if(frequency.x ==0)
        {
            frequency.x = 1;
        }
        if(frequency.y == 0)
        {
            frequency.y = 1;
        }
        if(frequency.z == 0)
        {
            frequency.z = 1;
        }

    }

    // Update is called once per frame
    void Update () {
        transform.position = transform.position + new Vector3(amplitude.x * Mathf.Sin(1/frequency.x * Time.time), amplitude.y * Mathf.Sin(1/frequency.y * Time.time), amplitude.z * Mathf.Sin(1/frequency.z * Time.time));
	}
}
