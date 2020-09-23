using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestPlayer : MonoBehaviour
{
    
    QuestManager manager;
    public QuestData[] questData;

    [Header("Tiled Quest")]
    public bool HasTiledCandy;
    public GameObject TiledCandy;

    bool inSunset = false;

    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
        HasTiledCandy = manager.quests[4].progress == 1;
        if(manager.quests[4].progress > 1)
        {
            Destroy(TiledCandy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!GetComponent<Ouch>().introduction && !GetComponent<Ouch>().inDungeon)
            if (manager.quests[1].active)
        {
            if (FindObjectOfType<NanTeleport>().nanGate)
            {

                manager.changeProgress(0,1);
            }
        }
        if (FindObjectOfType<TimeCycle>() != null)
        {
            if (inSunset && FindObjectOfType<TimeCycle>().currentTimeOfDay > 0.73f && FindObjectOfType<TimeCycle>().currentTimeOfDay < 0.75f)
            {
                manager.changeProgress(11, 1);
            }
            if (FindObjectOfType<TimeCycle>().currentTimeOfDay > 0.2f && FindObjectOfType<TimeCycle>().currentTimeOfDay < 0.77f && manager.getProgress(12) != 0)
            {
                manager.changeProgress(12, 0);
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Quest"))
        {
            if(other.gameObject.name == "PoyoEscape")
            {
                manager.EndQuest(0);
            }
            if (other.gameObject.name == "TiledCandy")
            {
                manager.changeProgress(4,1);
                HasTiledCandy = true;
                Destroy(other.gameObject);
            }
            if(other.gameObject.name == "KinemaKey")
            {
                manager.changeProgress(8, 1);
                Destroy(other.gameObject);
            }
            if (other.gameObject.name == "CaperKey")
            {
                manager.changeProgress(7, 1);
                Destroy(other.gameObject);
            }
            if(other.gameObject.name == "Sunset")
            {
                inSunset = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Quest"))
        {
            if (other.gameObject.name == "Sunset")
            {
                inSunset = false;
            }
        }
    }

}
