using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeliAi : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool isHostile;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake(){
        player = GameObject.Find("XR Origin").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange);
        //if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(agent.isActiveAndEnabled){
            if(playerInSightRange && isHostile){
                if(playerInAttackRange) AttackPlayer();
                else ChasePlayer();
            }else Patrolling();
        }

    }

    private void Patrolling(){
        if(!walkPointSet) SearchWalkPoint();
        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint(){
        //calculate random point in range
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        float randomZ = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up ,2f,whatIsGround)){
            walkPointSet = true;
        }
    }
    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }
    private void AttackPlayer(){
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if(!alreadyAttacked){
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
        Debug.Log("attacked!");
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }
}
