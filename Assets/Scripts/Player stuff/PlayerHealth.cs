using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int MAX_HEALTH;
    int health;

    HealthBar healthBar;

    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<HealthBar>();
        health = MAX_HEALTH;
        UpdateHealth();
    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            health = 0;
            UpdateHealth();
            isDead = true;
        }
        else
        {
            health -= damage;
            UpdateHealth();
        }
    }

    public void ResetHealth()
    {
        health = MAX_HEALTH;
        UpdateHealth();
        isDead = false;
    }

    void UpdateHealth()
    {
        healthBar.UpdateHealth(health, MAX_HEALTH);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
