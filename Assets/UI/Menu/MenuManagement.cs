using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagement : MonoBehaviour
{
    public GameObject overrideObject;
    public RectTransform[] mainButtons;
    public float xPos = 224.33f;
    public float[] buttonPos;
    // Start is called before the first frame update
    void Start()
    {
        /*
        foreach(RectTransform b in mainButtons)
        {
            buttonPos.Add(b.position.y);
            
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveSystem.checkFile())
        {
            mainButtons[0].gameObject.SetActive(true);
            for (int i = 0; i < mainButtons.Length; i++)
            {
                mainButtons[i].anchoredPosition = new Vector3 (xPos, buttonPos[i]);
            }

        }
        else
        {
            mainButtons[0].gameObject.SetActive(false);
            mainButtons[1].anchoredPosition = new Vector3(xPos, buttonPos[0]);
            mainButtons[2].anchoredPosition = new Vector3(xPos, buttonPos[1]);
            mainButtons[3].anchoredPosition = new Vector3(xPos, buttonPos[2]);
            mainButtons[4].anchoredPosition = new Vector3(xPos, buttonPos[3]);
        }
    }
    public void newGame()
    {
        if (SaveSystem.checkFile())
        {
            overrideObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<loading>().LoadLevel(2);
        }
    }
}
