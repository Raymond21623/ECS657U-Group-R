using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Canvas pickupPromptCanvas; // Direct reference to the Canvas component

    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            isPlayerNear = true;
            if (pickupPromptCanvas != null)
            {
                Debug.Log("Showing Canvas");
                pickupPromptCanvas.enabled = true; // Show the Canvas
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (pickupPromptCanvas != null)
                pickupPromptCanvas.enabled = false; // Hide the Canvas
        }
    }

    private void PickUp()
    {
        Inventory playerInventory = Object.FindObjectOfType<Inventory>();
        if (playerInventory != null)
        {
            playerInventory.AddItem(gameObject);
            gameObject.SetActive(false); // Deactivate the key
            if (pickupPromptCanvas != null)
                pickupPromptCanvas.enabled = false; // Hide the Canvas

            Debug.Log("Key has been stored in inventory");
        }
    }
}
