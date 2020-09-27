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
    bool cursorLock;
    public bool cursorLockOverride;
    private void Start()
    {
        isPaused = false;
        hudMenu = false;
        cursorLockOverride = false;
    }
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause && FindObjectOfType<Keybind>().currentKey == null)
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
            cursorLock = false;
            Time.timeScale = 0f;
            mixer.SetFloat("lowpass", lowpass[1]);

        }
        else
        {
            if (canPause)
            {
                
               cursorLock = true;
                Time.timeScale = 1f;
                mixer.SetFloat("lowpass", lowpass[0]);
            }
            else
            {
                cursorLock = hideOnCantPause;
                Time.timeScale = 1f;
                mixer.SetFloat("lowpass", lowpass[0]);
            }
        }
        Cursor.visible = !(cursorLock && !cursorLockOverride);
        if (cursorLock && !cursorLockOverride)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
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
