using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MS_AbilityCooldownManager : MonoBehaviour
{
    public GameObject AbilitiesA; // Reference to Abilities_A GameObject
    public GameObject AbilitiesB; // Reference to Abilities_B GameObject
    public TMP_Text cooldownText_1;     // UI Text to display cooldown
    public TMP_Text cooldownText_2;     // UI Text to display cooldown

    private bool isOnCooldown;

    private void Start()
    {
        // Initialize to show Abilities_A
        AbilitiesA.SetActive(true);
        AbilitiesB.SetActive(false);
        cooldownText_1.gameObject.SetActive(false); // Hide cooldown text initially
        cooldownText_2.gameObject.SetActive(false);

        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnCooldownTick += UpdateCooldownDisplay;
        }
    }

    private void OnDestroy()
    {
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnCooldownTick -= UpdateCooldownDisplay;
        }
    }

    private void UpdateCooldownDisplay(float remainingTime)
    {
        if (!isOnCooldown)
        {
            // Show Abilities_B and display cooldown time
            AbilitiesA.SetActive(false);
            AbilitiesB.SetActive(true);
            cooldownText_1.gameObject.SetActive(true);
            cooldownText_2.gameObject.SetActive(true);
            isOnCooldown = true;
        }

        // Update cooldown timer text with remaining time
        cooldownText_1.text = Mathf.Ceil(remainingTime).ToString() + "s";
        cooldownText_2.text = Mathf.Ceil(remainingTime).ToString() + "s";

        // When cooldown reaches zero, reset to Abilities_A
        if (remainingTime <= 0)
        {
            isOnCooldown = false;
            AbilitiesA.SetActive(true);
            AbilitiesB.SetActive(false);
            cooldownText_1.gameObject.SetActive(false);
            cooldownText_2.gameObject.SetActive(false);
        }
    }

}
