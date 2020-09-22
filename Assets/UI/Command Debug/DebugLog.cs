using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class DebugLog : MonoBehaviour
{
    public enum varibleType { String, Float, Bool };
    public GameObject UIObject;
    public Command[] commands;
    InputField input;
    OutputText outputText;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponentInChildren<InputField>();
        outputText = GetComponentInChildren<OutputText>();
    }

    // Update is called once per frame
    void Update()
    {
        UIObject.SetActive(CheatData.CheckCheat());
    }
    public void entered(string inputText)
    {
        input.text = "";
        outputText.texts.Add(inputText);
        string[] parameters = inputText.Split(' ');
        if (parameters[0].Contains('/'))
        {
            bool command = false;
            foreach (Command c in commands)
            {
                if (parameters[0] == "/" + c.callCommand)
                {
                    executeCommand(commands.ToList().IndexOf(c), parameters);
                    command = true;
                }
            }
            if (!command)
            {
                Debug.LogError("Invalid Syntax");
                outputText.texts.Add("Invalid Syntax");
            }
        }
        input.Select();
        input.ActivateInputField();
    }
    void executeCommand(int cIndex, string[] parameters)
    {
        Command command = commands[cIndex];

        if (cIndex == 0)
        {
            if (parameters.Length == 2)
            {
                if (checkVariable(parameters[1], varibleType.Bool))
                {
                    FindObjectOfType<Ouch>().godMode = toBool(parameters[1], false);
                    outputText.texts.Add("God Mode set to " + toBool(parameters[1], false).ToString());
                }
                else
                {
                    invalidSyntax();
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if (cIndex == 1)
        {
            if (checkVariables(parameters, command.varibleType))
            {
                if (float.Parse(parameters[2]) == Mathf.RoundToInt(float.Parse(parameters[2])))
                {

                    if (parameters[1] == "set")
                    {
                        if (Mathf.RoundToInt(float.Parse(parameters[2])) <= 9999)
                        {
                            FindObjectOfType<CandyCounter>().targetAmount = Mathf.RoundToInt(float.Parse(parameters[2]));
                            outputText.texts.Add("Set the number of candy to " + Mathf.RoundToInt(float.Parse(parameters[2])).ToString());
                        }
                        else
                        {
                            invalidSyntaxMessage("Max Candy is 9999");
                        }
                    }
                    else if (parameters[1] == "give")
                    {
                        if (Mathf.RoundToInt(float.Parse(parameters[2])) + FindObjectOfType<CandyCounter>().targetAmount <= 9999)
                        {
                            FindObjectOfType<CandyCounter>().targetAmount += Mathf.RoundToInt(float.Parse(parameters[2]));
                            outputText.texts.Add("Gave " + Mathf.RoundToInt(float.Parse(parameters[2])).ToString() + " candy to player");
                        }
                        else
                        {
                            invalidSyntaxMessage("Max Candy is 9999. Current candy is: " + FindObjectOfType<CandyCounter>().targetAmount.ToString());
                        }
                    }
                    else
                    {
                        invalidSyntaxMessage("Must write set or give");
                    }
                }
                else
                {
                    invalidSyntaxMessage("Please enter int");
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if (cIndex == 2)
        {
            if (checkVariables(parameters, command.varibleType))
            {
                if (parameters.Length == 1)
                {
                    outputText.texts.Clear();
                }
                else
                {
                    invalidSyntax();
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if (cIndex == 3)
        {
            if (checkVariables(parameters, command.varibleType))
            {
                if (parameters.Length == 2)
                {
                    if (float.Parse(parameters[1]) <= 1 && float.Parse(parameters[1]) >= 0)
                    {
                        FindObjectOfType<TimeCycle>().currentTimeOfDay = float.Parse(parameters[1]);
                        outputText.texts.Add("Set the time to " + float.Parse(parameters[1]));
                    }
                    else
                    {
                        invalidSyntaxMessage("Enter float between 0 - 1");
                    }
                }
                else
                {
                    invalidSyntax();
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if (cIndex == 4)
        {
            if (checkVariables(parameters, command.varibleType))
            {
                if (parameters.Length == 2)
                {
                    if (float.Parse(parameters[1]) == Mathf.RoundToInt(float.Parse(parameters[1])))
                    {
                        if (float.Parse(parameters[1]) <= 1 && float.Parse(parameters[1]) >= 9)
                        {
                            FindObjectOfType<TimeCycle>().dayNum = Mathf.RoundToInt(float.Parse(parameters[1]));
                            outputText.texts.Add("Set the day to " + Mathf.RoundToInt(float.Parse(parameters[1])).ToString());
                        }
                        else
                        {
                            invalidSyntaxMessage("Enter float between 1 - 9");
                        }
                    }
                    else
                    {
                        invalidSyntaxMessage("Value must be a integer");
                    }
                }
                else
                {
                    invalidSyntax();
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if (cIndex == 5)
        {
            if (checkVariables(parameters, command.varibleType))
            {
                outputText.texts.Add("Help Menu");
                foreach (Command c in commands)
                {
                    outputText.texts.Add(c.name + ": " + c.helpText);
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if (cIndex == 6)
        {
            QuestManager questManager = FindObjectOfType<QuestManager>();
            if (checkVariables(parameters, command.varibleType))
            {
                if (checkInt(float.Parse(parameters[1])))
                {
                    int questindex = Mathf.RoundToInt(float.Parse(parameters[1]));
                    if (questindex >= 0 && questindex <= questManager.quests.Length - 1)
                    {
                        if (checkInt(float.Parse(parameters[2])))
                        {
                            int questProgress = Mathf.RoundToInt(float.Parse(parameters[2]));
                            if(questProgress >= 0 && questProgress <= questManager.quests[questindex].progressMax)
                            {
                                if(questManager.quests[questindex].progress == 0)
                                {
                                    questManager.StartQuest(questindex);
                                    questManager.changeProgress(questindex, questProgress);
                                    outputText.texts.Add("Change quest no. " + questindex.ToString() + " progress to " + questProgress.ToString());
                                }
                                else
                                {
                                    questManager.changeProgress(questindex, questProgress);
                                }
                            }
                            else
                            {
                                invalidSyntaxMessage("Must declare quest progress (int) between: 0 - " + (questManager.quests[questindex].progressMax).ToString());

                            }
                        }
                        else
                        {
                            invalidSyntaxMessage("Quest progress must be a integer");
                        }
                    }
                    else
                    {
                        invalidSyntaxMessage("Must declare quest index (int) between: 0 - " + (questManager.quests.Length - 1).ToString());
                    }
                }
                else
                {
                    invalidSyntaxMessage("Must declare quest index (int)");
                }
            }
            else
            {
                invalidSyntax();
            }
        }
        else if(cIndex == 7)
        {
            QuestManager questManager = FindObjectOfType<QuestManager>();
            TelidResearch telidResearch = FindObjectOfType<TelidResearch>();
            if (checkVariables(parameters, command.varibleType))
            {
                if (checkInt(float.Parse(parameters[1])))
                {
                    int researchIndex = Mathf.RoundToInt(float.Parse(parameters[1]));
                    if(researchIndex >= 0 && researchIndex <= telidResearch.items.Length - 1)
                    {
                        if (!telidResearch.items[researchIndex].completed)
                        {
                            if (telidResearch.items[researchIndex].questActive)
                            {
                                questManager.EndQuest(telidResearch.items[researchIndex].questIndex);
                            }
                            else
                            {
                                questManager.StartQuest(telidResearch.items[researchIndex].questIndex);
                                questManager.changeProgress(telidResearch.items[researchIndex].questIndex, questManager.quests[telidResearch.items[researchIndex].questIndex].progressMax);
                                
                            }

                        }
                        outputText.texts.Add("Change research no. " + researchIndex.ToString() + " to active");
                    }
                    else
                    {
                        invalidSyntaxMessage("Reserch index must be between 0 - " + (telidResearch.items.Length - 1).ToString());
                    }
                }
                else
                {
                    invalidSyntaxMessage("Research index must be a integer");
                }
                
            }
            else
            {
                invalidSyntax();
            }
        }
        else if(cIndex == 8)
        {
            if(checkVariables(parameters, command.varibleType))
            {
                if (checkInt(float.Parse(parameters[1])))
                {
                    int index = Mathf.RoundToInt(float.Parse(parameters[1]));
                    outputText.texts.Add(FindObjectOfType<QuestManager>().quests[index].name);
                }
            }
            else
            {
                invalidSyntax();
            }
        }
    }
    bool checkVariables(string[] inputStrings, varibleType[] varibleTypes)
    {
        bool valid = true;
        if (inputStrings.Length - 1 == varibleTypes.Length)
        {
            for (int i = 0; i < varibleTypes.Length - 1; i++)
            {
                if (!checkVariable(inputStrings[i + 1], varibleTypes[i]))
                {
                    valid = false;
                }
            }
        }
        else
        {
            valid = false;
        }
        return valid;
    }
    bool checkInt(float value)
    {
        return value == Mathf.RoundToInt(value);
    }
    void invalidSyntax()
    {
        Debug.LogError("Invalid Syntax");
        outputText.texts.Add("Invalid Syntax");
    }
    void invalidSyntaxMessage(string message)
    {
        Debug.LogError("Invalid Syntax: " + message);
        outputText.texts.Add("Invalid Syntax: " + message);
    }
    bool checkVariable(string inputString, varibleType varibleType)
    {
        if (varibleType == varibleType.Bool)
        {
            return inputString == "False" || inputString == "false" || inputString == "True" || inputString == "true";
        }
        else if (varibleType == varibleType.Float)
        {
            float output;
            return float.TryParse(inputString, out output);
        }
        else if (varibleType == varibleType.String)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool toBool(string inputString, bool normalValue)
    {
        if (inputString == "False" || inputString == "false")
        {
            return false;
        }
        else if (inputString == "True" || inputString == "true")
        {
            return true;
        }
        else
        {
            return normalValue;
        }
    }
}


public static class CheatData
{

    public static void SaveDefault()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/CU_CheatOff.occf";
        FileStream stream = new FileStream(path, FileMode.Create);

        string data = "If you decrypted this, what are you doing?";

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static bool CheckCheat()
    {
        string path = Application.persistentDataPath + "/CU_CheatOff.occf";
        if (File.Exists(path))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
