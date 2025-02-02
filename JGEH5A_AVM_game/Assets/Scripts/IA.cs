using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Image healthbar;

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
    private float maxHealth = 5f;
    private float damage;

    // animation IDs
    private int _animIDWalk;

    private bool _hasAnimator;

    private Animator _animator;

    // sound effect
    private AudioManager _audioManager;

    private void AssignAnimationIDs()
    {
        _animIDWalk = Animator.StringToHash("Walk_Anim");

    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _hasAnimator = TryGetComponent(out _animator);
    }

    private void Start()
    {
        health = 5f;
        damage = 1f;

        walkPoint = transform.position;
        walkDistance = 5f;
        timeBetweenAttack = 5f;
        sightRange = 15f;
        attackRange = 8f;
        inSightRange = false;
        inAttackRange = false;
        AssignAnimationIDs();
    }
    private void Update()
    {
        // Check for sight and attack
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (!inSightRange && !inAttackRange)
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
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDWalk, true);
            }
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDWalk, false);
            }
        }

    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDWalk, true);
        }
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDWalk, false);
        }

        if (!alreadyAttacked)
        {
            // Attack code here
            Rigidbody rb = Instantiate(projectile, spawnBulletPosition.position, spawnBulletPosition.rotation).GetComponent<Rigidbody>();
            _audioManager.PlaySFX(_audioManager.EnnemyBulletSound);

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
        healthbar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
