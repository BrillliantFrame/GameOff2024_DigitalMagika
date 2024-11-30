using UnityEngine;
using UnityEngine.UI;

public class MS_MainMenu : MonoBehaviour
{
    [SerializeField]
    private FadingUI _mainMenuUI;
    [SerializeField]
    private FadingUI _settingsUI;
    [SerializeField]
    private FadingUI _cheatsUI;
    [SerializeField]
    private Button _cheatsButton;

    public void Start()
    {
        _cheatsButton.gameObject.SetActive(Resources.Load<AvailableCheats>("Available Cheats").CanUseCheats());
        AkSoundEngine.PostEvent("MainMenu_Music", gameObject);
        Debug.Log("LAUNCHED");
    }

    public void PlayGame()
    {
        AppCore.Instance?.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchSettingsMenu()
    {
        _mainMenuUI.SwitchVisibility();
        _settingsUI.SwitchVisibility();
    }

    public void SwitchCheatsMenu()
    {
        _mainMenuUI.SwitchVisibility();
        _cheatsUI.SwitchVisibility();
    }
}
