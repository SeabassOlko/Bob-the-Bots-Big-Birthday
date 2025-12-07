using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditButton : MonoBehaviour
{
    SaveLoad saveLoad;
    public GameObject pauseCanvas;
   
    public Button quitButton;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        quitButton.onClick.AddListener(quitGame);
        pauseCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {


    }



    void quitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
}
