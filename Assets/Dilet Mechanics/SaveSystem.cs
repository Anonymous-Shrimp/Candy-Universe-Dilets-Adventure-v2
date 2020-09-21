using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    
    public static void SavePlayer(Ouch player, timePlace time, int ammt, QuestData[] questData, ResearchData telidData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/progression.dilet";
        FileStream stream = new FileStream (path, FileMode.Create);
        
        PlayerData data = new PlayerData(player, time, ammt, questData, telidData);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/progression.dilet";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            Debug.Log("Loaded File From " + path);

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null; 
        }
    }
    public static bool checkFile()
    {
        string path = Application.persistentDataPath + "/progression.dilet";
        return File.Exists(path);
    }
}
