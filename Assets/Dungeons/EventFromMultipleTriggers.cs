using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventFromMultipleTriggers : MonoBehaviour
{
    public bool[] conditions;
    public UnityEvent endingEvent;
    bool completed;

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            completed = true;
            foreach (bool b in conditions)
            {
                if (!b)
                {
                    completed = false;
                }
            }
            if (completed)
            {
                endingEvent.Invoke();
            }
        }
    }
    public void activateCondition(int index)
    {
        conditions[index] = true;
    }
}
