using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player; // Reference to the player's transform (parent object)
    public Transform cameraTransform; // Reference to the camera's transform for vertical mouse rotation
    public Vector3 thirdPersonOffset = new Vector3(0, 3f, -3.5f); // Offset for third-person view
    public Vector3 firstPersonOffset = new Vector3(0, 1.7f, 0.1f); // Offset for first-person view
    public float transitionSpeed = 2f; // Speed for smooth transition between views
    public float mouseSensitivity = 100f; // Mouse sensitivity for looking around
    private float xRotation = 0f; // To keep track of vertical camera rotation

    private Vector3 targetPosition;

    void Start()
    {
        // Initially set to third-person view
        targetPosition = thirdPersonOffset;
    }

    void Update()
    {
        // Smoothly transition the camera position relative to the player
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);

        // Handle mouse look around for vertical rotation only
        //MouseLookAround();
    }

    public void SwitchView(bool isFirstPerson)
    {
        if (isFirstPerson)
        {
            // Set the target position for first-person view (relative to the player)
            targetPosition = firstPersonOffset;
        }
        else
        {
            // Set the target position for third-person view (relative to the player)
            targetPosition = thirdPersonOffset;
        }
    }

    void MouseLookAround()
    {
        // Get mouse movement for vertical rotation
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera vertically (X-axis)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit camera vertical rotation to avoid flipping
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

