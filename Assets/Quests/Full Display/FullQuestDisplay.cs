using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullQuestDisplay : MonoBehaviour
{
    PauseMenu pause;
    public GameObject hudMenu;
    // Start is called before the first frame update
    void Start()
    {
        pause = FindObjectOfType<PauseMenu>();
        hudMenu.SetActive(pause.hudMenu);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            pause.hudMenu = !pause.hudMenu;
            hudMenu.SetActive(pause.hudMenu);
            
        }
    }
}
