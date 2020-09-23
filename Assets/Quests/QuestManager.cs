using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public questDisplay display;
    public List<QuestItem> questItems;
    public List<Quest> displayQuests;
    Ouch player;
    public GameObject HUDQuestDisplayItem;
    public GameObject HUDParent;
    public Text questProgresses;

    public Quest[] quests;
   

    private void Update()
    {
        if(Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Show Candy"]) && FindObjectOfType<PauseMenu>().hudMenu)
        {
            refreshPage();
        }
        int numComplete = 0;
        foreach(Quest q in quests)
        {
            if (q.completed)
            {
                numComplete++;
            }
            if(q.desciptionType == Quest.DesciptionType.Static)
            {
                try
                {
                    q.detailedDescription = q.detailedDescriptions[0];
                }
                catch
                {
                    Debug.Log(q.name + " doesn't have a zeroth description");
                }
            }
            else
            {
                try
                {
                    q.detailedDescription = q.detailedDescriptions[q.progress];
                }
                catch
                {
                    q.detailedDescription = q.detailedDescriptions[q.detailedDescriptions.Length - 1];
                }
            }
        }
        questProgresses.text = numComplete.ToString() + " / " + quests.Length.ToString();
    }
    private void Start()
    {
        if (questProgresses != null)
        {
            foreach (Text t in FindObjectsOfType<Text>())
            {
                if (t.gameObject.name == "Progress")
                {
                    questProgresses = t;
                }
            }
        }
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
        if (!quests[questIndex].active)
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

    }
    public int getProgress(int questIndex)
    {
        return quests[questIndex].progress;
    }
    public void changeProgress(int questIndex, int progress)
    {
        if (quests[questIndex].progress < progress)
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
        }
        else
        {
            quests[questIndex].progress = progress;

        }
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
        List<Quest> qes = quests.ToList();
        qes.Sort(sortQuests);

        foreach (Quest q in qes) {
            if (q.active || q.completed)
            {
                QuestItem qi = Instantiate(HUDQuestDisplayItem, HUDParent.transform).GetComponent<QuestItem>();
                questItems.Add(qi);
                qi.updateValues(q);
                displayQuests.Add(q);
            }
                }
      
        foreach(QuestItem q in questItems)
        {
            q.gameObject.name = q.title;
        }
        

    }

    IEnumerator saveAfterTime()
    {
        yield return new WaitForSeconds(3);
        player.SavePlayer();
        yield return null;
    }
    static int sortQuests(Quest q1, Quest q2)
    {
        return q1.completed.CompareTo(q2.completed);
    }
    
}
