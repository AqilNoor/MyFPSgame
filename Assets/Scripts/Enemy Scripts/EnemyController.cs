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
    public GameObject attackPoint;

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
        // patrolForThisTime means the new point from where Enemy starts patrolling
        if (patrolTimer > patrolForThisTime)
        {
            // inside this function enemy gets new point inside the Terrain 
            // when enemy reaches to the Boundary of the Terrain
            SetNewRandomDistance();

            // at new position patrolTimer set to be zero
            patrolTimer = 0f;
        }
        // Enemy is moving
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            // play walk animation
            enemyAnimator.Walk(true);
        }
        else
        {
            // if not moving, stop walk animation
            enemyAnimator.Walk(false);
        }
        
        // test the distance between Player and Enemy
        if (Vector3.Distance(transform.position, target.position)<= chaseDistance)
        {
            // stop walk animation
            enemyAnimator.Walk(false);
            // chane enemy state to chase so that enemy will run
            enemyState = EnemyState.CHASE;
            // Play Sound
        }



    }
    public void Chase() 
    {
        // Enable the Enemy to Move Again
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        // set the player position as destination
        // because Cannible is chasing(running towards) the player
        navAgent.SetDestination(target.position);
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            // play run animation
            enemyAnimator.Run(true);
        }
        else
        {
            // otherwise stop running
            enemyAnimator.Run(false);
        }
        if(Vector3.Distance(transform.position,target.position) <= attackDistance)
        {
            // stop run and walk animation
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            // change enemy state to attack
            enemyState = EnemyState.ATTACK;
            // reset the chase distance to previous
            // if the distance between player and enemy is higher and player gonna shoot enemy
            // enemy will notice it and will start chasing the player

            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
                // if player run away from enemy
            else if(Vector3.Distance(transform.position, target.position) > chaseDistance)
            {
                // then enemy will stop runnig
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
    public void Attack()
    {
       // before attack enemy will stop moving 
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attackTimer += Time.deltaTime;
        if (attackTimer > waitBeforeAttack)
        {
            enemyAnimator.Attack();
            // Reset attack timer otherwise enemy will attack again and again
            attackTimer = 0f;
            // play sound
            if( Vector3.Distance(transform.position,target.position)> attackDistance + chaseAfterAttackDistance)
            {
                enemyState = EnemyState.CHASE;
            }
        }
    }
    void SetNewRandomDistance()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDirection = Random.insideUnitSphere * randRadius;
        randDirection += transform.position;
        // to keep the Enemy onto the surface of Terrain
        NavMeshHit navhit;
        NavMesh.SamplePosition(randDirection, out navhit, randRadius, -1);
        navAgent.SetDestination(navhit.position);

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

    public EnemyState EnemyState
    {
        get;set;
    }




}
