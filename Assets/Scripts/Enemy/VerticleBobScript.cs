using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticleBobScript : MonoBehaviour
{
    [SerializeField] float bobAmount = 1f;
    // Update is called once per frame
    void Update()
    {
        float yPos = 4.2f - Mathf.PingPong(Time.time, bobAmount);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);       
    }
}
