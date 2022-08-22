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
    public float accumulatedDistance;
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
        if (!characterController.isGrounded)
            return;
        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                audioSource.volume = Random.Range(minVolume, maxVolume);
                audioSource.clip =  footStepClip[Random.Range(0, footStepClip.Length)];
                audioSource.Play();
                accumulatedDistance = 0f;
            }
        } else
        {
            accumulatedDistance = 0f;
        }
    }





}
