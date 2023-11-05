using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject textBox;

    public string fullText;

    TypeWriterEffect typeWriterEffect;

    TextMeshProUGUI text;


    private void Awake()
    {
        text = textBox.GetComponentInChildren<TextMeshProUGUI>();

        typeWriterEffect = text.GetComponent<TypeWriterEffect>();
    }

    private void Start()
    {
        textBox.SetActive(false);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            /*if(CompareTag("QuestTrigger"))
            {
                textBox.SetActive(true);
                typeWriterEffect.fullText = fullText;
                typeWriterEffect.RevealText();
            }*/
            if (gameObject.tag == "EndTrigger")
            {
                Debug.Log("collided");
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            } 
            else
            {
                textBox.SetActive(true);
                typeWriterEffect.fullText = fullText;
                typeWriterEffect.RevealText();
            }
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
