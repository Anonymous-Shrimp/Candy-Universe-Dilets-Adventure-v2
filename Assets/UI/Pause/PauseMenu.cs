using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenu;
    //[HideInInspector]
    public bool canPause = true;
    public bool hideOnCantPause = false;
    public GameObject savedPopUp;
    private void Start()
    {
        isPaused = false;
    }
    void Update () {
        print(isPaused);
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (isPaused)
        {
            Cursor.visible = true;
            Screen.lockCursor = false;
            Time.timeScale = 0f;

        }
        else
        {
            if (canPause)
            {
                Cursor.visible = false;
                Screen.lockCursor = true;
                Time.timeScale = 1f;
            }
            else
            {
                Cursor.visible = !hideOnCantPause;
                Screen.lockCursor = hideOnCantPause;
                Time.timeScale = 1f;
            }
        }
	}
    public void Resume()
    {
        if (savedPopUp != null)
        {
            savedPopUp.SetActive(false);
        }
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Screen.lockCursor = true;
       
        isPaused = false;
    }
    void Pause()
    {
        
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Screen.lockCursor = false;
        isPaused = true;

    }
    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        
    }
    public void quitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
