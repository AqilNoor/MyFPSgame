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
    [SerializeField]
    private GameObject arrowPrefab, spearPrefab;
    [SerializeField]
    private Transform arrowBowStartPoint;


    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        // though script is attached with player and first we are getting its child 
        // with Tag Look Root and then we are getting Look Root's child with tag Zoom Camera. 
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
                BulletFire();
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
                    BulletFire();
                } else
                {
                    // if we have Bow or Spear
                    if (isAiming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if(weaponManager.GetCurrentSelectedWeapon().bulletType==WeaponBulletType.ARROW)
                        {
                            // throw Arrow
                            ThrowArrowSpear(true);
                        } else if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            // throw Spear
                            ThrowArrowSpear(false);
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
            //  we press and hold right button of mouse 
            if (Input.GetMouseButtonDown(1))
            {
                // zooming in
                zoomCameraAnimator.Play(AnimationStrings.ZOOM_IN_ANIMATION_STATE);
                // Crosshair deactivating
                crosshair.SetActive(false);
            }
            // release right button of mouse
            if (Input.GetMouseButtonUp(1))
            {
                // Zooming Out
                zoomCameraAnimator.Play(AnimationStrings.ZOOM_OUT_ANIMATION_STATE);
                // activating Crosshair
                crosshair.SetActive(true);
            }
        }
        // for bow and spear with tag selfAim
        if(weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELFAIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                // activating Aim state with bool parameter
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                isAiming = true;
                
            }
            if (Input.GetMouseButtonUp(1))
            {
                // Deactivating Aim state with bool parameter
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                isAiming = false;

            }
        }// selfAIM

    }//ZoomInAndOut
     void ThrowArrowSpear(bool throwArrow)
    {
        if (throwArrow)
        {

            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = arrowBowStartPoint.position;
            arrow.GetComponent<ArrowSpearScript>().Launch(mainCamera);
        }else
        {
            GameObject spear = Instantiate(spearPrefab);
            spear.transform.position = arrowBowStartPoint.position;
            spear.GetComponent<ArrowSpearScript>().Launch(mainCamera);
        }
    }
    void BulletFire()
    {
        RaycastHit hit;
        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit);
        if(hit.transform.tag == Tags.ENEMY_TAG)
        {
            hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }

}
