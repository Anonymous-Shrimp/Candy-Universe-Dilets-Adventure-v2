using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGoblin : MonoBehaviour
{
    public AI goblin;
    private void Start()
    {
        goblin = FindObjectOfType<AI>();

    }
    private void Update()
    {
        if(goblin.Health <= 0 || Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Skip"]))
        {
            FindObjectOfType<loading>().LoadLevel(3);
        }
    }
}
