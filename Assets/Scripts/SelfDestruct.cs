using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    private float time;
    public float DestructInSec;
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time>=DestructInSec)
        {
            Destroy(gameObject);
        }
	}
}
