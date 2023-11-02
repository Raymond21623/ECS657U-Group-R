using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    public void RevealText()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
