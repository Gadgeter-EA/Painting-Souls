using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown, qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullScreen;
    
    // Here we are gonna store the resolutions that Unity detects
    public Resolution[] resolutions; 
    
    private void Start()
    {
        CheckForSettings();
    }

    void CheckForSettings()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
            audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }

        if (!PlayerPrefs.HasKey("Resolution"))
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "X" + resolutions[i].height + "@" + resolutions[i].refreshRate;

                options.Add(option);
                
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            
            PlayerPrefs.SetInt("Resolution", currentResolutionIndex);
            Screen.SetResolution(resolutions[currentResolutionIndex].width, resolutions[currentResolutionIndex].height, Screen.fullScreen);
        }
        else
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "X" + resolutions[i].height + "@" + resolutions[i].refreshRate;

                options.Add(option);
                
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
            resolutionDropdown.RefreshShownValue();
            Screen.SetResolution(resolutions[PlayerPrefs.GetInt("Resolution")].width, resolutions[PlayerPrefs.GetInt("Resolution")].height, Screen.fullScreen);
        }

        if (!PlayerPrefs.HasKey("Quality"))
        {
            PlayerPrefs.SetInt("Quality", 2);
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
            qualityDropdown.RefreshShownValue();
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }
        else
        {
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
            qualityDropdown.RefreshShownValue();
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        }

        if (!PlayerPrefs.HasKey("Fullscreen"))
        {
            PlayerPrefs.SetString("Fullscreen", "true");
            Screen.fullScreen = true;
            fullScreen.isOn = true;
        }
        else
        {
            if(PlayerPrefs.GetString("Fullscreen") == "true")
            {
                Screen.fullScreen = true;
                fullScreen.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                fullScreen.isOn = false;
            }
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetString("Fullscreen", "true");
        }
        else
        {
            PlayerPrefs.SetString("Fullscreen", "false");
        }
    }
}
