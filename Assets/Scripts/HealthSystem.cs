using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 10;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    [SerializeField] private TextMeshProUGUI healthText;
    public Slider healthbar;


    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateHealthUI();
    }

    public float getHealth
    {
        get { return health; }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        UpdateHealthUI();
        if (health <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        UpdateHealthUI();
    }

    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
 
    }

    private void UpdateHealthUI() 
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
            healthbar.value = health;
        }
    }
}