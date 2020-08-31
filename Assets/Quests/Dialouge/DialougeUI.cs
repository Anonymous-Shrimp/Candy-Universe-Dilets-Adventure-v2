using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeUI : MonoBehaviour
{
    public Text speakerText;
    public Text speechText;

    public GameObject UI;
    public bool active = false;

    public string speaker;
    public string speech;
    private void Start()
    {
        UI.SetActive(false);
    }
    void Update()
    {
        UI.SetActive(active);
        speakerText.text = speaker;
        speechText.text = speech;
    }
}
