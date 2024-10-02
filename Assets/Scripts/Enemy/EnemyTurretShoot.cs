using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretShoot : Enemy
{
    [SerializeField] Transform playerCenter;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileSpawnPoint;
    [SerializeField] ParticleSystem muzzleFlash;

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

        if (enemyVision.SeeTarget())
        {
            transform.LookAt(playerCenter.position);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

            if (canShoot) 
                StartCoroutine(Shoot());
        }
    }

    public void death()
    {
        isDead = true;
        popParticles.Play();
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        //anim.SetTrigger("Shoot");
        //yield return new WaitForSeconds(0.43f);
        //enemyAudioSource.PlayOneShot(attackClip);
        muzzleFlash.Play();
        Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.LookRotation(playerCenter.position - projectileSpawnPoint.transform.position, Vector3.up));
        yield return new WaitForSeconds(2f);
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
