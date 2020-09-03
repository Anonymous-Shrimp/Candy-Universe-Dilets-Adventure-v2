using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialouge : MonoBehaviour
{
    bool talkZone = false;
    bool talking = false;

    int index;

    public string Speaker;

    public string[] dialouge;
    public UnityEvent finalAction;

    DialougeUI UI;
    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<DialougeUI>();
    }

    // Update is called once per frame
    void Update()
    {
        talkZone = GetComponentInChildren<TalkArea>().talkArea;
        if (((talkZone && Input.GetKeyDown(KeyCode.V)) || talking) && !FindObjectOfType<PauseMenu>().hudMenu && FindObjectOfType<PauseMenu>().canPause)
        {
            talking = true;
            UI.active = true;
            UI.speaker = Speaker;
            FindObjectOfType<PauseMenu>().talking = true;
            UI.speech = dialouge[index];
            if (Input.GetKeyDown(KeyCode.Space))
            {
                index++;
                try
                {
                    UI.speech = dialouge[index];
                }
                catch
                {
                    talking = false;
                    UI.active = false;
                    FindObjectOfType<PauseMenu>().talking = false;

                    finalAction.Invoke();

                    index = 0;
                }
            }
        }
        }
}
