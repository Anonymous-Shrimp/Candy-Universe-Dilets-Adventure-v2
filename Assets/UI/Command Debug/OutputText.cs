using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class OutputText : MonoBehaviour
{
    public List<string> texts;
    Text textUI;
    
    void Start()
    {
        textUI = GetComponent<Text>();
    }
    
    void Update()
    {
        string outputText = "";
        foreach(string s in texts)
        {
            if (s != "")
            {
                outputText += "\n" + s;
            }
        }
        textUI.text = outputText;
    }
    public void addItem(string item)
    {
        texts.Add(item);
    }
    
}
