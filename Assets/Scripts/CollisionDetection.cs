using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject textBox;

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
            Debug.Log("Collided with player");
            textBox.SetActive(true);
            typeWriterEffect.RevealText();
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
