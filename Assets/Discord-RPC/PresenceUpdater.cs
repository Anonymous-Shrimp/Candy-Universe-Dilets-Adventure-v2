using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DiscordPresence;
using System;

public class PresenceUpdater : MonoBehaviour
{
    public string state = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        string detail = "";
        if(sceneName == "Fire" || sceneName == "Water" || sceneName == "Thunder" || sceneName == "Rock" || sceneName == "Ice" || sceneName == "Nan")
        {
            state = "In " + sceneName + " Dungeon";
        }
        else if(sceneName == "Overworld 3")
        {
            state = "In Overworld";
        }
        else
        {
            state = "In " + sceneName;
        }
        print(state);
        PresenceManager.UpdatePresence(detail: detail, state: state);
    }
}
