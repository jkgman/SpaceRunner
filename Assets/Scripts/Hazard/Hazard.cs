using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// What type of hazard
/// </summary>
public enum HazardType { Death, Slow }
/// <summary>
/// When collided with player effects them according to hazard type
/// </summary>
public class Hazard : MonoBehaviour {

    #region Variables
    [SerializeField]
    private HazardType haztype;
    private PlayerHandle player;
    private float spawnDist;
    #endregion

    #region Getters and Setters
    public PlayerHandle Player
    {
        private get {
            return player;
        }

        set {
            player = value;
        }
    }
    #endregion

    #region Implementations
    /// <summary>
    /// When collided with player checks its hazard type and calls apropriate response in player class
    /// Then destroys self
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(haztype == HazardType.Slow)
            {
                Player.Slow();
            } else
            {
                Player.Die();
            }
            Destroy(gameObject);
        }
    }
    #endregion

}
