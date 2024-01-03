using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public GameObject doorKeyPrefab; 

    public void DropKey()
    {
        if (doorKeyPrefab != null)
        {
            Instantiate(doorKeyPrefab, transform.position, Quaternion.identity);
        }
    }
}
