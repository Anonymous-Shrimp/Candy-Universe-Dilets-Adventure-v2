using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    public Animator anim;
    public Text text;

    public List<string> message;

    public bool canChange;
    public int showings;

    private float timer = 0f;
    public float disappearTime = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        timer = disappearTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        if (!(showings > 0))
        {
            timer = disappearTime;
        }
        anim.SetInteger("Showings", showings);
        AnimatorClipInfo[] a = anim.GetCurrentAnimatorClipInfo(0);
        if (timer <= 0 && a[0].clip.name == "FadedIn")
        {
            timer = disappearTime;
            anim.SetTrigger("Hide");
            showings -= 1;

            if (showings < 0)
            {
                showings = 0;
            }
        }
        if (canChange)
        {
            if (showings < message.ToArray().Length)
            {
                message.RemoveAt(0);
            }
            if (message.ToArray().Length > 0)
            {
                text.text = message[0];
            }

        }

    }
    public void addShowing(string text)
    {
        message.Add(text);
        showings++;
    }
}
