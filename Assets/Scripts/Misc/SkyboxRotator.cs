using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1f;  // Speed of rotation

    void Update()
    {
        // Rotate the skybox by modifying its "_Rotation" property
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}