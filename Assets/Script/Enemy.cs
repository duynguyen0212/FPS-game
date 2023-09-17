using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float chaseRange = 10f; // Range at which the enemy starts chasing the player.
    public float attackRange = 4f; // Range at which the enemy attacks the player.
    public float patrolSpeed = 2f; // Patrol movement speed.
    public float chaseSpeed = 5f; // Chase movement speed.

    private Transform player; // Reference to the player's transform.
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    public Animator enemyAni;
    private bool attackAni;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else if(distanceToPlayer <= attackRange && attackAni == false){
                AttackPlayer();
        }
        else{
            enemyAni.SetBool("Walking", false);
        }
        
    }

    void ChasePlayer()
    {
        // Set the player's position as the destination for chasing.
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);
        enemyAni.SetBool("Walking", true);
       
    }

    void AttackPlayer()
    {
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo(){
        Debug.Log("attack");
        attackAni = true;
        enemyAni.SetBool("Walking", false);
        enemyAni.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        enemyAni.SetBool("Attacking", false);
        attackAni = false;
    }
}
