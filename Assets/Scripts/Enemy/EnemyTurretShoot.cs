using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretShoot : Enemy
{
    [SerializeField] Transform player;

    [SerializeField] GameObject projectile;

    [SerializeField] AudioClip hurtClip;
    [SerializeField] AudioClip attackClip;


    bool canShoot = true;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        enemyAudioSource = GetComponent<AudioSource>();
        health = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (Vector3.Distance(transform.position, player.position) <= 10f)
        {
            transform.LookAt(player.position);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

            if (canShoot)
                StartCoroutine(shoot());

        }
    }

    public void death()
    {
        isDead = true;
        popParticles.Play();
        Destroy(gameObject, 0.5f);
    }

    IEnumerator shoot()
    {
        canShoot = false;
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.43f);
        enemyAudioSource.PlayOneShot(attackClip);
        Instantiate(projectile, transform.position + new Vector3(0, 0.75f, 0.5f), transform.rotation);
        yield return new WaitForSeconds(4f);
        canShoot = true;
    }

    public override void hit(int damage)
    {
        base.hit(damage);

        if (health <= 0)
        {
            death();
        }
    }
}
