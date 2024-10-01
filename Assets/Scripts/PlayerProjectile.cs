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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the EnemyHealth component from the collided enemy GameObject
            //EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            //if (enemyHealth != null)
            //{
            //    // Apply damage to the enemy
            //    enemyHealth.TakeDamage(bulletDamage);

            //    // Trigger the enemy's hit animation
            //    Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
            //    if (enemyAnimator != null)
            //    {
            //        enemyAnimator.SetTrigger("HitBullet");
            //    }

            //    // Optionally, destroy the enemy after a delay if health is 0
            //    if (enemyHealth.currentHealth <= 0)
            //    {
            //        Destroy(collision.gameObject, 1.0f); // Wait for 1 second before destroying





            //    }
            // }

            // Destroy the bullet after it hits the enemy
            // Destroy(gameObject);
            //}
            if (collision.gameObject.CompareTag("Ground"))
            {
                // Destroy the bullet if it hits the ground
                Destroy(gameObject);
            }
        }
    }
}
