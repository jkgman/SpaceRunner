using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Magnet : Collectable {
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Magnet");
            Pickup();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Magnet");
            Pickup();
        }
    }


    public override void Pickup()
    {
        //SoundManager.Instance.PlaySfx(_cue);
        // OR listen with player and do stuff then 

        base.Pickup();
    }


}
