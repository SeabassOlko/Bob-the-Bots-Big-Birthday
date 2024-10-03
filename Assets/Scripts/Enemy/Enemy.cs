using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(VisionConeScript))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField]HealthBar healthBar;
    protected Animator anim;
    [SerializeField] protected GameObject popParticles;

    protected float health;
    [SerializeField] protected float MAX_HEALTH;

    protected VisionConeScript enemyVision;

    protected AudioSource enemyAudioSource;

    protected bool isDead = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();

        enemyAudioSource = GetComponent<AudioSource>();

        enemyVision = GetComponent<VisionConeScript>();

        if (MAX_HEALTH <= 0) MAX_HEALTH = 4;

        health = MAX_HEALTH;
    }

    // Update is called once per frame
    public virtual void hit(int damage)
    {
        health -= damage;
    }

    public virtual void hitPlayer(Collider player)
    {

    }
}
