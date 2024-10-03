using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCrasherSpawner : MonoBehaviour
{
    public GameObject partyCrasher;
    public GameObject[] spawnPoint;
    public GameObject BannerToKill;
    public bool partyCrashed = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnPartyCrashers();
        }
    }

    public void SpawnPartyCrashers()
    {
        if (!partyCrashed)
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                Instantiate(partyCrasher, spawnPoint[i].transform.position, randomRotation);
            }

            Destroy(BannerToKill);

            partyCrashed = true;
        }
    }
}

