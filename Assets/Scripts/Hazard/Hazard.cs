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
    public bool canSlideUnder = false;
    public bool canJumpOver = false;
    private PlayerHandle player;
    public GameObject model;
    public int Length;
    public bool useOnlyOnce;
    public bool hasPreferedLane;
    public int preferedLane;
    #endregion

    #region Implementations
    private void Start()
    {
        player = PlayerHandle.instance;
        //Change collider for jumping and sliding
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
            if(canSlideUnder && player.Sliding)
            {
                return;
            }
            if(haztype == HazardType.Slow)
            {
                player.Slow();
            } else
            {
                player.Die();
            }
            if(model)
            {
                Destroy(model);
            }
            Destroy(gameObject);
            //or call destroy anim
        }
    }
    #endregion
    public bool Traversible() {
        if(canSlideUnder == true || canJumpOver == true)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
