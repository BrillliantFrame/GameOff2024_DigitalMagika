using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MS_SettingsManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider masterVolumeSlider;
    public AudioSource[] audioSources;

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
        audioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        LoadSettings();
        SetupResolutionDropdown();
    }

    public void SetMasterVolume()
    {
        // Get the value from the masterVolumeSlider directly
        float volume = masterVolumeSlider.value;

        AkSoundEngine.SetRTPCValue("MasterVolume", volume);


        // Optionally, you can log to confirm that the volume is being set
        Debug.Log("Master volume set to: " + volume);

        // Save the volume to PlayerPrefs for persistence
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    private void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        Debug.Log($"Found {resolutions.Length} screen resolutions.");

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);

        // Default to the current resolution
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        Debug.Log("Resolution dropdown successfully initialized.");
    }

    public void SetResolution()
    {
        if (resolutions == null || resolutions.Length == 0)
        {
            Debug.LogError("Resolutions array is not initialized or empty!");
            return;
        }

        int resolutionIndex = ResolutionDropdown.value;

        if (resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
        {
            Debug.LogError("Resolution index is out of bounds!");
            return;
        }

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        SafeSettings();

        Debug.Log($"Resolution set to: {resolution.width} x {resolution.height}");
    }

    public void SetFullscreen()
    {
        bool isFullscreen = FullscreenToggle.isOn;
        if (isFullscreen)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else
            Screen.fullScreenMode = FullScreenMode.Windowed;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        SafeSettings();
    }

    public void SetQuality()
    {
        int qualityIndex = QualityDropdown.value;
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        SafeSettings();

        int quality = QualitySettings.GetQualityLevel();
        Debug.Log($"Current quality is set to: {quality}");

        Debug.Log($"Quality set to: {qualityIndex} = {QualityDropdown.value}");
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
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        if (PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight"))
        {
            int resolutionWidth = PlayerPrefs.GetInt("ResolutionWidth");
            int resolutionHeight = PlayerPrefs.GetInt("ResolutionHeight");
            Screen.SetResolution(resolutionWidth, resolutionHeight, Screen.fullScreenMode);
            Debug.Log($"Init: Resolution set to: {resolutionWidth} x {resolutionHeight}");
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            var isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            FullscreenToggle.isOn = isFullscreen;
            if (isFullscreen)
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            else
                Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            QualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
            QualitySettings.SetQualityLevel(QualityDropdown.value);
        }

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            ResolutionDropdown.gameObject.SetActive(false);
        }
        QualityDropdown.gameObject.SetActive(false);
    }

    public void SafeSettings()
    {
        PlayerPrefs.Save();
    }
}
