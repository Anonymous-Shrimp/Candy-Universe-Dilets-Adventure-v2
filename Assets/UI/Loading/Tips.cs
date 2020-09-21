using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Text))]
public class Tips : MonoBehaviour
{
    public string[] tips;
    Text textUI;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        textUI = GetComponent<Text>();
        if (AcrossSceneTransfer.instance != null)
        {
            index = Mathf.RoundToInt(AcrossSceneTransfer.instance.data[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (AcrossSceneTransfer.instance != null)
            {
                AcrossSceneTransfer.instance.data[1] = index;
            }
        textUI.text = "Tip: " + tips[index];
    }
    int randomTip()
    {
        
        int r = Random.Range(0, tips.Length - 1);
        if (r == index)
        {
            return randomTip();
        }
        else
        {
            return r;
        }

    }
    public void getRandomTip()
    {
        index = randomTip();
    }
}
