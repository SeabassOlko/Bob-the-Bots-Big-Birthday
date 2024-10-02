using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    public Vector3 originalSize; // Store original size
    public bool isSquished = false; // Track if the player is currently squished

    public void StoreOriginalSize(Vector3 size)
    {
        if (originalSize == Vector3.zero) // Check if original size is not set
        {
            originalSize = size; // Save the original size
        }
    }
}
