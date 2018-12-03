using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : MonoBehaviour {
    public Transform target;
    float timeElapsed;
    float length;
	// Use this for initialization
	void Start () {
        length = 2;
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target.position, timeElapsed / length);
	}
}
