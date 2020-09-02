using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class TelidResearchItem
{
    public string name;
    public string descrip;
    public int candyCost;
    public bool completed = false;
    public bool active = false;
    public bool questActive = false;
    public Text candyTextAmount;
    public Text title;
    public Text descripText;
    public Sprite UISprite;
    public enum ResearchType { Attack,Movement,Defense};
    public ResearchType researchType;
    public int questIndex;
    public GameObject activeText;
}
