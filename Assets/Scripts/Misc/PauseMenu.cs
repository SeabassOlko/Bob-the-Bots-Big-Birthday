using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    SaveLoad saveLoad;
    public GameObject pauseCanvas;
    public GameObject SettingsCanvas;

    public Button resumeButton;
    public Button quitButton;
    public Button loadButton;
    public Button saveButton;


    // Start is called before the first frame update
    void Start()
    {
        saveLoad = FindObjectOfType<SaveLoad>();
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(quitGame);
        loadButton.onClick.AddListener(LoadGame);
        saveButton.onClick.AddListener(SaveGame);
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;

        }

    }

    void LoadGame()
    {
        saveLoad.LoadGame();
        ResumeGame();
    }

    void SaveGame()
    {
        saveLoad.SaveGame();
        ResumeGame();
    }

    void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    void quitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}
