using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // The name of the scene to load after pressing a key
    SaveLoad saveloadMGR;

    private bool canPressKey = false;

    void Start()
    {
        // Lock the player's control at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start the delay coroutine
        StartCoroutine(WaitBeforeUnlocking(10f));
    }

    private void Update()
    {
        // If the waiting period has passed, allow any key press to change the scene
        if (canPressKey && Input.anyKeyDown)
        {
            ChangeScene();
        }
    }

    private System.Collections.IEnumerator WaitBeforeUnlocking(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Unlock player controls
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canPressKey = true;
    }

    private void ChangeScene()
    {
        // Make sure a scene name has been specified
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
            
        }
        else
        {
            Debug.LogWarning("No scene specified to load.");
        }
    }
}
