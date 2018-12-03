using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceToSleep : MonoBehaviour {
    [Range(0, 100)]
    public int chanceToSleep;

	// Use this for initialization
	void Start () {
        if((Random.Range(0,100) >= chanceToSleep))
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
