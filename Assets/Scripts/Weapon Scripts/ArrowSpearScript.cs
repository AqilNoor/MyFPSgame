using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearScript : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed = 30f;
    public float deactivateTimer = 3f;
    public float damage = 15f;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void Launch(Camera mainCam)
    {
        myBody.velocity = mainCam.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }








}
