using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotator : MonoBehaviour {
    public Transform player;

	
	// Update is called once per frame
	void Update () {
        player.Rotate(Vector3.up);		
	}
}
