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
    // for Audio 
    private PlayerFootstepSound playerFootstepSound;
    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float minWalkVolume = 0.2f;
    private float maxWalkVolume = 0.6f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
        lookRoot = transform.GetChild(0);
        playerFootstepSound = GetComponentInChildren<PlayerFootstepSound>();
    }

    private void Start()
    {
        playerFootstepSound.minVolume = minWalkVolume;
        playerFootstepSound.maxVolume = maxWalkVolume;
        playerFootstepSound.stepDistance = walkStepDistance;
    }
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
            playerFootstepSound.stepDistance = sprintStepDistance;
            playerFootstepSound.minVolume = sprintVolume;
            playerFootstepSound.maxVolume = sprintVolume;

        }
         // is LeftShift key is not pressed and isCrpuching is false
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {

            //  change the value of speed to movetSpeed
            playerMovement.speed = moveSpeed;
            playerFootstepSound.stepDistance = walkStepDistance;
            playerFootstepSound.minVolume = minWalkVolume;
            playerFootstepSound.maxVolume = maxWalkVolume;




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
                playerFootstepSound.stepDistance = crouchStepDistance;
                playerFootstepSound.minVolume = crouchVolume;
                playerFootstepSound.maxVolume = crouchVolume;

                isCrouching = true;
            }







        }





    }






}
