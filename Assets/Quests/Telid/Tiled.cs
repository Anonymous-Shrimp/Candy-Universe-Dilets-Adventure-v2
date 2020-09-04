using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiled : MonoBehaviour
{
    public bool talkZone = false;
    public bool talking = false;
    public bool firstTime;
    public bool questComplete;
    public string[] firstTimeSpeech;
    public string[] regularSpeech;
    public string[] questSpeech;
    public string[] questSpeech2;

    int index = 0;
    DialougeUI dialouge;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = !FindObjectOfType<QuestManager>().quests[3].completed;
        questComplete = FindObjectOfType<QuestManager>().quests[4].completed;
        dialouge = FindObjectOfType<DialougeUI>();
    }

    // Update is called once per frame
    void Update()
    {
        talkZone = GetComponentInChildren<TalkArea>().talkArea;
        if(((talkZone && Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Interact"])) || talking) && !FindObjectOfType<PauseMenu>().hudMenu && FindObjectOfType<PauseMenu>().canPause)
        {
            talking = true;
            dialouge.active = true;
            dialouge.speaker = "Tiled";
            FindObjectOfType<PauseMenu>().talking = true;
            if (firstTime)
            {

                dialouge.speech = firstTimeSpeech[index];
                if (Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Jump"]))
                {
                    index++;
                    try
                    {
                        dialouge.speech = firstTimeSpeech[index];
                    }
                    catch
                    {
                        talking = false;
                        dialouge.active = false;
                        firstTime = false;
                        FindObjectOfType<PauseMenu>().talking = false;

                        index = 0;
                        
                        FindObjectOfType<QuestManager>().changeProgressByOne(3);
                        FindObjectOfType<QuestManager>().EndQuest(3);
                        FindObjectOfType<QuestManager>().StartQuest(4);
                    }
            }
            }
            else if(!questComplete)
            {
                if (FindObjectOfType<QuestPlayer>().HasTiledCandy)
                {
                    dialouge.speech = questSpeech2[index];
                    if (Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Jump"]))
                    {
                        index++;
                        try
                        {
                            dialouge.speech = questSpeech2[index];
                        }
                        catch
                        {
                            talking = false;
                            dialouge.active = false;
                            FindObjectOfType<PauseMenu>().talking = false;
                            FindObjectOfType<QuestManager>().changeProgress(4, 2);
                            if (!FindObjectOfType<PauseMenu>().hudMenu)
                            {
                                FindObjectOfType<FullQuestDisplay>().tab = 1;
                                FindObjectOfType<FullQuestDisplay>().HUDMenu(true);
                            }
                            index = 0;
                        }
                    }
                }
                else
                {
                    dialouge.speech = questSpeech[index];
                    if (Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Jump"]))
                    {
                        index++;
                        try
                        {
                            dialouge.speech = questSpeech[index];
                        }
                        catch
                        {
                            talking = false;
                            dialouge.active = false;
                            FindObjectOfType<PauseMenu>().talking = false;

                            index = 0;
                        }
                    }
                }
            }
            else
            {
                dialouge.speech = regularSpeech[index];
                if (Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Jump"]))
                {
                    index++;
                    try
                    {
                        dialouge.speech = regularSpeech[index];
                    }
                    catch
                    {
                        talking = false;
                        dialouge.active = false;
                        FindObjectOfType<PauseMenu>().talking = false;
                        if (!FindObjectOfType<PauseMenu>().hudMenu)
                        {
                            FindObjectOfType<FullQuestDisplay>().tab = 1;
                            FindObjectOfType<FullQuestDisplay>().HUDMenu(true);
                        }

                        index = 0;
                    }
                }
            }
            
        }
    }
    public void changeQuestStatus(bool quest)
    {
        questComplete = quest;
    }
    
}
