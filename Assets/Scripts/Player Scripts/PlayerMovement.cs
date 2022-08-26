using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_Controller;
    private Vector3 move_Direction;
    public float speed = 5f;
    private float gravity = 20f;
    public float jump_Force = 10f;
    private float vertical_Velocity;

    private void Awake()
    {
        // storing charactercontroller component
        character_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {    // Axis.HORIZONTAL and Axis.VERTICAL getting from TagHolder Script
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        // move the object according to the origin of object having transform
        move_Direction = transform.TransformDirection(move_Direction);


        move_Direction *= speed * Time.deltaTime;
        ApplyGravity();
        // CharacterController.Move() manage all the movements 
        character_Controller.Move(move_Direction);


    }

    void ApplyGravity()
    {
        // keep the player on ground
        vertical_Velocity -= gravity * Time.deltaTime;
        PlayerJump();

        
        move_Direction.y = vertical_Velocity * Time.deltaTime;

    }

    void PlayerJump()
    {
        // player must be on ground and SPACE key is pressed 
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }
    }
}
