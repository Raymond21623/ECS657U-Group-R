using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDoor : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isDoorOpen = false;


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
        isDoorOpen = true;
        StartCoroutine(CloseDoorAfterDelay(30));
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
