using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBomber : Enemy
{
    [SerializeField] int damage = 5;
    [SerializeField] float stopDistance = 1f;
    [SerializeField] float explosionRadius = 3f;

    [SerializeField] GameObject explosionSFX;
    [SerializeField] GameObject balloonCenterMass;

    EnemyMovement movement;

    [SerializeField] Transform player;
    [SerializeField] AudioClip attackClip;
    [SerializeField] ParticleSystem sparklerParticles;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        enemyAudioSource = GetComponent<AudioSource>();
        movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (enemyVision.SeeTarget())
        {
            movement.chase();
            if (!sparklerParticles.isPlaying)
                sparklerParticles.Play();
            StartCoroutine(BlowUp());
            if (Vector3.Distance(transform.position, player.position) <= stopDistance)
            {
                movement.stopMove();
            }
        }
        else
            movement.Idle();
    }

    public override void hit(int damage)
    {
        base.hit(damage);

        if (health <= 0)
        {
            death();
        }
    }

    public override void hitPlayer(Collider player)
    {
        base.hitPlayer(player);

        //player.gameObject.GetComponent<PlayerController>().Hurt(damage);

    }

    IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(4.0f);
        Instantiate(explosionSFX, balloonCenterMass.transform.position, balloonCenterMass.transform.rotation);
        if (Vector3.Distance(transform.position, player.position) <= explosionRadius)
        {
            Debug.Log("Player hit by explosion, takes " + damage + " damage");
            //hurt player
        }
        Destroy(gameObject);
    }


    void death()
    {
        health = 0;
        isDead = true;
        movement.isDead = isDead;
        popParticles.Play();
        movement.stopMove();
        anim.SetTrigger("Death");
        Destroy(gameObject);
    }
}
