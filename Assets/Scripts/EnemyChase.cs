using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 10f; // Distance at which the enemy will start chasing the player
    [SerializeField] private float stopDistance = 2f; // Distance at which the enemy will stop chasing the player
    [SerializeField] private float moveSpeed = 5f; // Speed of the enemy
    [SerializeField] private Transform player; // Reference to the player's Transform

    private bool isChasing = false; // Track if the enemy is currently chasing

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
            isChasing = false; // Stop chasing behavior (you may want to implement idle/patrol behavior here)
        }
    }
}
