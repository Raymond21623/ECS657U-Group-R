using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        items.Add(item);
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
}
