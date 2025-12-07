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
    public float minDistance = 0.5f; // Minimum distance the camera can be to the player
    public LayerMask collisionMask; // Layers the camera should collide with (like walls)

    public bool NormalView = true;

    void Start()
    {
        // Initially set to third-person view
        targetPosition = thirdPersonOffset;
    }

    void Update()
    {
        // Adjust the camera position to avoid clipping
        AdjustCameraPosition();

        // Smoothly transition the camera position relative to the player
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);

        // Handle mouse look around for vertical rotation only
        //MouseLookAround();
    }

    void AdjustCameraPosition()
    {
        // Calculate the target position for the camera based on the player's position
        Vector3 desiredCameraPos = player.TransformPoint(thirdPersonOffset);

        // Perform a raycast to check if there's an obstacle between the player and the camera
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredCameraPos, out hit, collisionMask) && NormalView)
        {
            // If an obstacle is detected, move the camera closer to the hit point
            float distanceToObstacle = Vector3.Distance(player.position, hit.point);
            Vector3 direction = (desiredCameraPos - player.position).normalized;
            targetPosition = player.InverseTransformPoint(player.position + direction * Mathf.Clamp(distanceToObstacle, minDistance, thirdPersonOffset.magnitude));
        }
        else if (NormalView)
        {
            // No obstacle, use the full third-person offset
            targetPosition = thirdPersonOffset;
        }
    }

    public void SwitchView(bool isFirstPerson)
    {
        if (isFirstPerson)
        {
            // Set the target position for first-person view (relative to the player)
            targetPosition = firstPersonOffset;
            NormalView = false;
        }
        else
        {
            // Set the target position for third-person view (relative to the player)
            targetPosition = thirdPersonOffset;
            NormalView = true;
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

