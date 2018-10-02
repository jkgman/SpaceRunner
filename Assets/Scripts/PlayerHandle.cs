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
    #endregion

    #region Implementations
    /// <summary>
    /// get references, and set initial z
    /// </summary>
    void Start () {
        character = GetComponent<CharacterController>();
        z = transform.position.z;
	}

    /// <summary>
    /// Calls Movement every frame, 
    /// and looks if weve lost too many speed levels to die
    /// </summary>
	void Update () {
        MovementCalc();
        if(speedLevel>= maxSpeedLevel)
        {
            Die();
        }

    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Gets inputs and moves character accordingly
    /// </summary>
    private void MovementCalc() {
        moveVector = Vector3.zero;

        if(character.isGrounded)
        {
            vertVelocity = -.5f;
        } else
        {
            vertVelocity -= gravity * Time.deltaTime;
        }

        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        moveVector.y = vertVelocity;

        character.Move(moveVector * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// adds count to speedlevel and adjusts the locked z
    /// </summary>
    public void Slow() {
        speedLevel++;
        z = z - speedLevelOffset;
    }
    /// <summary>
    /// Destroys game object and call game over sequence
    /// </summary>
    public void Die() {
        Destroy(gameObject);
    }
    #endregion
}
