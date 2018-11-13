using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles movement, dying and such
/// </summary>
public class PlayerHandle : MonoBehaviour, IitemEvents {

    #region Variables
    //TODO: add descriptions for exposed vars
    private CharacterController character;
    private Vector3 moveVector;
    private float z;
    public float speedLevelOffset;
    [Range(1, 10)]
    public int maxSpeedLevel;
    private int speedLevel = 0;
    public float speed;
    public float gravity;
    private float vertVelocity = -1;
    private InputHandle input;
    public float maxMovePerSecond;
    public float movementDeadZone = 1;
    public bool godMode = false;
    private int currentLane = 2;
    private bool control = false;
    private LaneGenerator lane;
    public Animator anim;
    public bool sliding = false;
    public bool jumping = false;
    private LevelController controller;
    #endregion

    #region Singleton
    public static PlayerHandle instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of player found");
            return;
        }
        instance = this;
    }
    #endregion

    #region Implementations
    /// <summary>
    /// get references, set initial z, and subscribes this class to input
    /// </summary>
    void Start () {
        lane = FindObjectOfType<LaneGenerator>();
        character = GetComponent<CharacterController>();
        controller = LevelController.instance;
        input = InputHandle.instance;
        input.onMovement += MovementCalc;
        EventSystemListeners.main.AddListener(gameObject);
    }

    /// <summary>
    /// Calls Movement every frame,
    /// and looks if weve lost too many speed levels to die
    /// </summary>
	void Update()
    {
        if(speedLevel >= maxSpeedLevel)
        {
            Die();
        }
        MovementCalc();
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Gets inputs and moves character accordingly
    /// </summary>
    private void MovementCalc( Vector2 endPos, Vector2 direction, float distance) {
        if(control)
        {
            
            
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if(direction.x > 0)
                {
                    if(currentLane + 1 < lane.CurrentLaneCount)
                    {
                        currentLane++;
                    }
                } else if(direction.x < 0)
                {
                    if(currentLane > 0)
                    {
                        currentLane--;
                    }
                }
            } else
            {
                if(direction.y > 0)
                {
                    StartCoroutine("Jump");
                } else if(direction.y < 0)
                {
                    StartCoroutine("Slide");
                }
            }
        }
    }
    private void MovementCalc() {
        if(control)
        {
            if(moveVector.x > -movementDeadZone && moveVector.x < movementDeadZone)
            {
                moveVector.x = 0;
            }
            moveVector.y = -gravity;
            Vector3 target = new Vector3(lane.LanePositions[currentLane].x, transform.position.y + moveVector.y, transform.position.z);
            var offset = target - transform.position;
            //Get the difference.
            if(offset.magnitude > .1f)
            {
                //If we're further away than .1 unit, move towards the target.
                offset = offset.normalized * speed;
                //normalize it and account for movement speed.
                character.Move(offset * Time.deltaTime * 10);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Activates control inputs and sets the current Z reference
    /// </summary>
    public void ActivateControl() {
        control = true;
        z = transform.position.z;
    }
    public void DeactivateControl() {
        control = false;
    }
    /// <summary>
    /// Adds count to speedlevel and adjusts the locked z
    /// </summary>
    public void Slow() {
        if(!godMode)
        {
            speedLevel++;
            z = z - speedLevelOffset;
        }
    }
    /// <summary>
    /// Destroys game object and call game over sequence
    /// </summary>
    public void Die() {
        if(!godMode)
        {
            controller.FailPlanet();
            Destroy(gameObject);
        }
    }
    #endregion

    #region Coroutines
    //todo: see if i can make this with passing in the animation name and a reference to a bool
    //These both play an animation and set a bool on while doing so
    IEnumerator Slide()
    {
        float time = 0;
        anim.Play("Slide");
        sliding = true;
        while(anim.GetCurrentAnimatorStateInfo(0).length > time)
        {
            time += Time.deltaTime;
            yield return null;
        }
        sliding = false;
    }
    IEnumerator Jump()
    {
        float time = 0;
        anim.Play("Jump");
        jumping = true;
        while(anim.GetCurrentAnimatorStateInfo(0).length > time)
        {
            time += Time.deltaTime;
            yield return null;
        }
        jumping = false;
    }


    #endregion


    //Do the stuff that powerups do
    public void ItemCollected(Collectable.CollectableType type)
    {
        Debug.Log("playerhandle: Message received");
    }
}
