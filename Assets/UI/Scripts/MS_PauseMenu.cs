using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MS_PauseMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    public void ToggleOptionsMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Escape key pressed");
        if (context.performed)
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

                CharacterController2D.Instance?.DisableInput();

                AkSoundEngine.SetRTPCValue("LowPassPauseMenu_RTPC", 0f);
                AkSoundEngine.SetRTPCValue("PauseMenu_RTPC", 0f);
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                CharacterController2D.Instance?.EnableInput();

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
