using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiletBar : MonoBehaviour {
    private Image bar;
	// Use this for initialization
	void Start () {
        bar = transform.Find("Bar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	public void SetSize(float sizeNormalized) {
        bar.fillAmount = sizeNormalized;
	}
    public void SetColor(Color color)
    {
        bar.color = color;
    }
}
