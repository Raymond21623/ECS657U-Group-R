using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] public Transform target;

    void Update()
    {
        // Get the current rotation of the health bar
        Vector3 currentRotation = transform.eulerAngles;

        // Calculate the new rotation only along the Y-axis
        float newYRotation = Mathf.Atan2(target.position.x - transform.position.x, target.position.z - transform.position.z) * Mathf.Rad2Deg;

        // Set the new rotation while keeping X and Z axes unchanged
        transform.eulerAngles = new Vector3(currentRotation.x, newYRotation, currentRotation.z);
    }
}
