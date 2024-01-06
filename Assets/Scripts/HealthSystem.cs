using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 10;
    [SerializeField] float armour = 0;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    [SerializeField] private TextMeshProUGUI healthText;
    public Slider healthbar;
    public Slider armourbar;



    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateHealthUI();
        armour = 0;
        armourbar.value = 0;
    }

    public float getHealth
    {
        get { return health; }
    }

    public float getArmour
    {
        get { return armour; }
    }

    public void TakeDamage(float damageAmount)
    {


        armour -= damageAmount;

        if (armour <=0)
        {
            health += armour;
            armour = 0;
        }

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

    public void AddArmor(float armourValue)
    {
        armour += armourValue;
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
            armourbar.value = armour;
        }
    }
}