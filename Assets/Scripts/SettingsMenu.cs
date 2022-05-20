using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown; 
    
    // Here we are gonna store the resolutions that Unity detects
    Resolution[] resolutions; 
    
    void Start()
    {
        resolutions = Screen.resolutions; //Saving resolutions
        
        resolutionDropdown.ClearOptions(); // Clearing the dropdown for new resolutions

        List<string> options = new List<string>(); // Creating a LIST of strings for the resolutions
        // This is gonna be shown to the player
        
        // Variable to store the index of current resolution and put it by default
        int currentResolutionIndex = 0; 

        for (int i = 0; i < resolutions.Length; i++) //Looping through the resolutions
        {
            // Saving the resolutions in an understandable format
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate + "Hz"; 
            options.Add(option); // Adding the option to the List

            // If width and height of current loop equals the current resolution
            // we save the index
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            { 
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options); //Updating the dropdown with the new resolutions
        resolutionDropdown.value = currentResolutionIndex; // Modifying Dropdown to show current resolution
        resolutionDropdown.RefreshShownValue();  // Updating Dropwdown
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex]; // Getting the correct resolution from de array
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
