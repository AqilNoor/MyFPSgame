using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        animator.SetBool(AnimationStrings.WALK_PARAMETER, walk);
    }

    public void Run(bool run)
    {
        animator.SetBool(AnimationStrings.RUN_PARAMETER, run);
    }

    public void Attack()
    {
        animator.SetTrigger(AnimationStrings.ATTACK_TRIGGER);
    }

    public void Dead()
    {
        animator.SetTrigger(AnimationStrings.DEAD_TRIGGER);
    }



}
