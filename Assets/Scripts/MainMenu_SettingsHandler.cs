using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class MainMenu_SettingsHandler : MonoBehaviour
{
    public static float volume;

    public Slider masterVolume;
    public TMP_Dropdown resolution;
    public Image fullscreenCheck;
    public Sprite fullscreenOn;
    public Sprite fullscreenOff;

    public Toggle fullscreen;
    public Slider sensitivity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MasterVolumeChange()
    {
        AudioListener.volume = masterVolume.value;
    }

    public void ResolutionChange()
    {
        switch (resolution.value) // TO DO: Dodaæ wiêcej rozdzielczoœci
        {
            case 0:
                Screen.SetResolution(1920, 1080, true); 
                break;
            case 1:
                Screen.SetResolution(1280, 720, true); 
                break;
            case 2:
                Screen.SetResolution(720, 480, true); 
                break;

        }
        Debug.Log(Screen.currentResolution);
    }

    public void FullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
        {
            fullscreenCheck.sprite = fullscreenOn;
        }
        else fullscreenCheck.sprite = fullscreenOff;
        Debug.Log(Screen.fullScreen);
    }
}
