using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromList : MonoBehaviour {
    public GameObject[] spawnables;
    
	// Use this for initialization
	void Start () {
        GameObject obj = Instantiate(spawnables[Random.Range(0, spawnables.Length)]);
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        Destroy(this);
    }
}
