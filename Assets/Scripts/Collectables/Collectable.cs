using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Pickup");
            Pickup();
        }
    }

    public virtual void Pickup()
    {
        //SoundManager.Instance.PlaySfx(_cue);
        LevelController.instance.SendConsumeMessage(this);
        Destroy(gameObject);
    }
}
