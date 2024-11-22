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
                AkSoundEngine.SetRTPCValue("LowPassPauseMenu_RTPC", 0f);
                AkSoundEngine.SetRTPCValue("PauseMenu_RTPC", 0f);
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                AkSoundEngine.SetRTPCValue("LowPassPauseMenu_RTPC", 100f);
                AkSoundEngine.SetRTPCValue("PauseMenu_RTPC", 100f);
            }
        }
    }

    public void ReturnToMainMenu()
    {

        Time.timeScale = 1f;
        AppCore.Instance?.BackToMain();
        AkSoundEngine.SetRTPCValue("LowPassPauseMenu_RTPC", 100f);
        AkSoundEngine.SetRTPCValue("PauseMenu_RTPC", 100f);
        AkSoundEngine.PostEvent("Stop_All", gameObject);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
}
