using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;
    public float health = 100f;
    public bool isPlayer, isCannible, isBoar;
    private bool isDead;
    void Awake()
    {
        if (isBoar || isCannible)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            // get enemy Audio
        }
        if (isPlayer)
        {

        }
    }
    public void ApplyDamage(float damage)
    {
        // if we died dont execute rest of the Code
        if (isDead)
            return;

        health -= damage;

        if (isPlayer)
        {
            // show the state(display the health UI value

        }
        
        if (isCannible || isBoar)
        {
            // if enemy is patrolling far away from player and player shoot 
            if (enemyController.EnemyState == EnemyState.PATROL)
            {
                // enemy will notic the shoot and chase the player 
                enemyController.chaseDistance = 50f;
            }
        }

        if (health <= 0f)
        {
            PlayerDied();
            isDead = true;
        }

    }

    void PlayerDied()
    {
        if (isCannible)
        {
            // cannible does not have dead animation
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            // AddTurque() applys angular force to gameObject
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnimator.enabled = false;
            // start coroutine
            // EnemyManager spawn more enemies
        }

        if (isBoar)
        {
            // Boar has a dead animation
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemyController.enabled = false;
            enemyAnimator.Dead();
            // Spawn more enemies
        }

        if (isPlayer)
        {
            // when player is died all enemies will be called inside enemies[]
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                //every kind of action of the enemies will be disabled
                enemies[i].GetComponent<EnemyController>().enabled = false;
                // call Enemy Manager to stop spawning enemies
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        // when player is died, Restart the game
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            // when the any enemy will die, it will deactivate 
            Invoke("TurnOffGame", 3f);
        }
    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FPS Level1");

    }
    void TurnOffGame()
    {
        gameObject.SetActive(false);
    }
}
