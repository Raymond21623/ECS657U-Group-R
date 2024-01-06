using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class triggerMessage : MonoBehaviour
{

    public GameObject textBox;
    public TypeWriterEffect typeWriterEffect;
    public int delayTime = 3;

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
        StartCoroutine(HideMessageAfterDelay(delayTime));
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.SetActive(false);
    }


private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowMessage();
        }
    }

   
}
