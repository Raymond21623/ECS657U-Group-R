using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class triggerMessage : MonoBehaviour
{

    public GameObject textBox;
    public TypeWriterEffect typeWriterEffect;

    public string message;

    void Start()
    {
        textBox.SetActive(false);
    }

    private void ShowMessage()
    {
        textBox.SetActive(true);
        typeWriterEffect.fullText = message;
        typeWriterEffect.RevealText();
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
