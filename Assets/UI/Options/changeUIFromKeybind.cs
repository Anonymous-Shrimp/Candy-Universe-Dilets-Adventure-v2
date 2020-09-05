using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class changeUIFromKeybind : MonoBehaviour
{
    Text text;
    public string normalText1;
    public string Keybind;
    public string normalText2;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        text.text = normalText1 + FindObjectOfType<Keybind>().keys[Keybind].ToString() + normalText2;
    }
}
