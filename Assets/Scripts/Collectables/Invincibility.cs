using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Invincibility : Collectable {

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

        base.Pickup();

    }
}
