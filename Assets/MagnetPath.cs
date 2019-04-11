using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPath : MonoBehaviour {
    public GameObject emitter, target;

    public float RotationSpeed;
    public float scaleX;
    public float objectX;
    private Quaternion startRot;

    // Use this for initialization
    void Start () {
        startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if ( target != null & emitter != null) { 
            transform.position = (target.transform.position + emitter.transform.position) /2;

            Vector3 _dir = (target.transform.position - emitter.transform.position).normalized;
            Quaternion _lookRot = Quaternion.LookRotation(_dir);
            transform.rotation = Quaternion.Slerp(transform.rotation , _lookRot * startRot, Time.deltaTime * RotationSpeed);
            scaleX = Mathf.Abs( Vector3.Distance(target.transform.position, emitter.transform.position)) / objectX;
            transform.localScale = new Vector3( scaleX , 1, 1);
        }

        //Could be destroyed here once the length between objects become miniscule
        if (scaleX < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
