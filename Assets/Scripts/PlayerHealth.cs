using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; 
    public int armor = 0;
    public TextMeshProUGUI healthText;
    private PlayerMovement playerMovement;
    private Vector3 startPosition;

    private void Start()
        {
            startPosition = transform.position;
            UpdateHealthUI();
            playerMovement = GetComponent<PlayerMovement>();
        }


    public void TakeDamage(int damage)
    {
        // Calculate damage after armor reduction
        int damageAfterArmor = Mathf.Max(damage - armor, 0);

        health -= damageAfterArmor;
        health = Mathf.Max(health, 0);
        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with an enemy projectile
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage(5); // Reduce health by 5 or any other desired amount
            Destroy(collision.gameObject); // Optionally destroy the projectile after it hits the player
        }
    }

    private void Die()
{
    // Handle player death here

    // Disable player movement
    if (playerMovement != null)
    {
        playerMovement.enabled = false;
    }

    // Play death animation (assuming you have an Animator component and a "Die" animation)
    Animator animator = GetComponent<Animator>();
    if (animator != null)
    {
        animator.SetTrigger("Die");
    }

    // Respawn the player at the start position after a delay
    Invoke("Respawn", 0.2f); 
}

private void Respawn()
{
    // Enable player movement
    if (playerMovement != null)
    {
        playerMovement.enabled = true;
    }

    // Reset player health if needed
    health = 100; // Or your default health value
    UpdateHealthUI();

    // Move player to start position
    transform.position = startPosition;


    // If you have a death animation, you might want to reset the Animator's state
    Animator animator = GetComponent<Animator>();
    if (animator != null)
    {
        animator.ResetTrigger("Die");
        animator.Play("Idle"); // Replace "Idle" with your default animation state name
    }
}

    public void AddArmor(int armorValue)
    {
        armor += armorValue;
        // Optionally update armor UI here
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + health;
    }

    // Method to restart the level
    void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
