using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyCounter : MonoBehaviour
{
    [Range(0, 9999)]
    public int candyAmount;

    private float candyAmountApprox;

    bool showValue;

    Text candyText;
    Animator anim;
    [Range(0, 9999)]
    public int targetAmount;

    private void Start()
    {
        anim = GetComponent<Animator>();
        candyText = GetComponentInChildren<Text>();
        candyAmountApprox = targetAmount;
    }
    
    private void Update()
    {
        anim.SetBool("FadedIn", showValue || Input.GetKey(KeyCode.E));
        candyText.text = candyAmount.ToString();
        candyAmount = Mathf.RoundToInt(candyAmountApprox);
        if(Mathf.Abs(targetAmount - candyAmountApprox) < 1 && targetAmount != candyAmountApprox)
        {
           candyAmountApprox = targetAmount;
            showValue = true;
        }

        else if (targetAmount > candyAmountApprox)
        {
            candyAmountApprox += Time.unscaledDeltaTime * Mathf.Abs(targetAmount - candyAmount);
            showValue = true;
        }
        else if(targetAmount < candyAmountApprox)
        {
            candyAmountApprox -= Time.unscaledDeltaTime * Mathf.Abs(targetAmount - candyAmount);
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
