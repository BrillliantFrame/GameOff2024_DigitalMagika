using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            CharacterController2D.Instance?.OnDamage();
            AkSoundEngine.PostEvent("Trap_Hit", gameObject);
        }
    }
}
