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
    /// <summary>
    /// When collided with player checks its hazard type and calls apropriate response in player class
    /// Then destroys self
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(CanSlideUnder && PlayerHandle.instance.sliding)
            {
                return;
            }
            if(CanJumpOver && PlayerHandle.instance.jumping)
            {
                return;
            }
            if(Haztype == HazardType.Slow)
            {
                PlayerHandle.instance.Slow();
                Destroy(gameObject);
            }
            else if(Haztype == HazardType.Death)
            {
                PlayerHandle.instance.Die();
                Destroy(gameObject);
            }
            
            //TODO: (Mandatory) Make call to levelcontroller for a particle
            //levelcontroller.Poof();
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
