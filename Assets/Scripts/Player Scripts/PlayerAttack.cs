using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;
    private float nextTimeToShoot;
    public float fireRate = 15f;
    public float damage = 20f;
    private Animator zoomCameraAnimator;
    private bool zoomed;
    private Camera mainCamera;
    private GameObject crosshair;
    private bool isAiming;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        zoomCameraAnimator = transform.Find(Tags.lOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCamera = Camera.main;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        // if we have AssualtRifle
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // if we press and hold Left Mouse button AND Time is greater than nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToShoot)
            {
                nextTimeToShoot = Time.time + 1f / fireRate;
                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
               // BulletFire();
            }
        } else
        {
            // if we have a weapon that shoots once
            if (Input.GetMouseButtonDown(0))
            {
                // attack with Axe
                if (weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // attack with shoot
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                   // BulletFire();
                } else
                {
                    // if we have Bow or Spear
                    if (isAiming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if(weaponManager.GetCurrentSelectedWeapon().bulletType==WeaponBulletType.ARROW)
                        {
                            // throw Arrow
                        } else if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            // thow Spear
                        }
                    }

                }
            }
        } 
    }



    void ZoomInAndOut()
    {
        //we are going to aim with Camera on weapon
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {

            if (Input.GetMouseButtonDown(1))
            {

                zoomCameraAnimator.Play(AnimationTag.ZOOM_IN_ANIMATION);
                crosshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnimator.Play(AnimationTag.ZOOM_OUT_ANIMATION);
                crosshair.SetActive(true);
            }
        }
        if(weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELFAIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                isAiming = true;
                
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                isAiming = false;

            }
        }// selfAIM

    }//ZoomInAndOut

}
