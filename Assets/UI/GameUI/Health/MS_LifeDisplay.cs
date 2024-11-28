using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite _brokenHeartSprite;        // Reference to the broken heart sprite
    [SerializeField]
    private Sprite _fullHeartSprite;          // Reference to the full heart sprite
    [SerializeField]
    private float _heartSize = 50f; // Size of each heart icon
    [SerializeField]
    private Image[] heartIcons;             // Array to store heart images
    [SerializeField]
    private FadingUI _gameOverUI;

    private void Start()
    {
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnPlayerDamaged += OnPlayerHealthChanged;
            CharacterController2D.Instance.OnPlayerHealed += OnPlayerHealthChanged;
            CharacterController2D.Instance.OnPlayerLivesEnded += GameOver;
        }

        UpdateHeartIcons(true);
    }

    private void OnDestroy()
    {
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnPlayerDamaged -= OnPlayerHealthChanged;
            CharacterController2D.Instance.OnPlayerHealed -= OnPlayerHealthChanged;
            CharacterController2D.Instance.OnPlayerLivesEnded -= GameOver;
        }
    }

    private void UpdateHeartIcons(bool init)
    {
        var currentLives = CharacterController2D.Instance?.CurrentLives ?? 0;

        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (init)
            {
                var rect = heartIcons[i].GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(_heartSize, _heartSize);
            }

            if (currentLives > i)
            {
                heartIcons[i].sprite = _fullHeartSprite;
            }
            else
            {
                heartIcons[i].sprite = _brokenHeartSprite;
            }
        }
    }

    private void OnPlayerHealthChanged()
    {
        UpdateHeartIcons(false);
    }

    public void GameOver()
    {
        StartCoroutine(gameOver());
    }

    private IEnumerator gameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return _gameOverUI.show();
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
