using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy this projectile upon collision with anything
        Destroy(gameObject);
    }
}
