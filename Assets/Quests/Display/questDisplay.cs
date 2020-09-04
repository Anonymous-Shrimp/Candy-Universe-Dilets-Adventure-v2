using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questDisplay : MonoBehaviour
{
    public Animator anim;
    public Text title;
    public Text descrip;
    public Text progress;
    public Image questType;
    public Text questTypeLabel;

    public GameObject TABTip;

    public List<string> titleText;
    public List<string> descripText;
    public List<string> progressText;
    public List<Color> questIcon;
    public List<string> questTypeText;

    public Color mainQuest;
    public Color sideQuest;
    public Color tiledQuest;

    public bool canChange;
    public int showings;

    private float timer = 0f;
    public float disappearTime = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        timer = disappearTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        if(!(showings > 0))
        {
            timer = disappearTime;
        }
        anim.SetInteger("Showings", showings);
        AnimatorClipInfo[] a = anim.GetCurrentAnimatorClipInfo(0);
        if ((Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Skip"]) || timer <= 0) && a[0].clip.name == "Showed")
        {
            timer = disappearTime;
            anim.SetTrigger("Hide");
            showings -= 1;

            if(showings < 0)
            {
                showings = 0;
            }
        }
        if (canChange)
        {
            if (showings < titleText.ToArray().Length)
            {
                titleText.RemoveAt(0);
                descripText.RemoveAt(0);
                progressText.RemoveAt(0);
                questIcon.RemoveAt(0);
                questTypeText.RemoveAt(0);
            }
            if (titleText.ToArray().Length > 0)
            {
                title.text = titleText[0];
                descrip.text = descripText[0];
                progress.text = progressText[0];
                questType.color = questIcon[0];
                questTypeLabel.text = questTypeText[0];
                TABTip.SetActive(showings > 1);
            }

        }
        else if(!FindObjectOfType<PauseMenu>().hudMenu)
        {
            FindObjectOfType<FullQuestDisplay>().tab = 0;
        }

    }
    public void addShowing(string title, string descrip, string progress, Quest.QuestType questType)
    {
        titleText.Add(title);
        descripText.Add(descrip);
        progressText.Add(progress);
        if (questType == Quest.QuestType.Main)
        {
            questIcon.Add(mainQuest);
            questTypeText.Add("Main Quest");
        }
        else if(questType == Quest.QuestType.Side)
        {
            questIcon.Add(sideQuest);
            questTypeText.Add("Side Quest");
        }
        else
        {
            questIcon.Add(tiledQuest);
            questTypeText.Add("Telid Quest");
        }
        showings++;
    }
}
