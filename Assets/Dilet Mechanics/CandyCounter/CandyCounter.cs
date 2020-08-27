using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyCounter : MonoBehaviour
{
    [Range(0, 999)]
    public int candyAmount;

    private int previousCandyAmount;
    bool showValue;

    Text candyText;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        candyText = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        anim.SetBool("FadedIn", showValue || Input.GetKey(KeyCode.E));
        candyText.text = candyAmount.ToString();

        if(previousCandyAmount != candyAmount)
        {
            anim.SetTrigger("Flash");
        }

        previousCandyAmount = candyAmount;
    }
    public void flashAmount()
    {
        anim.SetTrigger("Flash");
    }
}
