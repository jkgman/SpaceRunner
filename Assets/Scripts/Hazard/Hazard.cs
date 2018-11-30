using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// How the hazard will interact with the player
/// </summary>
public enum HazardType { Death, Slow, Collectible }
/// <summary>
/// Holds data for how the hazard will spawn and interact with player
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Hazard : MonoBehaviour {

    #region Variables
    [SerializeField]
    private HazardType haztype;
    [SerializeField, Tooltip("Can the player slide under this hazard")]
    private bool canSlideUnder = false;
    [SerializeField, Tooltip("Can the player jump over this hazard")]
    private bool canJumpOver = false;
    [SerializeField, Tooltip("If hasPreferedLane it will pull the lane number here")]
    private int lane;
    private PlayerHandle player;

    #region Getters and Setters
    public HazardType Haztype
    {
        get {
            return haztype;
        }
    }
    public bool CanSlideUnder
    {
        get {
            return canSlideUnder;
        }
        private set {
            canJumpOver = value;
        }
    }
    public bool CanJumpOver
    {
        get {
            return canJumpOver;
        }
        private set {
            canJumpOver = value;
        }
    }

    public int Lane
    {
        get {
            return lane;
        }
    }

    #endregion

    #endregion

    #region Implementations
    private void Start()
    {
        player = PlayerHandle.instance;
        //TODO: (Idea) Change collider based on lane width to appropriatly contain
    }

    /// <summary>
    /// When collided with player checks its hazard type and calls apropriate response in player class
    /// Then destroys self
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(CanSlideUnder && player.sliding)
            {
                return;
            }
            if(CanJumpOver && player.jumping)
            {
                return;
            }
            if(Haztype == HazardType.Slow)
            {
                player.Slow();
            } else
            {
                player.Die();
            }
            Destroy(gameObject);
            //TODO: (Mandatory) Make call to destroy animation when we have them and make anim variable
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Returns if the hazard can be traversed by jumping or sliding
    /// </summary>
    /// <returns>True if traversible</returns>
    public bool Traversible() {
        if(CanSlideUnder == true || CanJumpOver == true)
        {
            return true;
        } else
        {
            return false;
        }
    }
    #endregion
}
