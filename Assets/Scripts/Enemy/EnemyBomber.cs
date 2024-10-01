using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    [SerializeField] int damage = 5;

    EnemyMovement movement;

    [SerializeField] Transform player;
    [SerializeField] AudioClip attackClip;

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

        if (Vector3.Distance(transform.position, player.position) <= 15f)
        {
            movement.chase();
            if (Vector3.Distance(transform.position, player.position) <= 2.0f)
            {
                //blow up
            }
        }
        else
            movement.patrol();

        //anim.SetFloat("Speed", transform.TransformDirection(transform.position).magnitude);
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
