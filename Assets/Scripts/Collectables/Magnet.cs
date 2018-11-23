using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Magnet : Collectable {

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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
