using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyHUD : MonoBehaviour
{
    public Text candyText;
    private void Update()
    {
        candyText.text = FindObjectOfType<CandyCounter>().candyAmount.ToString();
    }
}
