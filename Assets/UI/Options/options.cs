using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class options : MonoBehaviour
{
    public AudioMixer AudioMixer;
    Resolution[] resolutions;
    string[] Qualities;
    //public Dropdown ResolutionDropdown;
    public Dropdown QualityDropdown;
    public Slider[] volumeSliders;
    public GameObject postProcess;
    public Toggle fullscreen;
    public Toggle fx;

    private void Start()
    {
        float value;
        AudioMixer.GetFloat("masterVolume", out value);
        volumeSliders[0].value = value;

        AudioMixer.GetFloat("musicVolume", out value);
        volumeSliders[1].value = value;
        AudioMixer.GetFloat("SFXVolume", out value);
        volumeSliders[2].value = value;



       // int CurrentResolutionIndex = 0;
        resolutions = Screen.resolutions;
        Qualities = QualitySettings.names;

        //ResolutionDropdown.ClearOptions();
        QualityDropdown.ClearOptions();

        List<string> options = new List<string>();
        List<string> optionsQ = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string Option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(Option);
            /*
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
            */
        }
        
        for (int i = 0; i < Qualities.Length; i++)
        {
            string OptionQ = Qualities[i];
            optionsQ.Add(OptionQ);

        }
        //print(CurrentResolutionIndex);
        //ResolutionDropdown.AddOptions(options);
        //ResolutionDropdown.value = CurrentResolutionIndex;
        //ResolutionDropdown.RefreshShownValue();
        

        QualityDropdown.AddOptions(optionsQ);
        QualityDropdown.RefreshShownValue();

        fullscreen.isOn = Screen.fullScreen;

        QualityDropdown.value = QualitySettings.GetQualityLevel();


        setFX(true);
        SetMasterVolume(1);
        SetSFXVolume(1);
        SetMusicVolume(1);

        LoadFile();

       // SetResolution(CurrentResolutionIndex);
       // ResolutionDropdown.RefreshShownValue();
    }
    
    private void Update()
    {
        SetQuality(QualityDropdown.value);
        SetMasterVolume(volumeSliders[0].value);
        SetMusicVolume(volumeSliders[1].value);
        SetSFXVolume(volumeSliders[2].value);
        setFX(fx.isOn);
        
        SaveFile();
        //ResolutionDropdown.value = resolutions.ToList().IndexOf(Screen.currentResolution);
        //ResolutionDropdown.RefreshShownValue();

    }
    public void SetResolution(int ResolutionIndex)
    {
        
        //Resolution resolution = resolutions[ResolutionIndex];
            //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        
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

    public void setFX(bool fx)
    {
        if (postProcess != null)
        {
            postProcess.SetActive(fx);
        }
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
        {
            Resolution maxRes = Screen.resolutions[Screen.resolutions.Length - 1];
            print(maxRes);
            Screen.SetResolution(maxRes.width, maxRes.height, isFullscreen);
        }
    }
    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/options.shr";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        optionData data = new optionData(this);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/options.shr";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            optionData data = (optionData)bf.Deserialize(file);
            file.Close();

            volumeSliders[0].value = data.masterVolume;
            volumeSliders[1].value = data.musicVolume;
            volumeSliders[2].value = data.SFXVolume;
            fx.isOn = data.FX;
        }
        else
        {
            Debug.LogError("File not found");
            volumeSliders[0].value = 1;
            volumeSliders[1].value = 1;
            volumeSliders[2].value = 1;
            fx.isOn = true;
            Screen.fullScreen = true;
            

        }



    }
}