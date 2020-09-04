using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class options : MonoBehaviour
{
    public AudioMixer AudioMixer;
    Resolution[] resolutions;
    string[] Qualities;
    public TMP_Dropdown ResolutionDropdown;
    public TMP_Dropdown QualityDropdown;
    public Slider[] volumeSliders;


    private void Start()
    {
        float value;
        AudioMixer.GetFloat("masterVolume", out value);
        volumeSliders[0].value = value;
        
        AudioMixer.GetFloat("musicVolume", out value);
        volumeSliders[1].value = value;
        AudioMixer.GetFloat("SFXVolume", out value);
        volumeSliders[2].value = value;


        

        int CurrentResolutionIndex = 0;
        resolutions = Screen.resolutions;
        Qualities = QualitySettings.names;

        ResolutionDropdown.ClearOptions();
        QualityDropdown.ClearOptions();

        List<string> options = new List<string>();
        List<string> optionsQ = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string Option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(Option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        for (int i = 0; i < Qualities.Length; i++)
        {
            string OptionQ = Qualities[i];
            optionsQ.Add(OptionQ);

        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = CurrentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        QualityDropdown.AddOptions(optionsQ);
        QualityDropdown.RefreshShownValue();

        
    }

    private void Update()
    {
        QualityDropdown.value = QualitySettings.GetQualityLevel();
        SetMasterVolume(volumeSliders[0].value);
        SetMusicVolume(volumeSliders[1].value);
        SetSFXVolume(volumeSliders[2].value);

    }
    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("masterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        AudioMixer.SetFloat("SFXVolume", volume);
    }
    


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}