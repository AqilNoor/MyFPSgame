using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool canUnlock = true;

    [SerializeField]
    private float senstivity = 5f;

    [SerializeField]
    private int smoothSteps = 10;

    [SerializeField]
    private float smoothWeight = 0.4f;

    [SerializeField]
    private float rollAngle = 10f;

    [SerializeField]
    private float rollspeed = 3f;

    [SerializeField]
    private Vector2 defaultLookLimit = new Vector2(-70f, 80f);

    private Vector2 lookAngles;

    private Vector2 currentMouseLook;

    private Vector2 smoothMove;

    private float currenRollAngle;

    private int lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

   

    }
    void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));
        lookAngles.x += currentMouseLook.x * senstivity * (invert ? 1f : -1f);
        lookAngles.y = currentMouseLook.y * senstivity;
        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimit.x, defaultLookLimit.y);

        currenRollAngle = Mathf.Lerp(currenRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * rollAngle, Time.deltaTime * rollspeed);

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0, 0);
        playerRoot.localRotation = Quaternion.Euler(0, lookAngles.y, 0);

    }

























}
