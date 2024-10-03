using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretShoot : Enemy
{
    [SerializeField] Transform playerCenter;
    [SerializeField] GameObject balloonCenterMass;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileSpawnPoint;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip gunShotClip;


    bool canShoot = true;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        enemyAudioSource = GetComponent<AudioSource>();
        health = MAX_HEALTH;
        playerCenter = GameObject.Find("BobCenterMass").transform;
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
        Instantiate(popParticles, balloonCenterMass.transform.position, balloonCenterMass.transform.rotation);
        Destroy(gameObject);
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

    public override void hit(int damage)
    {
        base.hit(damage);

        if (health <= 0)
        {
            death();
        }
    }
}
