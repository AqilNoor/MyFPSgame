using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    private float currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;
    public float patrolRadiusMin = 20f;
    public float patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    private float patrolTimer;
    public float waitBeforeAttack = 2f;
    private float attackTimer;
    private Transform target;
    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }
    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        // when the enemy first get to the player
        // attack right away
        attackTimer = waitBeforeAttack;
        // memorize the value of chase distance
        // so that we can put it back
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if( enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }
     public void Patrol()
    {
        // tell navAgent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;
        // add to the patrol timer
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolForThisTime)
        {
            SetNewRandomDistance();
            patrolTimer = 0f;
        }
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Walk(true);
        }
        else
        {
            enemyAnimator.Walk(false);
        }
        
        // test the distance between Player and Enemy
        if (Vector3.Distance(transform.position, target.position)<= chaseDistance)
        {
            enemyAnimator.Walk(false);enemyState = EnemyState.CHASE;
            // Play Sound
        }



    }
    public void Chase() 
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        // set the player position as destination
        // because Cannible is chasing(running towards) the player
        navAgent.SetDestination(target.position);
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Run(true);
        }
        else
        {
            enemyAnimator.Run(false);
        }
        if(Vector3.Distance(transform.position,target.position) <= attackDistance)
        {
            // stop animation
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.ATTACK;
            // reset the chase distance to previous
            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
            else if(Vector3.Distance(transform.position, target.position) > chaseDistance)
            {
                // stop runnig
                enemyAnimator.Run(false);
                enemyState = EnemyState.PATROL;
                // reset the patrol timmer so that the function can calculate 
                // the new patrol destination right away
                patrolTimer = patrolForThisTime;
                // reset the chase distance
                if ( chaseDistance != currentChaseDistance)
                {
                    chaseDistance = currentChaseDistance;
                }
            }
        }
    }
    public void Attack() { }
    void SetNewRandomDistance() { }








}
