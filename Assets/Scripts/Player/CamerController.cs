using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Follows player based on their distance when game starts
/// </summary>
public class CamerController : MonoBehaviour {
    [Tooltip("Camera offset from player")]
    public Vector3 offset;
    public float _speed;
    private PlayerHandle player;

	void Start () {
        player = FindObjectOfType<PlayerHandle>();
        //offset = transform.position - player.transform.position;
    }
	
	void Update ()
    {

        if (player.jumping)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset - Vector3.forward * 3, _speed * Time.deltaTime);
            transform.LookAt(player.transform);
        } else if(player.sliding)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset + Vector3.down * 2, _speed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        else
        {
            if(player)
            {
                //transform.position = player.transform.position + offset;
                transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, _speed * Time.deltaTime);
                transform.LookAt(player.transform);
            }
        }
    }
}
