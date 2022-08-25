using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] footStepClip;
    private CharacterController characterController;
    [HideInInspector]
    public float minVolume, maxVolume;
    [HideInInspector]
    public float totalDistanceCovered;
    [HideInInspector]
    public float stepDistance; // stepDistance = walkStepDistance = 0.4f

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // CharacterController is attached with Player
        characterController = GetComponentInParent<CharacterController>();
    }
 
    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
        
    }

    void CheckToPlayFootstepSound()
    {
        // if Player is not moving
        if (!characterController.isGrounded)
            return;

        // if Player is Moving
        if (characterController.velocity.sqrMagnitude > 0)
        {
            // accumulate the total step-distance 
            totalDistanceCovered += Time.deltaTime;
             
             
            if (totalDistanceCovered > stepDistance)
            { 
                audioSource.volume = Random.Range(minVolume, maxVolume);
                audioSource.clip =  footStepClip[Random.Range(0, footStepClip.Length)];
                audioSource.Play();

                // after playing audio setting value 0 to totalDistanceCovered
                totalDistanceCovered = 0f;
            }
        } else
        {
            totalDistanceCovered = 0f;
        }
    }





}
