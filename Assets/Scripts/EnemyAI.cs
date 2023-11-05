using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public float maxHealth = 100;

    EnemyHealthBar healthBar;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

private void AttackPlayer()
{
    transform.LookAt(player);

    if (!alreadyAttacked)
    {
        // Instantiate the projectile and get its Rigidbody
        GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();

        // Ignore collision between the projectile and the enemy collider
        Collider enemyCollider = GetComponent<Collider>();
        Collider projectileCollider = projectileInstance.GetComponent<Collider>();
        if (enemyCollider != null && projectileCollider != null)
        {
            Physics.IgnoreCollision(enemyCollider, projectileCollider);
        }

        // Calculate the direction from the enemy to the player
        // Aim slightly downwards by subtracting a small vector pointing down from the direction
        Vector3 directionToPlayer = (player.position - transform.position).normalized - new Vector3(0, 0.1f, 0);

        // Apply forces to the projectile
        rb.AddForce(directionToPlayer * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        // Destroy the projectile after some time to prevent cluttering the scene
        Destroy(projectileInstance, 5f); // Adjust the time as needed

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
}

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health);

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
