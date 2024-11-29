using UnityEngine;

public class HealthPickup : CollectableObject
{
    [SerializeField]
    [Min(1)]
    private int _healthRecovered = 1;
    protected override void onPickup(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (CharacterController2D.Instance?.ReceiveHealing(_healthRecovered) ?? false)
            {
                AkSoundEngine.PostEvent("Player_Regen", gameObject);
                AppCore.Instance?.PickUpItem(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
