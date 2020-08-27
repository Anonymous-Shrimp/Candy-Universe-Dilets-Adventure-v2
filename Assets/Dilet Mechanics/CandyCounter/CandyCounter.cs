using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyCounter : MonoBehaviour
{
    [Range(0, 999)]
    public int candyAmount;
    
    bool showValue;

    Text candyText;
    Animator anim;

    public int targetAmount;

    private void Start()
    {
        anim = GetComponent<Animator>();
        candyText = GetComponentInChildren<Text>();
        candyAmount = targetAmount;
    }
    
    private void Update()
    {
        anim.SetBool("FadedIn", showValue || Input.GetKey(KeyCode.E));
        candyText.text = candyAmount.ToString();
        if(Mathf.Abs(targetAmount - candyAmount) == 1)
        {
            candyAmount += 1;
            showValue = true;
        }
        else if (targetAmount > candyAmount)
        {
            candyAmount += 2;
            showValue = true;
        }
        else if(targetAmount < candyAmount)
        {
            candyAmount -= 2;
            showValue = true;
        }
        else
        {
            showValue = false;
        }
        
    }
    public void flashAmount()
    {
        anim.SetTrigger("Flash");
    }
    public void changeAmount(int ammt)
    {
        targetAmount += ammt;
    }
}
