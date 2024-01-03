using UnityEngine;
using TMPro;

public class KeyPickup : MonoBehaviour
{
    public string pickupPromptName; // Correctly declared as a string
    private TextMeshProUGUI pickupPromptText;

    private bool isPlayerNear = false;

    private void Start()
    {
        // Find the TextMeshPro component by searching for its name
        GameObject pickupPromptGameObject = GameObject.Find(pickupPromptName);
        if (pickupPromptGameObject != null)
        {
            pickupPromptText = pickupPromptGameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (pickupPromptText != null)
                pickupPromptText.gameObject.SetActive(true); // Show the pickup prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (pickupPromptText != null)
                pickupPromptText.gameObject.SetActive(false); // Hide the pickup prompt
        }
    }

    private void PickUp()
    {
        Inventory playerInventory = Object.FindAnyObjectByType<Inventory>(); // Use the new method
        if (playerInventory != null)
        {
            playerInventory.AddItem(gameObject);
            gameObject.SetActive(false); // Deactivate the key
            if (pickupPromptText != null)
                pickupPromptText.gameObject.SetActive(false); // Hide the prompt

            Debug.Log("Key has been stored in inventory");
        }
    }
}
