using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonManTrigger : MonoBehaviour
{
    private SaveLoad saveloadMGR;

    void Start()
    {
        // Find and assign the SaveLoad manager if it's in the scene
        saveloadMGR = FindObjectOfType<SaveLoad>();

        if (saveloadMGR == null)
        {
            Debug.LogError("SaveLoad manager not found in the scene. Please ensure there is a GameObject with the SaveLoad script attached.");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (saveloadMGR != null)
            {
                
                SceneManager.LoadScene("FUNTIME");
            }
            else
            {
                Debug.LogError("SaveLoad manager reference is missing. Cannot save game.");
            }
        }
    }
}
