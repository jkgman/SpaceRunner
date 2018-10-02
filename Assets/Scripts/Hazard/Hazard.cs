using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HazardType { Death, Slow }
public class Hazard : MonoBehaviour {
    public HazardType haztype;
    public PlayerHandle player;
    private float spawnDist;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(haztype == HazardType.Slow)
            {
                player.Slow();
            } else
            {
                player.Die();
                
            }
            Destroy(gameObject);
        }
        
    }
}
