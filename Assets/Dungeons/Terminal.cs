using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {
    public TerminalControl terminal;
    public Material material;
    private bool activated;
    
    void Start()
    {
        //material.EnableKeyword("_Emmision");
        gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0f, 0.0f);
    }
    void ApplyDamage(int theDamage)
    {
        if (!activated)
        {
            StartCoroutine(Fade(1, 0));
            terminal.terminals -= 1;
            activated = true;
            //material.DisableKeyword("_Emmision");
        }
    }
    IEnumerator Fade(float Sec, float a)
    {

        for (float i = 1; i >= 0; i -= Time.deltaTime / Sec)
        {
            a = (i * -1) + 1;
            gameObject.GetComponent<Renderer>().material.color = new Color(i, a, a);
            yield return null;
        }
    }
}
