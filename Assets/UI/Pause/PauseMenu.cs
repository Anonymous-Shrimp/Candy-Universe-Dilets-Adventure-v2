using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenu;
    [HideInInspector]
    public bool canPause = true;
    private void Start()
    {
        isPaused = false;
    }
    void Update () {
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
        }
        else
        {
            Cursor.visible = false;
            Screen.lockCursor = true;
        }
	}
    public void Resume()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Screen.lockCursor = true;
        Cursor.visible = true;
        Screen.lockCursor = false;
    }
    void Pause()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

    }
    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        Cursor.visible = true;
        Screen.lockCursor = false;
    }
    public void quitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
