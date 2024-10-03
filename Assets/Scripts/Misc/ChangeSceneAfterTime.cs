using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterTime : MonoBehaviour
{
    [SerializeField] float lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSceneTimer(lifetime));
    }

    IEnumerator ChangeSceneTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Credits");
    }
}
