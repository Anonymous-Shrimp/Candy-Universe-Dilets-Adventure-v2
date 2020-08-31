using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public string detailedDescription;

    public enum QuestType { Main, Side, Telid };
    public QuestType questType;
    public UnityEvent defaultAction;
    public UnityEvent startingAction;
    public UnityEvent activeDuringQuest;
    public UnityEvent endingAction;
    public bool active = false;
    public bool started = false;
    public int progress = 0;
    public int progressMax;
    public bool completed = false;
}
