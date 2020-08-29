using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    public questDisplay display;
    Ouch player;
    private void Update()
    {
    }
    private void Start()
    {
        display = FindObjectOfType<questDisplay>();
        player = GetComponent<Ouch>();
        foreach(Quest q in quests)
        {
            q.defaultAction.Invoke();
            if (q.active && q.started)
            {
                q.activeDuringQuest.Invoke();
            }
        }
    }
    public void StartQuest(int questIndex)
    {
        
            if (!quests[questIndex].completed)
            {
                quests[questIndex].active = true;
                quests[questIndex].started = true;
            }
            quests[questIndex].startingAction.Invoke();
        quests[questIndex].activeDuringQuest.Invoke();
        if (display != null)
        {
            string progressText;
            if (quests[questIndex].progress == 0)
            {
                progressText = "New Quest!";
            }
            else if (quests[questIndex].progress >= quests[questIndex].progressMax)
            {
                progressText = "Completed!";
            }
            else
            {
                progressText = quests[questIndex].progress.ToString() + " / " + quests[questIndex].progressMax.ToString();
            }
            bool questType = quests[questIndex].questType == Quest.QuestType.Main;
            display.addShowing(quests[questIndex].name, quests[questIndex].description, progressText, questType);

        }
        else
        {
            string progressText;
            if (quests[questIndex].progress == 0)
            {
                progressText = "New Quest!";
            }
            else if (quests[questIndex].progress >= quests[questIndex].progressMax)
            {
                progressText = "Completed!";
            }
            else
            {
                progressText = quests[questIndex].progress.ToString() + " / " + quests[questIndex].progressMax.ToString();
            }
            bool questType = quests[questIndex].questType == Quest.QuestType.Main;
            FindObjectOfType<questDisplay>().addShowing(quests[questIndex].name, quests[questIndex].description, progressText, questType);
        }
        
        
    }
    public int getProgress(int questIndex)
    {
        return quests[questIndex].progress;
    }
    public void changeProgress(int questIndex, int progress)
    {
        quests[questIndex].progress = progress;
        string progressText;
        if (quests[questIndex].progress == 0)
        {
            progressText = "New Quest!";
        }
        else if (quests[questIndex].progress >= quests[questIndex].progressMax)
        {
            progressText = "Completed!";
        }
        else
        {
            progressText = quests[questIndex].progress.ToString() + " / " + quests[questIndex].progressMax.ToString();
        }
        bool questType = quests[questIndex].questType == Quest.QuestType.Main;
        display.addShowing(quests[questIndex].name, quests[questIndex].description, progressText, questType);
        if (quests[questIndex].progress >= quests[questIndex].progressMax)
        {
            EndQuest(questIndex);
        }
    }
    public void changeProgressByOne(int questIndex)
    {
        quests[questIndex].progress += 1;
        string progressText;
        if (quests[questIndex].progress == 0)
        {
            progressText = "New Quest!";
        }else if(quests[questIndex].progress >= quests[questIndex].progressMax)
        {
            
            progressText = "Completed!";
        }
        else
        {
            progressText = quests[questIndex].progress.ToString() + " / " + quests[questIndex].progressMax.ToString();
        }
        bool questType = quests[questIndex].questType == Quest.QuestType.Main;
        display.addShowing(quests[questIndex].name, quests[questIndex].description, progressText, questType);
        if(quests[questIndex].progress >= quests[questIndex].progressMax)
        {
            EndQuest(questIndex);
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
                quests[questIndex].started = false;
                quests[questIndex].endingAction.Invoke();
            }
        }
        catch
        {
            throw;

        }
        StartCoroutine(saveAfterTime());
        
    }
    IEnumerator saveAfterTime()
    {
        yield return new WaitForSeconds(3);
        player.SavePlayer();
        yield return null;
    }
    
}
