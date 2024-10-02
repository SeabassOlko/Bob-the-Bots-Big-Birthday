using UnityEngine;

public class ParticleOnCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleEffect; // Reference to the particle system

    private ParticleSystem instantiatedEffect;

    private void Start()
    {
        // Instantiate the particle system but deactivate it
        instantiatedEffect = Instantiate(particleEffect, transform.position, Quaternion.identity);
        instantiatedEffect.gameObject.SetActive(false); // Deactivate initially
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision detected with: " + collider.gameObject.name);

        // Check if the collided object has a specific tag
        if (collider.gameObject.CompareTag("Player"))
        {
            // Set the position and activate the particle effect
            instantiatedEffect.transform.position = transform.position; // Reset position
            instantiatedEffect.gameObject.SetActive(true); // Activate it
            instantiatedEffect.Play(); // Play the particle effect
            Debug.Log("Particle effect triggered on collision!");
        }
    }

    private void Update()
    {
        // Check if the particle system has finished playing
        if (instantiatedEffect.isPlaying == false && instantiatedEffect.gameObject.activeSelf)
        {
            // Deactivate it once it's done playing
            instantiatedEffect.gameObject.SetActive(false);
        }
    }
}

