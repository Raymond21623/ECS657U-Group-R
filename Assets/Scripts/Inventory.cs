using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject keyImage;

    private void Start()
    {
        keyImage.SetActive(false);
    }


    private void Update()
    {
        if (items != null && checkItems("doorKey(Clone)"))
        {
            ShowKeyImage();
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
        if (keyImage != null)
        {
            keyImage.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Key image not assigned");
        }
    } 
}