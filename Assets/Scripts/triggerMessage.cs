using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class triggerMessage : MonoBehaviour
{

    public GameObject textBox;
    public TextMeshProUGUI textComponent;

    void Start()
    {
        textBox.SetActive(false);
    }

    private void ShowMessage()
    {
        textBox.SetActive(true);
        textComponent.text = "Tutorial Level";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textBox.SetActive(false);
        }
    }
}
