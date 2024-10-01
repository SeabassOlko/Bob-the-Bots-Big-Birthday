using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform offset;
    public float bulletSpeed = 20.0f;

    private PlayerController playerController;
    private Animator anim;

    public ParticleSystem shootEffect;
    [SerializeField] private AudioSource Gunshot;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerController.weapon != null)
        {
            // Handle aiming when the right mouse button is held down
            if (Input.GetMouseButton(1))
            {
               // anim.SetBool("Aiming", true);

                // If the left mouse button is pressed while aiming, shoot
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                    Gunshot.Play();
                }
            }
            else
            {
                // Set "Aim" to false only when the right mouse button is released
               // anim.SetBool("Aiming", false);
            }
        }
    }

    void Shoot()
    {

            GameObject spawnedBullet = Instantiate(bullet, offset.position, offset.rotation);
            Rigidbody bulletRb = spawnedBullet.GetComponent<Rigidbody>();
            bulletRb.velocity = offset.forward * bulletSpeed;
            Instantiate(shootEffect, offset.position, offset.rotation);

    }
}
