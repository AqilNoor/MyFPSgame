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
    public float stepDistance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }
    void Start()
    {
        
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

            totalDistanceCovered += Time.deltaTime;

            if (totalDistanceCovered > stepDistance)
            { 
                audioSource.volume = Random.Range(minVolume, maxVolume);
                audioSource.clip =  footStepClip[Random.Range(0, footStepClip.Length)];
                audioSource.Play();
                totalDistanceCovered = 0f;
            }
        } else
        {
            totalDistanceCovered = 0f;
        }
    }





}
