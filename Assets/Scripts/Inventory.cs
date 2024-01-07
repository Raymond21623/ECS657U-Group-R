using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject door_keyImage;
    public GameObject final_keyImage;


    private void Start()
    {
        door_keyImage.SetActive(false);
        final_keyImage.SetActive(false);

    }


    private void Update()
    {
        if (items != null && checkItems("doorKey(Clone)"))
        {
            ShowKeyImage();
        }
        if (items != null && checkItems("finalKey"))
        {
            ShowFinalKeyImage();
        }
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log(item);
    }

    public bool checkItems(string itemName)
    {
        foreach (var item in items)
        {
            if (item != null && item.name == itemName)
            {
                Debug.Log(item + "is in inventory");
                return true;
            }
        }
        return false;
    }

    private void ShowKeyImage()
    {
        if (door_keyImage != null)
        {
            door_keyImage.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Key image not assigned");
        }
    }

    private void ShowFinalKeyImage()
    {
        if (final_keyImage != null)
        {
            door_keyImage.SetActive(false);
            final_keyImage.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Key image not assigned");
        }
    }
}