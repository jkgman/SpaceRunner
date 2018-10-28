using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles movement, dying and such
/// </summary>
public class PlayerHandle : MonoBehaviour {

    #region Variables
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
    public bool Sliding = false;
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


    public void ActivateControl(){
        control = true;
        z = transform.position.z;
    }

    #region Implementations
    /// <summary>
    /// get references, and set initial z
    /// </summary>
    void Start () {
        lane = FindObjectOfType<LaneGenerator>();
        character = GetComponent<CharacterController>();
        input = InputHandle.instance;
        input.onMovement += MovementCalc;
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
    private void MovementCalc(Vector2 endPos, Vector2 direction, float distance) {
        //moveVector.x = (direction * distance).x * speed;
        if(control)
        {


            //gonna clean and move this later to inputhandle and only pass the type of swipe ie. left, up etc.
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if(direction.x > 0)
                {
                    if(currentLane + 1 < lane.currentLaneCount)
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

                    Debug.Log("Up");
                } else if(direction.y < 0)
                {
                    StartCoroutine("Slide");
                }
            }

            //if (moveVector.x > -movementDeadZone && moveVector.x < movementDeadZone)
            //{
            //    moveVector.x = 1 * Mathf.Sign(moveVector.x);
            //}
            //Debug.Log("Start vector:" + moveVector.x);
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
    /// adds count to speedlevel and adjusts the locked z
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
            Destroy(gameObject);
        }
    }
    #endregion


    IEnumerator Slide()
    {
        float time = 0;
        anim.Play("Slide");
        Sliding = true;
        while(!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            yield return null;
        }
    }
}
