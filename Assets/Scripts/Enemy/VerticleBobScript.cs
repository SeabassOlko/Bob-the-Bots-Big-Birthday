using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticleBobScript : MonoBehaviour
{
    [SerializeField] float bobAmount = 0.5f;
    [SerializeField] float parentTransform;

    [SerializeField] float originalYPos;

    void Start()
    {
        originalYPos = transform.position.y - bobAmount;
    }
    // Update is called once per frame
    void Update()
    {
        parentTransform = transform.parent.position.y;
        float yPos = (parentTransform + originalYPos) + (Mathf.Cos(Time.time) * bobAmount);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);       
    }
}
