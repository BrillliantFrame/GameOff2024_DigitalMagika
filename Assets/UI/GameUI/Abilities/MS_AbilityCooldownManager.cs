using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MS_AbilityCooldownManager : MonoBehaviour
{
    public GameObject AbilitiesA; // Reference to Abilities_A GameObject
    public GameObject AbilitiesB; // Reference to Abilities_B GameObject
    public TMP_Text cooldownText_1;     // UI Text to display cooldown
    public TMP_Text cooldownText_2;     // UI Text to display cooldown

    public float cooldownDuration = 5f; // Example cooldown duration in seconds
    private float cooldownTimer;
    private bool isOnCooldown;

    private void Start()
    {
        // Initialize to show Abilities_A
        AbilitiesA.SetActive(true);
        AbilitiesB.SetActive(false);
        cooldownText_1.gameObject.SetActive(false); // Hide cooldown text initially
        cooldownText_2.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Debugging to test function
        if (Input.GetKeyDown(KeyCode.Home))
        {
            TriggerCooldown(5);
        }

        if (isOnCooldown)
        {
            // Update cooldown timer
            cooldownTimer -= Time.deltaTime;
            cooldownText_1.text = Mathf.Ceil(cooldownTimer).ToString() + "s"; // Show remaining cooldown time
            cooldownText_2.text = Mathf.Ceil(cooldownTimer).ToString() + "s";

            // If cooldown is over, switch back to Abilities_A
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                AbilitiesA.SetActive(true);
                AbilitiesB.SetActive(false);
                cooldownText_1.gameObject.SetActive(false);
                cooldownText_2.gameObject.SetActive(false);
            }
        }
    }

    // Call this method to trigger the cooldown
    public void TriggerCooldown(float cooldownDuration)
    {
        if (!isOnCooldown)
        {
            isOnCooldown = true;
            cooldownTimer = cooldownDuration; // Set from CharacterController2D

            // Show Abilities_B and display cooldown time
            AbilitiesA.SetActive(false);
            AbilitiesB.SetActive(true);
            cooldownText_1.gameObject.SetActive(true);
            cooldownText_2.gameObject.SetActive(true);
        }
    }

}
