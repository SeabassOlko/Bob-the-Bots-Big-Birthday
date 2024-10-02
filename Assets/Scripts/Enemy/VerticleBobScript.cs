using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticleBobScript : MonoBehaviour
{
    [SerializeField] float bobAmount = 0.5f;

    float originalYPos;

    void Start()
    {
        originalYPos = transform.position.y - bobAmount;    
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Original y pos: " + originalYPos + " bob mafs: " + (Mathf.Cos(Time.time) * bobAmount));
        float yPos = originalYPos + (Mathf.Cos(Time.time) * bobAmount);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);       
    }
}
