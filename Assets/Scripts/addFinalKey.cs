using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addFinalKey : MonoBehaviour
{

    private bool isPlayerNear = false;
    public GameObject finalKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected at End Game");
            isPlayerNear = true;
            Inventory playerInventory = Object.FindObjectOfType<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(finalKey);
                Debug.Log("Final Key has been stored in inventory" + finalKey);

            }
        }
    }
}
