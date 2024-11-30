using UnityEngine;

public class MS_EndingSceneManager : MonoBehaviour
{
    public RectTransform creditsPanel; // Assign CreditsPanel in Inspector
    public float scrollSpeed = 30f;
    public float startingOffset = -2625;
    public float stopPositionY = 1035; // Where the scrolling should stop

    void Start()
    {
        // Get the height of the play window
        float playWindowHeight = Camera.main.pixelHeight;

        // Set the starting position of the creditsPanel to just below the play window
        creditsPanel.anchoredPosition = new Vector2(creditsPanel.anchoredPosition.x, startingOffset + playWindowHeight);
    }

    void Update()
    {
        // Scroll the creditsPanel upward until it reaches the stop position
        if (creditsPanel.anchoredPosition.y < stopPositionY)
        {
            creditsPanel.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
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
