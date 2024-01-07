using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class finalDoor : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isDoorOpen = false;

    private Inventory playerInventory;

    public GameObject textBox;
    public TextMeshProUGUI textComponent;

    public GameObject textBox2;
    public TextMeshProUGUI textComponent2;

    void Start()
    {
        textBox.SetActive(false);
        textBox2.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!isDoorOpen && playerInventory != null && playerInventory.checkItems("finalKey"))
            {
                OpenDoor();
            }
            else
            {
                ShowMessage();
            }
        }
    }

    void OpenDoor()
    {
        transform.rotation = Quaternion.Euler(0, 40, 0);
        isDoorOpen = true;
        StartCoroutine(CloseDoorAfterDelay(10));
    }

    IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseDoor();
    }
    void CloseDoor()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isDoorOpen = false;
    }

    private void ShowMessage()
    {
        textBox.SetActive(true);
        textBox2.SetActive(true);

        textComponent.text = "Missing Final Key";
        textComponent2.text = "Discover All Rooms For Key";

        StartCoroutine(HideMessageAfterDelay(3));

    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            playerInventory = other.gameObject.GetComponent<Inventory>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            playerInventory = null;
        }
    }
}
