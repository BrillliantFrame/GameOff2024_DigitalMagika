using UnityEngine;
using UnityEngine.SceneManagement;

public class MS_MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Load the first level in de scene manger buildindex
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
