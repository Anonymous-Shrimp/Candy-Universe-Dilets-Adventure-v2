using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.ComponentModel;

[System.Serializable]
public class Quest
{
    public enum QuestType { Main, Side, Telid };
    public enum DesciptionType { Static, Dynamic };

    [Header("Basics")]
    public string name;
    public string description;

    [Header("Descriptions")]
    
    public DesciptionType desciptionType;
    public string detailedDescription;
    public string[] detailedDescriptions;

    [Header("Quest Data")]
    
    public QuestType questType;

    [Header("Progress")]
    public bool active = false;
    public bool started = false;
    public int progress = 0;
    public int progressMax;
    public bool completed = false;

    [Header("Events")]
    public UnityEvent defaultAction;
    public UnityEvent startingAction;
    public UnityEvent activeDuringQuest;
    public UnityEvent endingAction;

    
}

