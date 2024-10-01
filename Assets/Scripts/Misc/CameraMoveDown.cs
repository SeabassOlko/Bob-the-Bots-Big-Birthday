using UnityEngine;

public class CameraMoveDown : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed at which the camera moves downward

    void Update()
    {
        // Move the camera downward
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
