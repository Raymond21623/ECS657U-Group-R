using UnityEngine;
using UnityEngine.UI; // For the built-in UI
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Player's health

    // Reference to the UI Text component
    public TextMeshProUGUI healthText;  

    private PlayerMovement playerMovement;

    private void Start()
    {
        UpdateHealthUI();

        playerMovement = GetComponent<PlayerMovement>();
    }

    // Call this method whenever the player's health changes
    public void TakeDamage(int damage)
    {
        health -= damage;
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

        // Show a "Game Over" screen or message
        // This can be a UI element that you enable upon death
        // gameOverPanel.SetActive(true); // Assuming you have a reference to a UI panel

        // Optionally, after a delay, restart the level or go to the main menu
        // Invoke("RestartLevel", 3f); // Restart the level after 3 seconds
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + health;
    }
}
