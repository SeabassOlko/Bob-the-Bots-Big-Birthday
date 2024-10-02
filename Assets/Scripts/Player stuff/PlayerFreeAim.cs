using System.Collections;
using UnityEngine;

public class PlayerFreeAim : MonoBehaviour
{
    [SerializeField] private Transform gun;               // Gun Transform (attach your gun model here)
    [SerializeField] private Transform cameraTransform;   // Main Camera Transform
    [SerializeField] private float rotationSmoothness = 10f;

    [SerializeField] private float maxAimAngle = 90f;     // Maximum up/down rotation in degrees
    private float xRotation = 0f;                         // Camera vertical rotation tracker
    [SerializeField] private float mouseSensitivity = 100f;

    void Update()
    {
        // Handle mouse look for free aiming
        HandleMouseLook();

        // Align the gun with the camera
        AlignGunToCamera();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player horizontally (Y-axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically (X-axis)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxAimAngle, maxAimAngle);  // Limit camera vertical rotation
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void AlignGunToCamera()
    {
        if (gun != null)
        {
            // Smoothly align the gun's forward direction with the camera's forward direction
            Quaternion targetRotation = cameraTransform.rotation;
            gun.rotation = Quaternion.Slerp(gun.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }
    }
}
