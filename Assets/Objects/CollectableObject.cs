using UnityEngine;

public abstract class CollectableObject : MonoBehaviour
{
    protected abstract void onPickup(Collider2D collider);

    void OnTriggerEnter2D(Collider2D collider)
    {
        onPickup(collider);
    }
}
