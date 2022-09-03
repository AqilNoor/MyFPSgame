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
    private PlayerStats playerStats;
    private float sprintValue = 100f;
    private float sprintTreshold = 10f;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
        playerFootstepSound = GetComponentInChildren<PlayerFootstepSound>();
        playerStats = GetComponent<PlayerStats>();
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
        // if we have stamina , we can sprint
        if (sprintValue > 0f)
        {    // if LeftShift key is pressed and isCrouching is false
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                //  change the value of speed to sprintSpeed
                playerMovement.speed = sprintSpeed;

                // Sound effects while sprinting
                playerFootstepSound.stepDistance = sprintStepDistance;
                playerFootstepSound.minVolume = sprintVolume;
                playerFootstepSound.maxVolume = sprintVolume;

            }
        }

        // is LeftShift key is not pressed and isCrpuching is false
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {

            //  change the value of speed to movetSpeed
            playerMovement.speed = moveSpeed;

            // Sound effects while not sprinting
            playerFootstepSound.stepDistance = walkStepDistance;
            playerFootstepSound.minVolume = minWalkVolume;
            playerFootstepSound.maxVolume = maxWalkVolume;

             }
        if (Input.GetKey(KeyCode.LeftShift) && (!isCrouching))
        {
            sprintValue -= sprintTreshold * Time.deltaTime;
            if (sprintValue <= 0f)
            {
                sprintValue = 0f;

                //  change the value of speed to movetSpeed
                playerMovement.speed = moveSpeed;

                // Sound effects while not sprinting
                playerFootstepSound.stepDistance = walkStepDistance;
                playerFootstepSound.minVolume = minWalkVolume;
                playerFootstepSound.maxVolume = maxWalkVolume;
            }
            playerStats.DisplayStaminaStats(sprintValue);
        }
        else
        {
            if(sprintValue != 100f)
            {
                sprintValue += (sprintTreshold / 2f) * Time.deltaTime;
                playerStats.DisplayStaminaStats(sprintValue);
                if (sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
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

                // Sounde ffects when not Crouching 
                playerFootstepSound.stepDistance = walkStepDistance;
                playerFootstepSound.minVolume = minWalkVolume;
                playerFootstepSound.maxVolume = maxWalkVolume;
                isCrouching = false;


            }
            else
            {
                // if we are not crouching -- Crouch
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;

                // Sound effects while crouching
                playerFootstepSound.stepDistance = crouchStepDistance;
                playerFootstepSound.minVolume = crouchVolume;
                playerFootstepSound.maxVolume = crouchVolume;

                isCrouching = true;
            }

        }
    }
}
