using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool isPaused = false;
    public GameObject pauseMenu;
    //[HideInInspector]
    public bool canPause = true;
    public bool hideOnCantPause = false;
    public bool hudMenu = false;
    public bool talking = false;
    public GameObject savedPopUp;
    public GameObject optionsMenu;
    public int[] lowpass = { 5000, 300 };
    public AudioMixer mixer;
    private void Start()
    {
        isPaused = false;
        hudMenu = false;
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
        if ((isPaused || hudMenu || talking) && canPause)
        {
            Cursor.visible = true;
            Screen.lockCursor = false;
            Time.timeScale = 0f;
            mixer.SetFloat("lowpass", lowpass[1]);

        }
        else
        {
            if (canPause)
            {
                Cursor.visible = false;
                Screen.lockCursor = true;
                Time.timeScale = 1f;
                mixer.SetFloat("lowpass", lowpass[0]);
            }
            else
            {
                Cursor.visible = !hideOnCantPause;
                Screen.lockCursor = hideOnCantPause;
                Time.timeScale = 1f;
                mixer.SetFloat("lowpass", lowpass[0]);
            }
        }
	}
    public void unFreezeEverything()
    {
        if(FindObjectOfType<FullQuestDisplay>() != null)
        {
            FindObjectOfType<FullQuestDisplay>().HUDMenu(false);
            canPause = false;
        }
    }
    public void Resume()
    {
        if (savedPopUp != null)
        {
            savedPopUp.SetActive(false);
        }
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Screen.lockCursor = true;
       
        isPaused = false;
    }
    void Pause()
    {
        if (savedPopUp != null)
        {
            savedPopUp.SetActive(false);
        }
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
        }
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
