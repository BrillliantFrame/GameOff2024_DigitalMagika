using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public Sprite brokenHeartSprite;        // Reference to the broken heart sprite
    public Sprite fullHeartSprite;          // Reference to the full heart sprite
    public Transform heartsContainer;       // The container for the heart icons
    public int initialLives = 3;            // Default lives count
    public Vector2 heartSize = new Vector2(50, 50); // Size of each heart icon

    private static LifeDisplay instance;    // Singleton instance for persistence
    private static int currentLives;
    private Image[] heartIcons;             // Array to store heart images

    public GameObject gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object in all scenes
            currentLives = initialLives;
        }
        else
        {
            Destroy(gameObject);            // Destroy duplicate instances
        }
    }

    private void Start()
    {
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnPlayerDamaged += ReplaceHeartWithBroken;
            CharacterController2D.Instance.OnPlayerLivesEnded += GameOver;
            CharacterController2D.Instance.OnPlayerHealed += AddLife;
        }

        InitializeHeartIcons();
    }

    private void OnDestroy()
    {
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnPlayerDamaged -= ReplaceHeartWithBroken;
            CharacterController2D.Instance.OnPlayerLivesEnded -= GameOver;
            CharacterController2D.Instance.OnPlayerHealed -= AddLife;
        }
    }

    public void SetLives(int lives)
    {
        currentLives = lives;
        UpdateHeartIcons();
    }

    private void InitializeHeartIcons()
    {
        // Clear existing heart icons
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        // Create Image components as heart icons based on initialLives
        heartIcons = new Image[initialLives];
        for (int i = 0; i < initialLives; i++)
        {
            GameObject heartObject = new GameObject("Heart" + i, typeof(Image));
            heartObject.transform.SetParent(heartsContainer);

            Image heartImage = heartObject.GetComponent<Image>();
            heartImage.sprite = fullHeartSprite; // Set initial sprite to full heart
            heartIcons[i] = heartImage;

            // Set the size of each heart icon
            RectTransform rectTransform = heartImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = heartSize;
        }
    }

    private void UpdateHeartIcons()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < currentLives)
            {
                heartIcons[i].sprite = fullHeartSprite; // Set to full heart
            }
            else
            {
                heartIcons[i].sprite = brokenHeartSprite; // Set to broken heart
            }
        }
    }

    public void ReplaceHeartWithBroken()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateHeartIcons();

            if (currentLives == 0)
            {
                Debug.Log("Lives: " + currentLives);
                GameOver();
            }
        }
    }

    public void AddLife()
    {
        if (currentLives < initialLives)
        {
            currentLives++;
            UpdateHeartIcons();
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ReturnToMainMenu()
    {
        AkSoundEngine.PostEvent("Stop_All", gameObject);
        Time.timeScale = 1f;
        AppCore.Instance?.BackToMain();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
