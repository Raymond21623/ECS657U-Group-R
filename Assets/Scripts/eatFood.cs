using UnityEngine;
using TMPro;
using System.Collections;

public class eatFood : MonoBehaviour
{
    public Canvas pickupPromptCanvas;

    private bool isPlayerNear = false;

    private HealthSystem playerHealthSystem;
    public float healthIncreaseAmount = 5f;

    public GameObject[] foodModels;
    private int currentModelIndex = 1;

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
            if (playerHealthSystem.getHealth < 100)
            {
                eat();
                currentModelIndex++;
            }
            else
            {
                ShowMessage();
                Debug.Log("Full Health");
            }
                
        }
    }

    private void ShowMessage()
    {
        textBox.SetActive(true);
        textComponent.text = "Full Health";
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

    private void eat()
    {
        Debug.Log("Eating");
        if (playerHealthSystem != null)
        {
            gameObject.SetActive(false);
            if (pickupPromptCanvas != null)
                pickupPromptCanvas.enabled = false;
            foodModels[currentModelIndex].SetActive(true);
            Debug.Log(currentModelIndex);
            playerHealthSystem.IncreaseHealth(healthIncreaseAmount);         
        }
    }
}
