using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour {
    PlayerHandle player;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerHandle>();
        offset = transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if(player)
        {
            transform.position = player.transform.position + offset;
        }
		
    }
}
