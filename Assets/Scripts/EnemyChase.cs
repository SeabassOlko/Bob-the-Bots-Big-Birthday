using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 10f; // Distance at which the enemy will start chasing the player
    [SerializeField] private float stopDistance = 2f; // Distance at which the enemy will stop chasing the player
    [SerializeField] private float moveSpeed = 5f; // Speed of the enemy
    [SerializeField] private Transform player; // Reference to the player's Transform
    [SerializeField] private AudioClip screamClip;

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool isChasing = false; // Track if the enemy is currently chasing
    private bool hasScreamed = false; // Track if the enemy has screamed

    private void Awake()
    {
        // Get the AudioSource component from the GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Missing AudioSource component on Enemy.");
        }
    }

    private void Update()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance && !isChasing)
        {
            StartChasing();
        }

        if (isChasing)
        {
            ChasePlayer(distanceToPlayer);
        }
    }

    private void StartChasing()
    {
        isChasing = true;
        Debug.Log("Enemy started chasing the player.");

        if (!hasScreamed)
        {
            PlayScream();
            hasScreamed = true;
        }
    }

    private void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistance)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Stop chasing if close enough
            Debug.Log("Enemy stopped chasing the player.");
            StopChasing();
        }
    }

    private void StopChasing()
    {
        isChasing = false;
        hasScreamed = false; // Reset so the enemy can scream again next time
    }

    private void PlayScream()
    {
        if (audioSource != null && screamClip != null)
        {
            audioSource.PlayOneShot(screamClip);
        }
        else
        {
            Debug.LogWarning("AudioSource or screamClip is not assigned.");
        }
    }
}
