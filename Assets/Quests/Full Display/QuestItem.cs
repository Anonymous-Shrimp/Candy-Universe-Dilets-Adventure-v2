using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    public Text titleText;
    public Text descripText;
    public Text detailedDescripText;
    public Text progressText;
    public Image typeImage;
    [Space]
    questDisplay display;

    public string title;
    public string descip;
    public string detailedDescrip;
    public string progress;
    public Quest.QuestType questType;

    private void Start()
    {
        display = FindObjectOfType<questDisplay>();
        
    }
    // Update is called once per frame
    void Update()
    {
       
        titleText.text = title;
        descripText.text = descip;
        detailedDescripText.text = detailedDescrip;
        progressText.text = progress;
        if (questType == Quest.QuestType.Main)
        {
            typeImage.color = display.mainQuest;
        }
        else if (questType == Quest.QuestType.Side)
        {
            typeImage.color = display.sideQuest;
        }
        else
        {
            typeImage.color = display.tiledQuest;
        }
    }
    public void updateValues(Quest q)
    {
        title = q.name;
        descip = q.description;
        detailedDescrip = q.detailedDescription;
        progress = q.progress.ToString() + " / " + q.progressMax.ToString();
        questType = q.questType;
    }
}
