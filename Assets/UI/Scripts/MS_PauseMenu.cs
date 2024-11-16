using UnityEngine;
using UnityEngine.SceneManagement;

public class MS_PauseMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionsMenu();
        }
    }

    public void ToggleOptionsMenu()
    {
        if (optionsMenu != null)
        {
            bool isActive = !optionsMenu.activeSelf;
            optionsMenu.SetActive(isActive);

            if (isActive)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        AppCore.Instance?.BackToMain();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
}
