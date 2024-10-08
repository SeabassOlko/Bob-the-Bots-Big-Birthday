using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotateScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
