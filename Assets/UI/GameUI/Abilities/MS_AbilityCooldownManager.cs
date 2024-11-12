using UnityEngine;
using UnityEngine.UI;

public class MS_AbilityCooldownManager : MonoBehaviour
{
    public Image AbilitiesCrystal; // Reference to the image (CooldownFullImage initially)
    public Sprite CooldownFullSprite; // Sprite for ability ready (CooldownFull)
    public Sprite CooldownEmptySprite; // Sprite for ability on cooldown (CooldownEmpty)

    private bool isOnCooldown = false; // Track the cooldown state
    private float maxCooldownTime; // To store the full cooldown duration
    private float currentCooldownTime; // To store the remaining cooldown time

    private void Start()
    {
        // Initialize to show the full (ready) image at the start
        AbilitiesCrystal.sprite = CooldownFullSprite;
        AbilitiesCrystal.fillAmount = 1; // Start with full image (ready)

        // Subscribe to the OnCooldownTick event from the CharacterController2D
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnCooldownTick += OnCooldownTick;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (CharacterController2D.Instance != null)
        {
            CharacterController2D.Instance.OnCooldownTick -= OnCooldownTick;
        }
    }

    private void OnCooldownTick(float remainingTime)
    {
        // Set the initial max cooldown time when the cooldown starts
        if (!isOnCooldown && remainingTime > 0)
        {
            maxCooldownTime = remainingTime; // Store the initial cooldown duration
            currentCooldownTime = remainingTime; // Set the current cooldown time
            AbilitiesCrystal.sprite = CooldownEmptySprite; // Switch to the empty (cooldown) image
            isOnCooldown = true;
        }

        // Update the current cooldown time as it ticks down
        if (isOnCooldown)
        {
            currentCooldownTime = remainingTime; // Update the current cooldown time
            // Update the fill amount of the image based on the remaining time
            AbilitiesCrystal.fillAmount = remainingTime / maxCooldownTime;
        }

        // When cooldown reaches zero, switch back to the full image
        if (remainingTime <= 0 && isOnCooldown)
        {
            AbilitiesCrystal.sprite = CooldownFullSprite; // Switch back to the full (ready) image
            AbilitiesCrystal.fillAmount = 1; // Ensure the fill is set to full
            isOnCooldown = false; // Cooldown is over
        }
    }
}
