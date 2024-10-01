using UnityEngine;

public class PlayerSquish : MonoBehaviour
{
    [SerializeField] private Vector3 squishScale = new Vector3(1, 0.2f, 1); // Scale to shrink player to
    [SerializeField] private float squishDuration = 0.5f; // Time taken to squish

    private Vector3 originalScale;
    public bool isSquished = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Press"))
        {
            Debug.Log("Player got squished!");
            StartCoroutine(SquishPlayer());
        }
    }

    private System.Collections.IEnumerator SquishPlayer()
    {
        isSquished = true;
        float elapsedTime = 0f;

        // Gradually squish the player down
        while (elapsedTime < squishDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, squishScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final squished scale to make sure it reaches the target
        transform.localScale = squishScale;

        // Optional: Add a delay before resetting to original scale, if desired
        yield return new WaitForSeconds(10f);

        // Gradually return to the original scale
        elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            transform.localScale = Vector3.Lerp(squishScale, originalScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set back to the original scale
        transform.localScale = originalScale;
        isSquished = false;
    }
}

