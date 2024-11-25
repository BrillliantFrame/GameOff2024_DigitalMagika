using UnityEngine;
using UnityEngine.SceneManagement;

public class MS_MainMenu : MonoBehaviour
{
    public void Start()
    {
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
}
