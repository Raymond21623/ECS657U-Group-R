using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDoor : MonoBehaviour
{
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            OpenDoor();

        }
    }

    void OpenDoor()
    {
        transform.rotation = Quaternion.Euler(0, 40, 0);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
