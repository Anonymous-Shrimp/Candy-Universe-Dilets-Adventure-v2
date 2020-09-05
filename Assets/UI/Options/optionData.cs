using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class optionData
{
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;
    public bool FX;

    public optionData(options o)
    {
        masterVolume = o.volumeSliders[0].value;
        musicVolume = o.volumeSliders[1].value;
        SFXVolume = o.volumeSliders[2].value;
        FX = o.postProcess.activeInHierarchy;
    }
}
