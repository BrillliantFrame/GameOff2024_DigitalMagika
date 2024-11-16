using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MS_SettingsManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Graphics Settings")]
    public TMP_Dropdown ResolutionDropdown;
    public Toggle FullscreenToggle;
    public TMP_Dropdown QualityDropdown;


    public Slider uiScaleSlider;
    public Canvas mainCanvas;

    private Resolution[] resolutions;

    private void Start()
    {
        // Load saved settings here
        LoadSettings();
        SetupResolutionDropdown();
    }

    private void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            ResolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
        SafeSettings();
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        SafeSettings();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        SafeSettings();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        SafeSettings();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        SafeSettings();
    }

    //Optional
    public void SetMusicVolume(float volume)
    {
        // musicSource.volume = volume;
        //PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        // sfxSource.volume = volume;
        //PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetUIScale(float scale)
    {
        if (mainCanvas != null)
        {
            // Change the scale of the main canvas
            mainCanvas.transform.localScale = new Vector3(scale, scale, 1);

            // Save the scale setting
            PlayerPrefs.SetFloat("UIScale", scale);
            PlayerPrefs.Save();
        }
    }

    // Load Settings on Start
    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            AudioListener.volume = masterVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            // musicSource.volume = musicVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            // sfxSource.volume = sfxVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            ResolutionDropdown.value = resolutionIndex;
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            FullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;
            Screen.fullScreen = FullscreenToggle.isOn;
        }

        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            QualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
            QualitySettings.SetQualityLevel(QualityDropdown.value);
        }
    }

    public void SafeSettings()
    {
        PlayerPrefs.Save();
    }
}
