﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]

public class Keybind : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text[] buttonTexts;
    private GameObject currentKey;

 



    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Run", KeyCode.LeftShift);
        keys.Add("Slap", KeyCode.Mouse0);
        keys.Add("Zoom", KeyCode.Mouse1);
        keys.Add("Show Candy", KeyCode.E);
        keys.Add("Eat Candy", KeyCode.F);
        keys.Add("HUD Menu", KeyCode.C);
        keys.Add("Enlarge Minimap", KeyCode.Q);
        keys.Add("Switch Minimap View", KeyCode.R);
        keys.Add("Interact", KeyCode.V);
        keys.Add("Skip", KeyCode.Tab);
        //keys.Add("Pause", KeyCode.Escape);
        Debug.Log(keys.ToString());
        LoadFile();
        buttonTexts[0].text = keys["Up"].ToString();
        buttonTexts[1].text = keys["Down"].ToString();
        buttonTexts[2].text = keys["Left"].ToString();
        buttonTexts[3].text = keys["Right"].ToString();
        buttonTexts[4].text = keys["Jump"].ToString();
        buttonTexts[5].text = keys["Run"].ToString();
        buttonTexts[6].text = keys["Slap"].ToString();
        buttonTexts[7].text = keys["Zoom"].ToString();
        buttonTexts[8].text = keys["Show Candy"].ToString();
        buttonTexts[9].text = keys["Eat Candy"].ToString();
        buttonTexts[10].text = keys["HUD Menu"].ToString();
        buttonTexts[11].text = keys["Enlarge Minimap"].ToString();
        buttonTexts[12].text = keys["Switch Minimap View"].ToString();
        buttonTexts[13].text = keys["Interact"].ToString();
        buttonTexts[14].text = keys["Skip"].ToString();
        //buttonTexts[15].text = keys["Pause"].ToString();


    }
    void Update()
    {
        SaveFile();
        foreach (Text t in buttonTexts)
        {
            if (currentKey != null)
            {
                if (t.gameObject.name != currentKey.gameObject.name)
                {
                    t.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                }
                else
                {
                    t.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.3f);
                }
            }
            else
            {
                t.gameObject.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0f);
            }
            t.color = new Color(1, 1, 1);
            
            foreach (Text td in buttonTexts)
            {
                if(keys[t.name] == keys[td.name] && t.name != td.name)
                {
                    td.color = new Color(1, 0, 0);
                    t.color = new Color(1, 0, 0);
                }
            }
            
        }
    }


    // Update is called once per frame
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }
    public void restoreDefaults()
    {
        keys["Up"] = KeyCode.W;
        keys["Down"] = KeyCode.S;
        keys["Left"] = KeyCode.A;
        keys["Right"] = KeyCode.D;
        keys["Jump"] = KeyCode.Space;
        keys["Run"] = KeyCode.LeftShift;
        keys["Slap"] = KeyCode.Mouse0;
        keys["Zoom"] = KeyCode.Mouse1;
        keys["Show Candy"] = KeyCode.E;
        keys["Eat Candy"] = KeyCode.F;
        keys["HUD Menu"] = KeyCode.C;
        keys["Enlarge Minimap"] = KeyCode.Q;
        keys["Switch Minimap View"] = KeyCode.R;
        keys["Interact"] = KeyCode.V;
        keys["Skip"] = KeyCode.Tab;
        //keys["Pause"] = KeyCode.Escape;
        buttonTexts[0].text = keys["Up"].ToString();
        buttonTexts[1].text = keys["Down"].ToString();
        buttonTexts[2].text = keys["Left"].ToString();
        buttonTexts[3].text = keys["Right"].ToString();
        buttonTexts[4].text = keys["Jump"].ToString();
        buttonTexts[5].text = keys["Run"].ToString();
        buttonTexts[6].text = keys["Slap"].ToString();
        buttonTexts[7].text = keys["Zoom"].ToString();
        buttonTexts[8].text = keys["Show Candy"].ToString();
        buttonTexts[9].text = keys["Eat Candy"].ToString();
        buttonTexts[10].text = keys["HUD Menu"].ToString();
        buttonTexts[11].text = keys["Enlarge Minimap"].ToString();
        buttonTexts[12].text = keys["Switch Minimap View"].ToString();
        buttonTexts[13].text = keys["Interact"].ToString();
        buttonTexts[14].text = keys["Skip"].ToString();
        //buttonTexts[15].text = keys["Pause"].ToString();
    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/control.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(keys);
        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/control.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            keys = data.keys;
        }
        else
        {
            Debug.LogError("File not found");
            return;
        }

        

    }
}
