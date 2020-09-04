using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Dictionary<string, KeyCode> keys;

    public GameData(Dictionary<string, KeyCode> key)
    {
        keys = key;
    }
}

