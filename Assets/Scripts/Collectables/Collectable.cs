using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public CollectableType type;

    public enum CollectableType
    {
        Coin,
        SpeedUp,

    }

    public virtual void Pickup()
    {
        ItemManager.instance.AddToInv(this);
        Destroy(gameObject);
    }

    public virtual CollectableType GetColType()
    {
        return type;
    }
}
