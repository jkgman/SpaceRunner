using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour {

    public Transform target;
    public Vector3 axis;
    public float speed;

    private void Update()
    {
        transform.RotateAround(target.position, axis, speed*Time.deltaTime);
        
    }
}
