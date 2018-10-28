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
    private float vertVelocity;
    private InputHandle input;
    public float maxMovePerSecond;
    public float movementDeadZone = 1;
    public bool godMode = false;
    public Vector3[] lanePoints;
    private int currentLane = 2;
    #endregion

    #region Implementations
    /// <summary>
    /// get references, and set initial z
    /// </summary>
    void Start () {
        character = GetComponent<CharacterController>();
        z = transform.position.z;
        input = InputHandle.instance;
        input.onMovement += MovementCalc;
    }

    /// <summary>
    /// Calls Movement every frame, 
    /// and looks if weve lost too many speed levels to die
    /// </summary>
	void Update () {
        if(speedLevel>= maxSpeedLevel)
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

        //gonna clean and move this later to inputhandle and only pass the type of swipe ie. left, up etc.
        if( Mathf.Abs(direction.x )> Mathf.Abs(direction.y)) { 
            if (direction.x > 0 )
            {
                if (currentLane+1 < lanePoints.Length)
                {
                    currentLane++;
                }
            } else if(direction.x < 0)
            {
                if (currentLane > 0)
                {
                    currentLane--;
                }
            }
        } else
        {
            if (direction.y > 0)
            {
                
                Debug.Log("Up");
            }
            else if (direction.y < 0)
            {
                Debug.Log("Down");
            }
        }

        //if (moveVector.x > -movementDeadZone && moveVector.x < movementDeadZone)
        //{
        //    moveVector.x = 1 * Mathf.Sign(moveVector.x);
        //}
        //Debug.Log("Start vector:" + moveVector.x);
    }
    private void MovementCalc() {
        if(character.isGrounded)
        {
            vertVelocity = -.5f;
        } else
        {
            vertVelocity -= gravity * Time.deltaTime;
        }
        if(moveVector.x > -movementDeadZone && moveVector.x < movementDeadZone)
        {
            moveVector.x = 0;
        }

        moveVector.y = vertVelocity;
        //Vector3 move = new Vector3(Mathf.Min(Mathf.Abs(moveVector.x), maxMovePerSecond) * Mathf.Sign(moveVector.x), moveVector.y, moveVector.z) * Time.deltaTime;


        Vector3 target = new Vector3( lanePoints[currentLane].x,moveVector.y, transform.position.z) ;
        var offset = target - transform.position ;
        //Get the difference.
        if (offset.magnitude > .1f)
        {
            //If we're further away than .1 unit, move towards the target.
            offset = offset.normalized * speed ;
            //normalize it and account for movement speed.
            character.Move(offset * Time.deltaTime * 10);
        }


        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        //moveVector.x -= move.x;
        //Debug.Log("Updated vector:" + moveVector.x);

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
}
