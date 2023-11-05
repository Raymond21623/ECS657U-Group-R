using UnityEngine;

public class Armour : MonoBehaviour
{
    public int armorValue = 1; // The amount of armor this sphere will give

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.AddArmor(armorValue);
                Destroy(gameObject); // Destroy the sphere after giving armor
            }
        }
    }
}
