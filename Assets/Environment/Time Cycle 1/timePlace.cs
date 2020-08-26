using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class timePlace
{
    public float TimeOfDay;
    public int dayNum;
    
    public timePlace(float _TimeOfDay, int _dayNum)
    {
        TimeOfDay = _TimeOfDay;
        dayNum = _dayNum;
    }
}
