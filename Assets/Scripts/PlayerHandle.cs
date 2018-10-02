using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandle : MonoBehaviour {
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
    
	// Use this for initialization
	void Start () {
        character = GetComponent<CharacterController>();
        z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        MovementCalc();
        if(speedLevel>= maxSpeedLevel)
        {
            Die();
        }

    }

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
    public void Slow() {
        speedLevel++;
        z = z - speedLevelOffset;

    }
    public void Die() {
        Destroy(gameObject);
    }
}
