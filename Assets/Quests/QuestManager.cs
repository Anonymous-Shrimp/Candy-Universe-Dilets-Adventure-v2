using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    public questDisplay display;
    public List<QuestItem> questItems;
    Ouch player;
    public GameObject HUDQuestDisplayItem;
    public GameObject HUDParent;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && FindObjectOfType<PauseMenu>().hudMenu)
        {
            refreshPage();
        }
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
        refreshPage();
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
            
            display.addShowing(quests[questIndex].name, quests[questIndex].description, progressText, quests[questIndex].questType);

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
            
            FindObjectOfType<questDisplay>().addShowing(quests[questIndex].name, quests[questIndex].description, progressText, quests[questIndex].questType);
        }
        refreshPage();


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
        
        display.addShowing(quests[questIndex].name, quests[questIndex].description, progressText, quests[questIndex].questType);
        if (quests[questIndex].progress >= quests[questIndex].progressMax)
        {
            EndQuest(questIndex);
        }
        refreshPage();
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
        
        display.addShowing(quests[questIndex].name, quests[questIndex].description, progressText, quests[questIndex].questType);
        if(quests[questIndex].progress >= quests[questIndex].progressMax)
        {
            EndQuest(questIndex);
        }
        refreshPage();
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
        refreshPage();
    }
    public void refreshPage()
    {
        foreach (QuestItem q in questItems) {
            Destroy(q.gameObject);

        }
        questItems.RemoveRange(0, questItems.ToArray().Length);
        
        foreach (Quest q in quests) {
            if (q.active || q.completed)
            {
                QuestItem qi = Instantiate(HUDQuestDisplayItem, HUDParent.transform).GetComponent<QuestItem>();
                questItems.Add(qi);
                qi.updateValues(q);

            }
                }
        List<QuestItem> qs;
        qs = new List<QuestItem>();
        foreach(QuestItem q in questItems)
        {
            qs.Add(q);
        }
        foreach(QuestItem q in qs)
        {
            if (q.completed)
            {
                questItems.Remove(q);
                questItems.Add(q);
            }
        }
        
        foreach(QuestItem q in questItems)
        {
            q.gameObject.name = q.title;
            q.transform.SetAsLastSibling();
        }
        

    }

    IEnumerator saveAfterTime()
    {
        yield return new WaitForSeconds(3);
        player.SavePlayer();
        yield return null;
    }
    
}
