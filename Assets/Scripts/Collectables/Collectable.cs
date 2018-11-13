using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    #region public variables
    public CollectableType type;
    public Texture2D UiTexture;
    public AudioClip _cue;

    #endregion

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
        LevelController.instance.SendCollectionMessage(type);
        Destroy(gameObject);
    }

    public virtual CollectableType GetColType()
    {
        return type;
    }
}
