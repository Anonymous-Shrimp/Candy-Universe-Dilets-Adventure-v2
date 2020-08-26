using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingScreen;
    public Animator anim;
    public PauseMenu pause;

    

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void LoadLevelString(string sceneName)
    {
        StartCoroutine(LoadAsynchronouslyString(sceneName));
    }
    public void LoadSameLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));

    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        if(pause != null)
        {
            pause.Resume();
        }

        
        loadingScreen.SetActive(true);
        if (pause != null)
        {
            pause.canPause = false;
        }
        Debug.Log("Switch scene to " + sceneIndex);
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
    IEnumerator LoadAsynchronouslyString(string sceneName)
    {

        if (pause != null)
        {
            pause.Resume();
        }


        loadingScreen.SetActive(true);
        if (pause != null)
        {
            pause.canPause = false;
        }
        Debug.Log("Switch scene to " + sceneName);
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);



        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        loadingScreen.SetActive(false);
        if (pause != null)
        {
            pause.canPause = true;
        }
    }
    public void quit()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(wait(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
