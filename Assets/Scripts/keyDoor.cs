using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyDoor : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isDoorOpen = false;

    private Inventory playerInventory; // Reference to the player's inventory



    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!isDoorOpen && playerInventory != null && playerInventory.checkItems("doorKey(Clone)"))
            {
                OpenDoor();
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

    private bool hasKey()
    {
        return playerInventory != null && playerInventory.items.Count > 0;
    }
}
