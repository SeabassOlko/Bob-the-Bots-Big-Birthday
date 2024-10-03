using System.Collections;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public ParticleSystem[] vfxArray;
    public float interval = 3f;

    void Start()
    {
        StartCoroutine(PlayAndStopVFX());
    }

    IEnumerator PlayAndStopVFX()
    {
        while (true)
        {
            foreach (ParticleSystem vfx in vfxArray)
            {
                vfx.Play();
            }

            yield return new WaitForSeconds(interval);

            foreach (ParticleSystem vfx in vfxArray)
            {
                vfx.Stop();
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
