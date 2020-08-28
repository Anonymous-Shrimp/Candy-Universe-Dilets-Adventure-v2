using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    Ouch player;
    private void Start()
    {
        player = GetComponent<Ouch>();
        foreach(Quest q in quests)
        {
            q.defaultAction.Invoke();
            if (q.active && !q.started)
            {
                q.startingAction.Invoke();
            }else if (q.active && q.started)
            {
                q.activeDuringQuest.Invoke();
            }
        }
    }
    public void StartQuest(int questIndex)
    {
        try
        {
            if (!quests[questIndex].completed && !quests[questIndex].active)
            {
                quests[questIndex].active = true;
            }
            quests[questIndex].startingAction.Invoke();
        }
        catch
        {
            throw;
        }
    }
    
    public void EndQuest(int questIndex)
    {
        try
        {
            if (quests[questIndex].active && !quests[questIndex].completed)
            {
                quests[questIndex].completed = true;
                quests[questIndex].active = false;
                quests[questIndex].endingAction.Invoke();
            }
        }
        catch
        {
            throw;
        }
    }
    
}
