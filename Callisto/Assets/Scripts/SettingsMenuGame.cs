using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuGame : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Resolution[] resolutions;

    [SerializeField] private Slider master;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private Toggle isFullscreen;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionDropDown.AddOptions(options);

        if (!PlayerPrefs.HasKey("resolutionIndex"))
        {
            resolutionDropDown.value = currentResolutionIndex;
            resolutionDropDown.RefreshShownValue();
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            float value = PlayerPrefs.GetFloat("volume");
            master.SetValueWithoutNotify(value);
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            float value = PlayerPrefs.GetFloat("Music");
            music.SetValueWithoutNotify(value);
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            float value = PlayerPrefs.GetFloat("SFX");
            sfx.SetValueWithoutNotify(value);
        }

        if (PlayerPrefs.HasKey("fullScreenMode"))
        {
            bool fullScreenMode = PlayerPrefs.GetInt("fullScreenMode") == 1;
            Screen.fullScreen = fullScreenMode;
            isFullscreen.SetIsOnWithoutNotify(fullScreenMode);
        }

        if (PlayerPrefs.HasKey("resolutionIndex"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("resolutionIndex");
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            resolutionDropDown.value = resolutionIndex;
            resolutionDropDown.RefreshShownValue();

        }
    }

    public void SetVolumeMaster()
    {
        float volume = master.value;
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetVolumeMusic()
    {
        float volume = music.value;
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetVolumeSFX()
    {
        float volume = sfx.value;
        audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        int fullScreenInt = isFullScreen ? 1 : 0;
        PlayerPrefs.SetInt("FullScreenMode", fullScreenInt);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }
}
