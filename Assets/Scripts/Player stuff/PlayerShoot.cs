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
    [SerializeField] float cooldownTime = 1f;
    [SerializeField] GameObject ReticleCanvas;

    bool canShoot = true;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (playerController.weapon != null && GetComponent<PlayerSize>().isSquished == false)
        {
            // Handle aiming when the right mouse button is held down
            if (Input.GetMouseButton(1))
            {
                anim.SetBool("Aiming", true);
                if (!ReticleCanvas.activeSelf)
                {
                    ReticleCanvas.SetActive(true);
                }

                // If the left mouse button is pressed while aiming, shoot
                if (Input.GetMouseButtonDown(0) && canShoot)
                {
                    Shoot();
                    canShoot = false;
                    StartCoroutine(ShotCooldown());
                    audioSource.PlayOneShot(gunshotClip);
                    muzzleFlash.Play();
                }
            }
            else
            {
                // Set "Aim" to false only when the right mouse button is released
                anim.SetBool("Aiming", false);
                if (ReticleCanvas.activeSelf)
                {
                    ReticleCanvas.SetActive(false);
                }
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
                else if (hit.collider.tag == "Post")
                {
                    hit.collider.gameObject.GetComponent<PostTrigger>().Hit();
                }
            }
        }
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}
