using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(collision.collider.GetComponent<CharacterController2D>()?.OnDamage());
        }
    }
}
