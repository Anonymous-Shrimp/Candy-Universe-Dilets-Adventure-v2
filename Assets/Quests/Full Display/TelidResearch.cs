using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelidResearch : MonoBehaviour
{
    QuestManager manager;
    public GameObject cantBuy;
    public Sprite questionSprite;
    public TelidResearchItem[] items;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
        FindObjectOfType<TelidResearch>().cantBuy.SetActive(false);
        foreach(TelidResearchItem t in items)
        {
            t.completed = manager.quests[t.questIndex].completed;
            t.questActive = manager.quests[t.questIndex].active;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(TelidResearchItem t in items)
        {
            if (t.completed)
            {
                t.title.text = t.name;
                t.descripText.text = t.descrip;
                t.candyTextAmount.gameObject.SetActive(false);
                t.title.gameObject.GetComponentInParent<Image>().sprite = t.UISprite;
            }
            else if(t.questActive)
            {
                t.title.text = "???";
                t.descripText.text = "Click to see quest page";
                t.candyTextAmount.gameObject.SetActive(false);
                t.candyTextAmount.text = t.candyCost.ToString();
                t.title.gameObject.GetComponentInParent<Image>().sprite = questionSprite;
            }
            else
            {
                t.title.text = "???";
                t.descripText.text = "";
                t.candyTextAmount.gameObject.SetActive(true);
                t.title.gameObject.GetComponentInParent<Image>().sprite = questionSprite;
            }
            t.activeText.SetActive(t.active);
        }
        foreach (TelidResearchItem t in items)
        {
            t.completed = manager.quests[t.questIndex].completed;
            t.questActive = manager.quests[t.questIndex].active;
        }
    }
    public void click(int index)
    {
        if (items[index].completed)
        {
            if (!items[index].active)
            {
                foreach (TelidResearchItem t in items)
                {
                    if (t.researchType == items[index].researchType)
                    {
                        t.active = false;
                    }

                }
                items[index].active = true;
            }
            else
            {
                items[index].active = false;
            }
        }
        else if(!items[index].questActive)
        {
            if(FindObjectOfType<CandyCounter>().targetAmount >= items[index].candyCost)
            {
                items[index].questActive = true;
                try
                {
                    manager.StartQuest(items[index].questIndex);
                    FindObjectOfType<CandyCounter>().changeAmount(-items[index].candyCost);
                    FindObjectOfType<FullQuestDisplay>().HUDMenu(false);
                }
                catch
                {
                    Debug.Log("Quest Not Found");
                }
            }
            else
            {
                cantBuy.SetActive(true);
            }
        }else if (items[index].questActive)
        {
            
            FindObjectOfType<FullQuestDisplay>().tab = 0;
        }
    }
}
