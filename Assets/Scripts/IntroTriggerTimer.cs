using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTriggerTimer : MonoBehaviour
{
    [SerializeField] private GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        WaitForSeconds(20);
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        trigger.SetActive(true);
    }
}
