using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour {
    [SerializeField] private Light sun;
    [SerializeField] private GameObject sunObject;
    [SerializeField] private float secondsInFullDay = 60f;

    [Range(0, 1)] public float currentTimeOfDay = 0;
    [Range(0, 1)] private float actualTime = 0;
    private float timeMultiplier = 1f;
    private float sunInitialIntestity;
    public int dayNum = 1;

    void Start(){
        sunInitialIntestity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            dayNum = dayNum + 1;
        }
        if(currentTimeOfDay < 0.5)
        {
            actualTime = currentTimeOfDay + 0.5f;
        }
        else
        {
            actualTime = currentTimeOfDay - 0.5f;
        }
    }

    void UpdateSun()
    {
        sunObject.transform.localRotation = Quaternion.Euler((actualTime * 360f), 170, 0);
        float intestityMultiplier = 1;
        if (actualTime <= 0.2f || actualTime >= 0.75f)
        {
            intestityMultiplier = 0;
           
        }
        else if (actualTime <= 0.25f)
        {
            intestityMultiplier = Mathf.Clamp01((actualTime - 0.23f) * (1 / 0.02f));
            
        }
        else if (actualTime >= 0.73f) {
            intestityMultiplier = Mathf.Clamp01(1 - ((actualTime -0.75f) * (1/0.02f)));
            
        }


        sun.intensity = sunInitialIntestity * intestityMultiplier;
    }
}
