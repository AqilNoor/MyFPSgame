using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    public PlayerMovement playerMovement;


    public float sprintSpeed = 10f;
    public float crouchSpeed = 2f;
    public float moveSpeed = 5f;
    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    private bool isCrouching;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {

        // if LeftShift key is pressed and isCrouching is false
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            //  change the value of speed to sprintSpeed
            playerMovement.speed = sprintSpeed;
        }
         // is LeftShift key is not pressed and isCrpuching is false
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {

            //  change the value of speed to movetSpeed
            playerMovement.speed = moveSpeed;
        }

    } // Sprint

    void Crouch()
    {
        // check if C key is down
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                // if we are crouching -- StandUp
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = moveSpeed;
                isCrouching = false;


            }else
            {
                // if we are not crouching -- Crouch
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
                isCrouching = true;
            }







        }





    }






}
