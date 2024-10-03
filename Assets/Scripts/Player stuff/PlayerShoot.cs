using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    private PlayerController playerController;
    private Animator anim;

    AudioSource audioSource;
    [SerializeField] AudioClip gunshotClip;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float shootDistance;
    [SerializeField] int damage;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (playerController.weapon != null)
        {
            // Handle aiming when the right mouse button is held down
            if (Input.GetMouseButton(1))
            {
                anim.SetBool("Aiming", true);

                // If the left mouse button is pressed while aiming, shoot
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                    audioSource.PlayOneShot(gunshotClip);
                    muzzleFlash.Play();
                }
            }
            else
            {
                // Set "Aim" to false only when the right mouse button is released
               anim.SetBool("Aiming", false);
            }
        }
    }

    public void SetMuzzleFlash(ParticleSystem temp)
    {
        muzzleFlash = temp;
    }

    void Shoot()
    {
        Transform camera = Camera.main.transform;
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit))
        {
            if (hit.distance <= shootDistance)
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<Enemy>().hit(damage);
                }
            }
        }
    }
}
