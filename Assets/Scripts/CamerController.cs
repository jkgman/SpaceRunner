using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Follows player based on their distance when game starts
/// </summary>
public class CamerController : MonoBehaviour {

    //TODO: make this way more dynamic RIP

    PlayerHandle player;
    Vector3 offset;

	void Start () {
        player = FindObjectOfType<PlayerHandle>();
        offset = transform.position - player.transform.position;
    }
	
	void Update () {
        if(player)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
