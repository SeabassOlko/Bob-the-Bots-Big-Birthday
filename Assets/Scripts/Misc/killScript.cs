using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killScript : MonoBehaviour
{
    public GameObject[] objectToKill;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToKill.Length > 0)
            {
                foreach (GameObject obj in objectToKill)
                {
                    Destroy(obj);
                }
            }
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (objectToKill.Length > 0)
            {
                foreach (GameObject obj in objectToKill)
                {
                    Destroy(obj);
                }
            }
        }
    }
}
