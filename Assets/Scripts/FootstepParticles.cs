using UnityEngine;
using System.Collections;

public class FootstepParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem footstepParticle; // Reference to the footstep particle system
    [SerializeField] private float particleOffsetY = 0.1f; // Offset for particle position (if needed)

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the ground layer
        if (other.CompareTag("Ground")) // Make sure the ground has this tag
        {
            // Trigger the footstep particles
            TriggerFootstepParticles(other.transform.position);
        }
    }

    private void TriggerFootstepParticles(Vector3 footPosition)
    {
        // Instantiate the particle effect at the foot's position
        Vector3 particlePosition = footPosition + Vector3.up * particleOffsetY; // Adjust position if necessary
        ParticleSystem instantiatedParticles = Instantiate(footstepParticle, particlePosition, Quaternion.identity);

        // Play the particle effect
        instantiatedParticles.Play();


    }


}
