using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData {

    public int health;
    public bool water;
    public bool thunder;
    public bool fire;
    public bool ice;
    public bool rock;
    public bool nan;
    public bool start;
    public bool inDungeon;
    public float[] position;
    public float timeOfDay;
    public int dayNum;
    public int candyAmount;
    public QuestData[] questData;

     public PlayerData(Ouch player, timePlace time, int ammount, QuestData[] _questData)
    {
   
        water = player.water;
        thunder = player.thunder;
        fire = player.fire;
        ice = player.ice;
        rock = player.rock;
        nan = player.nan;
        start = player.start;
        inDungeon = player.inDungeon;
        health = player.health;
        dayNum = time.dayNum;
        timeOfDay = time.TimeOfDay;
        candyAmount = ammount;
        questData = _questData;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }


}
[System.Serializable]
public class QuestData
{
    public bool active;
    public bool started;
    public bool completed;
    public int progress;
    public QuestData(bool _active, bool _started, bool _completed, int _progress)
    {
        active = _active;
        started = _started;
        completed = _completed;
        progress = _progress;
    }
}
