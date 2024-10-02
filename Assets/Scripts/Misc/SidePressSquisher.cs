using System.Collections;  // For IEnumerator
using UnityEngine;

public class SidePress : MonoBehaviour
{
    public float restoreDuration = 0.5f; // Duration to restore the player's size
    private bool isTriggered = false; // Flag to check if already triggered

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true; // Set the flag to true
            // Start the restore effect
            StartCoroutine(RestorePlayerSize(other.transform));
        }
    }

    private IEnumerator RestorePlayerSize(Transform playerTransform)
    {
        PlayerSize playerSize = playerTransform.GetComponent<PlayerSize>();
        if (playerSize != null && playerSize.isSquished)
        {
            Vector3 squishedScale = playerTransform.localScale; // Get current squished scale

            // Smoothly restore the player's size
            float elapsedTime = 0;
            while (elapsedTime < restoreDuration)
            {
                playerTransform.localScale = Vector3.Lerp(squishedScale, playerSize.originalSize, (elapsedTime / restoreDuration));
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure the player's scale is set to the original size
            playerTransform.localScale = playerSize.originalSize;

            playerSize.isSquished = false; // Mark as not squished anymore
            
        }
        isTriggered = false; // Reset the trigger flag
    }
}
