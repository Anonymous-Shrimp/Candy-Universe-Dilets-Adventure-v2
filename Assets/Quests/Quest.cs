using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public string name;
    public string description;
    public enum QuestType { Main, Side };
    public QuestType questType;
    public UnityEvent defaultAction;
    public UnityEvent startingAction;
    public UnityEvent activeDuringQuest;
    public UnityEvent endingAction;
    public bool active = false;
    public bool started = false;
    public bool completed = false;
}
