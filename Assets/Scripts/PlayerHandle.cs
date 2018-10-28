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
    private bool control = false;
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
        if(control)
        {
            moveVector.x = (direction * distance).x * speed;
            if(moveVector.x > -movementDeadZone && moveVector.x < movementDeadZone)
            {
                moveVector.x = 1 * Mathf.Sign(moveVector.x);
            }
            Debug.Log("Start vector:" + moveVector.x);
        }
        
    }
    private void MovementCalc() {
        if(control)
        {
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
            Vector3 move = new Vector3(Mathf.Min(Mathf.Abs(moveVector.x), maxMovePerSecond) * Mathf.Sign(moveVector.x), moveVector.y, moveVector.z) * Time.deltaTime;
            character.Move(move);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
            moveVector.x -= move.x;
            Debug.Log("Updated vector:" + moveVector.x);
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
}
