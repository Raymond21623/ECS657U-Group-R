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
    public GameObject textBox;
    public TextMeshProUGUI textComponent;

    public Slider healthbar;
    public Slider armourbar;

    public GameObject ArmourSlider;

    private int shownWarn;


    Animator animator;
    void Start()
    {
        textBox.SetActive(false);
        ArmourSlider.SetActive(false);
        animator = GetComponent<Animator>();
        UpdateHealthUI();
        armour = 0;
        armourbar.value = 0;
        shownWarn = 0;
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
            ArmourSlider.SetActive(false);

            if(shownWarn > 0)
            {
                warnArmour();
            }
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
        if(armour > 50)
        {
            armour = 50;
        }
        UpdateHealthUI();
        ShowArmourBar();
        shownWarn += 1;
    }

    private void ShowArmourBar()
    {
        if (ArmourSlider != null)
        {
            ArmourSlider.SetActive(true);
            Debug.Log("Armour Bar On");
        }
        else
        {
            Debug.LogWarning("Armour Slide not assigned");
        }
    }

    private void warnArmour() 
    {
        textBox.SetActive(true);
        textComponent.text = "Armour Destroyed";
        shownWarn = 0;
        StartCoroutine(HideMessageAfterDelay(3));
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.gameObject.SetActive(false);
    }

    public GameObject deathScreen;

    void Die()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0f; 
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 
        }
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