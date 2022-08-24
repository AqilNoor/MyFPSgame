using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELFAIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource shootSound, reloadSound;
    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject attackPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        animator.SetTrigger(AnimationTag.sHOOTtRIGGER);
    }

    public void Aim(bool canAim)
    {
        animator.SetBool(AnimationTag.aIMpARAMETER, canAim);
    }
    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shootSound.Play();
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(true);
        }
       
    }


}
