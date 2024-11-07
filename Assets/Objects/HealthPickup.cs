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
            collider.GetComponent<CharacterController2D>()?.ReceiveHealing(_healthRecovered);
            Destroy(gameObject);
        }
    }
}
