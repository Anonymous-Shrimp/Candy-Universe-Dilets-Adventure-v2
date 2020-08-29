using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questDisplay : MonoBehaviour
{
    public Animator anim;
    public Text title;
    public Text descrip;
    public Text progress;

    public List<string> titleText;
    public List<string> descripText;
    public List<string> progressText;

    public bool canChange;
    public int showings;

    private float timer = 0f;
    public float disappearTime = 5f;

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
        if(!(showings > 0))
        {
            timer = disappearTime;
        }
        anim.SetInteger("Showings", showings);
        AnimatorClipInfo[] a = anim.GetCurrentAnimatorClipInfo(0);
        if ((Input.GetKeyDown(KeyCode.Tab) || timer <= 0) && a[0].clip.name == "Showed")
        {
            timer = disappearTime;
            anim.SetTrigger("Hide");
            showings -= 1;

            if(showings < 0)
            {
                showings = 0;
            }
        }
        if (canChange)
        {
            if (showings < titleText.ToArray().Length)
            {
                titleText.RemoveAt(0);
                descripText.RemoveAt(0);
                progressText.RemoveAt(0);
            }
            if (titleText.ToArray().Length > 0)
            {
                title.text = titleText[0];
                descrip.text = descripText[0];
                progress.text = progressText[0];
            }
            
        }

    }
    public void addShowing(string title, string descrip, string progress)
    {
        titleText.Add(title);
        descripText.Add(descrip);
        progressText.Add(progress);
        showings++;
    }
}
