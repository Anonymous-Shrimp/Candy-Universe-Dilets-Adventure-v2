using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGoblin : MonoBehaviour
{
    public AI goblin;
    bool done = false;
    private void Start()
    {
        goblin = FindObjectOfType<AI>();

    }
    private void Update()
    {
        if((goblin.Health <= 0 || Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Skip"]))&& !done)
        {
            FindObjectOfType<loading>().LoadLevel(3);
            done = true;
        }
    }
}
