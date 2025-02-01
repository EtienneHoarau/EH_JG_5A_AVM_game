using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class IA : MonoBehaviour
{
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnBulletPosition;

    // Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;
    private float walkDistance;

    // Attacking
    private float timeBetweenAttack;
    private bool alreadyAttacked;

    // States
    private float sightRange;
    private float attackRange;
    private bool inSightRange, inAttackRange;

    private float health;
    private float damage;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        health = 5;
        damage = 1;

        walkPoint = transform.position;
        walkDistance = 5f;
        timeBetweenAttack = 5f;
        sightRange = 15f;
        attackRange = 8f;
        inSightRange = false;
        inAttackRange = false;
    }
    private void Update()
    {
        // Check for sight and attack
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!inSightRange && !inAttackRange)
        {
            Patroling();
        }
        if (inSightRange && !inAttackRange)
        {
            ChasePlayer();
        }
        if (inSightRange && inAttackRange)
        {
            AttackPlayer();
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkDistance, walkDistance);

        float randomX = Random.Range(-walkDistance, walkDistance);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, transform.up, 2f, GroundLayer))
        {
            walkPointSet = true;
        }
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code here
            Rigidbody rb = Instantiate(projectile, spawnBulletPosition.position, spawnBulletPosition.rotation).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;

            // Start cooldown timer
            StartCoroutine(ResetAttackCoroutine());
        }
    }

    private IEnumerator ResetAttackCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        alreadyAttacked = false;
    }


    public void TakeDamage()
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
