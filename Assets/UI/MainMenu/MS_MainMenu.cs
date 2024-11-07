using UnityEngine;
using UnityEngine.SceneManagement;

public class MS_MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AppCore.Instance?.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
