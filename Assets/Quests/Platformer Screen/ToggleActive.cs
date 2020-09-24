using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using platformQuest;

public class ToggleActive : MonoBehaviour
{
    public bool turnedOn;
    public Animator anim;
    public Renderer screen;
    public Player playerPlatformer;
    [HideInInspector]
    public bool isActive = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isActive = false;
        playerPlatformer = FindObjectOfType<platformQuest.Player>();
    }
    private void Update()
    {
        anim.SetBool("Active", turnedOn);
        playerPlatformer.canControl = isActive;
        if (isActive)
        {
            
            screen.material.EnableKeyword("_EMISSION");
        }
        else
        {
            screen.material.DisableKeyword("_EMISSION");
        }
    }
    public void changeActive(bool i)
    {
        turnedOn = i;
    }
}
