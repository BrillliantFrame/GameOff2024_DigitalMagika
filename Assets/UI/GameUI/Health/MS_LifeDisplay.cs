using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public GameObject heartPrefab;          // Reference to the heart icon prefab
    public Transform heartsContainer;       // The container for the heart icons
    public int initialLives = 3;            // Default lives count
    private static LifeDisplay instance;    // Singleton instance for persistence

    private static int currentLives;

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
        // Subscribe to CharacterController2D events
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnPlayerDamaged += RemoveLife;
            CharacterController2D.Instance.OnPlayerLivesEnded += GameOver;
            CharacterController2D.Instance.OnPlayerHealed += AddLife;
        }

        UpdateHeartIcons();
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnPlayerDamaged -= RemoveLife;
            CharacterController2D.Instance.OnPlayerLivesEnded -= GameOver;
            CharacterController2D.Instance.OnPlayerHealed -= AddLife;
        }
    }

    public void SetLives(int lives)
    {
        currentLives = lives;
        UpdateHeartIcons();
    }

    private void UpdateHeartIcons()
    {
        // Clear existing heart icons
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        // Spawn new heart icons based on currentLives
        for (int i = 0; i < currentLives; i++)
        {
            Instantiate(heartPrefab, heartsContainer);
        }
    }

    public void RemoveLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateHeartIcons();

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddLife()
    {
        currentLives++;
        UpdateHeartIcons();
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Player has no lives left.");
    }
}
