using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HarshEnvironment : MonoBehaviour
{
    public TimeCycle timeCycle;
    public UIMessage UI;
    public string message;
    public int chance;
    public Vector2 durationRange;
    float timer;
    public bool inArea;
    public PostProcessVolume fx;
    float fxWeight;
    float chanceTimer;
    // Start is called before the first frame update
    void Start()
    {
        timeCycle = FindObjectOfType<TimeCycle>();
        UI = FindObjectOfType<UIMessage>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(fx.weight > fxWeight)
        {
            fx.weight -= Time.deltaTime;
        }else if (fx.weight < fxWeight)
        {
            fx.weight += Time.deltaTime;
        }
        if (inArea && timer > 0)
        {
            if (timeCycle.currentTimeOfDay > 0.3f && timeCycle.currentTimeOfDay < 0.7f)
            {
                timer -= Time.deltaTime;
                FindObjectOfType<Ouch>().harshEnvironment = true;
                fxWeight = 1;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            chanceTimer -= Time.deltaTime;
            if (chanceTimer <= 0)
            {
                float r = Random.Range(0, chance);
                if (timeCycle.currentTimeOfDay > 0.3f && timeCycle.currentTimeOfDay < 0.7f && inArea && r == 0)
                {
                    timer = Random.Range(durationRange.x, durationRange.y);
                    UI.addShowing(message);
                }
                chanceTimer = 1;
            }
            FindObjectOfType<Ouch>().harshEnvironment = false;
            fxWeight = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dilet"))
        {
            inArea = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dilet"))
        {
            inArea = false;
        }
    }
}
