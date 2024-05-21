using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Toggle bloomToggle;

    [SerializeField] private Volume postProcess;
    private Bloom bloomEffect;


    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    List<string> options = new List<string>();


    private float currentRefreshRate;
    private int currentResolutionindex = 0;
    private bool currentFullScreen = true;

    private void Start()
    {
        FillScreenResolutions();
        postProcess.profile.TryGet<Bloom>(out bloomEffect);
        
        if (Screen.fullScreen == true)
        {
            currentFullScreen = true;
        }
        else
        {
            currentFullScreen = false;
        }
        fullScreenToggle.isOn = currentFullScreen;

        if(PlayerPrefs.GetInt("Vsync") == 1)
        {
            vsyncToggle.isOn = true;
            QualitySettings.vSyncCount = 1;
            
        }
        else if(PlayerPrefs.GetInt("Vsync") == 0)
        {
            vsyncToggle.isOn = false;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = (int)filteredResolutions[currentResolutionindex].refreshRateRatio.value;
        }

        if (PlayerPrefs.GetInt("Bloom") == 1)
        {
            bloomToggle.isOn = true;
            bloomEffect.active = true;

        }
        else if (PlayerPrefs.GetInt("Bloom") == 0)
        {
            bloomToggle.isOn = false;
            bloomEffect.active = false;
        }
    }

    private void FillScreenResolutions()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.value.ToString() + " Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionindex = i;
            }
            
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionindex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetBloom(bool bloom)
    {
        if(bloom)
        {
            bloomEffect.active = true;
            PlayerPrefs.SetInt("Bloom", 1);
        }
        else
        {
            bloomEffect.active = false;
            PlayerPrefs.SetInt("Bloom", 0);
        }  
    }

    public void SetVSync(bool vsync)
    {
        if(vsync)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("Vsync", 1);

        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("Vsync", 0);
            Application.targetFrameRate = (int)filteredResolutions[currentResolutionindex].refreshRateRatio.value;
        }
    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        currentFullScreen = fullscreen;
    }

    public void SetResolution(int index)
    {
        Resolution resolution = filteredResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, currentFullScreen);
    }
}
