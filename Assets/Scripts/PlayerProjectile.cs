using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    public float bulletDamage = 10f;  // Adjust the damage value as needed


    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Balloon"))
        {

            Destroy(collision.gameObject, 1.0f); // Wait for 1 second before destroying
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the bullet if it hits the ground
            Destroy(gameObject);
        }

    }
}
