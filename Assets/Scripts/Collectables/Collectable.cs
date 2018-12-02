using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Collectable : MonoBehaviour {

    #region public variables
    public CollectableType type;
    public Sprite UiTexture;
    //public AudioClip _cue;

    #endregion

    /// <summary>
    /// Type for collectables
    /// </summary>
    public enum CollectableType
    {
        Coin,
        SlowDown,
        Magnet,
        Invincibility,
        Refresh,
        Resurrect,
        Shield,
        Heart,
        Gem,
        Battery

    }

    public virtual void Pickup()
    {
        //SoundManager.Instance.PlaySfx(_cue);
        LevelController.instance.SendConsumeMessage(this);
        Destroy(gameObject);
    }
}
