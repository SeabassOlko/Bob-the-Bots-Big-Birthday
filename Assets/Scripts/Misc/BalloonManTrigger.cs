using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonManTrigger : MonoBehaviour
{


    void Start()
    {


    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {


            SceneManager.LoadScene("FUNTIME");

        }
    }
}
