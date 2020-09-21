using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHUD : MonoBehaviour
{
    public bool show;
    public bool canToggle = true;
    public Canvas[] canvases;

    // Start is called before the first frame update
    void Start()
    {
        show = true;
        canToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) &&canToggle)
        {
            show = !show;
        }

        foreach (Canvas c in canvases)
        {
            c.enabled = show;
        }
        
    }
}
