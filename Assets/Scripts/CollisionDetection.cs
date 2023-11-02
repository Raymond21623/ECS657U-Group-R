using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collided with player");
            textBox.enabled = true;
        }
    }
}
