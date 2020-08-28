using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPlayer : MonoBehaviour
{
    QuestManager manager;

    void Start()
    {
        manager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.quests[1].active)
        {
            if (FindObjectOfType<NanTeleport>().nanGate)
            {
                manager.EndQuest(1);
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
        }
    }
}
