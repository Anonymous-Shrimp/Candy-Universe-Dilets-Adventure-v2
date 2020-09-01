using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullQuestDisplay : MonoBehaviour
{
    PauseMenu pause;
    public GameObject hudMenu;
    public GameObject[] HUDMenuTabs;
    public int tab;
    private int previousTab;
    public GameObject telidTab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in HUDMenuTabs)
        {
            g.SetActive(false);
        }
        HUDMenuTabs[tab].SetActive(true);
        previousTab = tab;
        pause = FindObjectOfType<PauseMenu>();
        hudMenu.SetActive(pause.hudMenu);

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            telidTab.SetActive(FindObjectOfType<QuestManager>().quests[4].completed);
        }
        catch
        {

        }
        if (!FindObjectOfType<QuestManager>().quests[4].completed)
        {
            tab = 0;
        }
        if (Input.GetKeyDown(KeyCode.C) && pause.canPause)
        {
            pause.hudMenu = !pause.hudMenu;
            try
            {
                FindObjectOfType<TelidResearch>().cantBuy.SetActive(false);
            }
            catch
            {

            }
            hudMenu.SetActive(pause.hudMenu);

            try
            {
                FindObjectOfType<TelidResearch>().cantBuy.SetActive(false);
            }
            catch
            {

            }
        }
        if (previousTab != tab)
        {
            foreach (GameObject g in HUDMenuTabs)
            {
                g.SetActive(false);
            }
            HUDMenuTabs[tab].SetActive(true);
            previousTab = tab;
        }
    }
    public void SwitchTab(int index)
    {
        try
        {
            FindObjectOfType<TelidResearch>().cantBuy.SetActive(false);
        }
        catch
        {

        }
        tab = index;
        try
        {
            FindObjectOfType<TelidResearch>().cantBuy.SetActive(false);
        }
        catch
        {

        }
    }
    public void HUDMenu(bool show)
    {
        pause.hudMenu = show;
        hudMenu.SetActive(show);
    }
}
