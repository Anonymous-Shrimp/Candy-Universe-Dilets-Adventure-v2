using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeHeadsUp : MonoBehaviour
{
    Animator anim;
    public bool talking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool entered = false;
        foreach(TalkArea t in FindObjectsOfType<TalkArea>())
        {
            if (t.talkArea)
            {
                entered = true;
            }
        }
        anim.SetBool("Entered", entered && !talking);
    }
}
