using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public CollectableType type;
    public AudioClip _cue;

    public enum CollectableType
    {
        Coin,
        SlowDown,
        Magnet,
        Invincibility

    }

    public virtual void Pickup()
    {
        //SoundManager.Instance.PlaySfx(_cue);
        ItemManager.instance.SendCollectionMessage(type);
        Destroy(gameObject);
    }

    public virtual CollectableType GetColType()
    {
        return type;
    }
}
