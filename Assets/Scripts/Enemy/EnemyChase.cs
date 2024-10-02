using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float speed = 5f; // Speed of the enemy
    public float stoppingDistance = 1.5f; // Distance at which enemy stops chasing

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the enemy
        rb = GetComponent<Rigidbody>();

        // If player reference is not set, find the player by tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found. Please assign a player reference or ensure the player has the 'Player' tag.");
            }
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Calculate the distance to the player
            float distance = Vector3.Distance(player.position, transform.position);

            // Move towards the player if distance is greater than the stopping distance
            if (distance > stoppingDistance)
            {
                Vector3 movePosition = direction * speed * Time.fixedDeltaTime;
                rb.MovePosition(transform.position + movePosition);
            }
        }
    }
}
