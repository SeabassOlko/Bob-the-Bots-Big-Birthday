using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Enemy : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] protected ParticleSystem popParticles;

    protected float health;
    [SerializeField] protected float MAX_HEALTH;

    [SerializeField] protected GameObject drop;

    protected AudioSource enemyAudioSource;

    protected bool isDead = false;



    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();

        enemyAudioSource = GetComponent<AudioSource>();

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
