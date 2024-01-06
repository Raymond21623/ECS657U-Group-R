using UnityEngine;
using TMPro;
using System.Collections;

public class Armour : MonoBehaviour
{
    public Canvas pickupPromptCanvas;

    private bool isPlayerNear = false;

    private HealthSystem playerHealthSystem;
    private float armourincrease = 50;

    public GameObject textBox;
    public TextMeshProUGUI textComponent;

    private void Start()
    {
        textBox.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealthSystem = player.GetComponent<HealthSystem>();
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (playerHealthSystem.getArmour < 50)
            {
                PickUp();
            }
            else
            {
                ShowMessage();
                Debug.Log("Full Armour");
            }

        }
    }

    private void ShowMessage()
    {
        textBox.SetActive(true);
        textComponent.text = "Full Armour";
        StartCoroutine(HideMessageAfterDelay(3));

    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.gameObject.SetActive(false);

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
                pickupPromptCanvas.enabled = true;
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (pickupPromptCanvas != null)
                pickupPromptCanvas.enabled = false;
        }
    }


    private void PickUp()
    {
        Debug.Log("Picking Armour");

        Inventory playerInventory = Object.FindObjectOfType<Inventory>();
        if (playerInventory != null)
        {
            playerInventory.AddItem(gameObject);
            if (pickupPromptCanvas != null)
                pickupPromptCanvas.enabled = false; // Hide the Canvas

            Debug.Log("Armour has been stored in inventory"+ gameObject);
            playerHealthSystem.AddArmor(armourincrease);
        }
    }
}
