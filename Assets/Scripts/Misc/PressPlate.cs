using System.Collections;  // For IEnumerator
using UnityEngine;

public class PressPlate : MonoBehaviour
{
    [SerializeField] private GameObject TopPlunger; // Reference to the TopPlunger
    [SerializeField] private float squishAmount = 0.5f; // Amount to squash the player
    [SerializeField] private float squishDuration = 0.5f; // Duration of the squish effect
    [SerializeField] private float returnDuration = 0.2f; // Duration for returning to original position

    private Vector3 originalPosition; // Store the original position
    private bool isTriggered = false; // Flag to check if already triggered

    void Start()
    {
        originalPosition = transform.localPosition; // Save the original position in local space
    }

    void Update()
    {
        // Ensure that the TopPlunger stays in position relative to its parent
        if (TopPlunger.transform.localPosition.y < originalPosition.y - 0.1f)
        {
            TopPlunger.transform.localPosition = new Vector3(TopPlunger.transform.localPosition.x, originalPosition.y - 0.1f, TopPlunger.transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            Debug.Log("Player entered trigger. Slamming down.");
            isTriggered = true; // Set the flag to true
            // Start the squish effect
            StartCoroutine(SquishPlayer(other.transform));
        }
    }

    private IEnumerator SquishPlayer(Transform playerTransform)
    {
        // Store the original scale in a PlayerSize script
        PlayerSize playerSize = playerTransform.GetComponent<PlayerSize>();
        if (playerSize != null && !playerSize.isSquished)
        {
            playerSize.StoreOriginalSize(playerTransform.localScale); // Save original size
            playerSize.isSquished = true; // Mark as squished

            Vector3 squishedScale = new Vector3(playerTransform.localScale.x, playerTransform.localScale.y - squishAmount, playerTransform.localScale.z); // Calculate the new scale

            // Squish the player
            playerTransform.localScale = squishedScale;

            // Move the TopPlunger down
            Vector3 targetPosition = originalPosition - new Vector3(0, 0.5f, 0); // Move down by 0.5 units

            float elapsedTime = 0;
            while (elapsedTime < squishDuration)
            {
                // Smoothly move TopPlunger down
                TopPlunger.transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / squishDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the TopPlunger is at the target position
            TopPlunger.transform.localPosition = targetPosition;

            // Wait for a brief moment after squishing
            yield return new WaitForSeconds(0.1f);

            // Reset the TopPlunger position back to the original
            elapsedTime = 0;
            while (elapsedTime < returnDuration)
            {
                TopPlunger.transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / returnDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the TopPlunger is back to the original position
            TopPlunger.transform.localPosition = originalPosition;

            // Restore the original scale
            playerTransform.localScale = playerSize.originalSize;
            playerSize.isSquished = false; // Mark as not squished anymore
            isTriggered = false; // Reset the trigger flag
        }
    }
}
