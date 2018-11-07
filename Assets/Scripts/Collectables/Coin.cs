using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Coin : Collectable {

    //public new CollectableType type = CollectableType.Coin;
    private void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Pickup();
        }

    }


    public override void Pickup()
    {
        //item specific logic

        base.Pickup();
    }

}
