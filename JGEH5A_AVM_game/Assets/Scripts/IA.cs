using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] protected Image healthbar;

    // Patrolling
    private Vector3 walkPoint;
    private bool walkPointSet;
    private float walkDistance;

    // Attacking
    private float timeBetweenAttack;
    private bool alreadyAttacked;

    // States
    protected float sightRange;
    protected float attackRange;
    protected bool inSightRange, inAttackRange;

    protected float health;
    protected float maxHealth = 5f;
    protected float damage;

    // Activation
    private float isActivating = 0f;

    // animation IDs
    private int _animIDWalk;

    private bool _hasAnimator;

    private Animator _animator;

    // sound effect
    protected AudioManager _audioManager;

    private bool variantAttack = false;

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

    protected void Start()
    {
        health = 5f;
        damage = 1f;

        walkPoint = transform.position;
        walkDistance = 3f;
        timeBetweenAttack = 3f;
        sightRange = 16f;
        attackRange = 10f;
        inSightRange = false;
        inAttackRange = false;
        AssignAnimationIDs();
    }
    protected void Update()
    {
        if (isActivating > 3f)
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
        else
        {
            isActivating += Time.deltaTime;
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
    protected void Patroling()
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
    protected void ChasePlayer()
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
            if (variantAttack)
            {
                if (this is IA_Boss)
                {
                    for (float i = 0f; i < 10f; i = i + 3f)
                    {
                        StartCoroutine(VariantAttack1(i));
                    }
                }
                Rigidbody rb = Instantiate(projectile, spawnBulletPosition.position, spawnBulletPosition.rotation).GetComponent<Rigidbody>();
                _audioManager.PlaySFX(_audioManager.EnnemyBulletSound);

                rb.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            }
            else
            {
                if (this is IA_Boss)
                {
                    VariantAttack2();
                }
                else
                {
                    for (float i = 0f; i < 10f; i = i + 3f)
                    {
                        StartCoroutine(VariantAttack1(i));
                    }
                }
            }
            variantAttack = !variantAttack;

            alreadyAttacked = true;

            // Start cooldown timer
            StartCoroutine(ResetAttackCoroutine());
        }
    }

    private IEnumerator VariantAttack1(float time) // 3 projectiles.
    {
        yield return new WaitForSeconds(time / 3f);
        Rigidbody rb = Instantiate(projectile, spawnBulletPosition.position, spawnBulletPosition.rotation).GetComponent<Rigidbody>();
        _audioManager.PlaySFX(_audioManager.EnnemyBulletSound);

        rb.AddForce(transform.forward * 1.5f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
    }
    private void VariantAttack2() // 12 projectiles autour de l'ennemi
    {
        int projectileCount = 12;
        float radius = projectileCount / 2f;

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculation of the angle and spawn position 
            float angle = i * (360f / projectileCount) * Mathf.Deg2Rad;
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);

            // Calculation of the direction for each bullet
            Vector3 direction = (spawnPosition - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            var bullet = Instantiate(projectile, spawnPosition, rotation);
            _audioManager.PlaySFX(_audioManager.EnnemyBulletSound);

            // Appliquer la force dans la bonne direction
            bullet.GetComponent<Rigidbody>().AddForce(direction * 1.5f, ForceMode.Impulse);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 0.05f, ForceMode.Impulse);
            bullet.GetComponent<Rigidbody>().AddForce(transform.up * 8f, ForceMode.Impulse);
        }
    }







    private IEnumerator ResetAttackCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        alreadyAttacked = false;
    }


    public void TakeDamage()
    {
        if (this is IA_Boss)
        {
            health -= damage / 15;
        }
        else
        {
            health -= damage;
        }
        healthbar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            _audioManager.PlaySFX(_audioManager.deathRobot);
            ZoneAccessManager.Instance.RemoveEnnemy(gameObject);
            Destroy(gameObject);
        }


    }
}
