using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VisionConeScript))]
public class EnemyPatroller : Enemy
{
    bool canShoot = true;
    [SerializeField] Transform playerCenter;
    [SerializeField] GameObject balloonCenterMass;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileSpawnPoint;
    [SerializeField] ParticleSystem muzzleFlash;

    EnemyMovement movement;
    [SerializeField] float stopAndShootDistance;
    [SerializeField] AudioClip gunShotClip;
    [SerializeField] ParticleSystem sparklerParticles;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        enemyAudioSource = GetComponent<AudioSource>();
        movement = GetComponent<EnemyMovement>();
        playerCenter = GameObject.Find("BobCenterMass").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (enemyVision.SeeTarget())
        {
            movement.chase();

            if (Vector3.Distance(transform.position, playerCenter.position) <= stopAndShootDistance)
            {
                movement.stopMove();
                transform.LookAt(playerCenter.position);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

                if (canShoot)
                    StartCoroutine(Shoot());
            }
            else
                movement.startMove();
        }
        else
            movement.Idle();
    }

    public override void hit(int damage)
    {
        base.hit(damage);

        if (health <= 0)
        {
            Death();
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        enemyAudioSource.PlayOneShot(gunShotClip);
        muzzleFlash.Play();
        Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.LookRotation(playerCenter.position - projectileSpawnPoint.transform.position, Vector3.up));
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }

    void Death()
    {
        health = 0;
        isDead = true;
        movement.isDead = isDead;
        Instantiate(popParticles, balloonCenterMass.transform.position, balloonCenterMass.transform.rotation);
        movement.stopMove();
        Destroy(gameObject);
    }
}
