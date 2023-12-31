using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
 
public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 3;
    public Slider healthbar;

    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;
 
    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 3.8f;
    [SerializeField] float aggroRange = 20f;

    [Header("Key Drop")]
    [SerializeField] GameObject doorKeyPrefab;
 
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    private float speedAfterDifficulty;
    private float baseSpeed;
 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        baseSpeed = agent.speed;
        speedAfterDifficulty = baseSpeed * DifficultyManager.SpeedMultiplier;
        agent.speed = speedAfterDifficulty;
        Debug.Log($"Base Speed: {baseSpeed}, Multiplier: {DifficultyManager.SpeedMultiplier}, Adjusted Speed: {speedAfterDifficulty}");
    }
 
    // Update is called once per frame
    void Update()
    {

        healthbar.value = health;
        animator.SetFloat("speed", agent.velocity.magnitude / speedAfterDifficulty);
 
        if (player == null)
        {
            return;
        }
 
        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;
 
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);

        float currentSpeed = baseSpeed * DifficultyManager.SpeedMultiplier;
        if (agent.speed != currentSpeed)
        {
            agent.speed = currentSpeed;
            speedAfterDifficulty = currentSpeed;
            Debug.Log($"Adjusted Speed: {agent.speed}, Multiplier: {DifficultyManager.SpeedMultiplier}, New Speed: {speedAfterDifficulty}");
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print(true);
            player = collision.gameObject;
        }
    }
 
    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);

        if (doorKeyPrefab != null)
        {
            Instantiate(doorKeyPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
 
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
 
        if (health <= 0)
        {
            Die();
        }
    }

    public float Health
    {
        get { return health; }
    }

    public void StartDealDamage()
    {
         GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
         GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
 
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}