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
                BulletFire();
            }
        } else {
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
                    BulletFire();
                } else
                {

                }
            }
        } 
    }



    void ZoomInAndOut()
    {
        
    }

    public void BulletFire()
    {

    }


















}
