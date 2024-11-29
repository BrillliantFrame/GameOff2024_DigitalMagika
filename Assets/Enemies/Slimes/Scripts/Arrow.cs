using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private float _arrowSpeed = 1f;

    private Rigidbody2D _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            CharacterController2D.Instance?.OnDamage();
        }
        Destroy(gameObject);
    }

    public void Shoot(float direction)
    {
        _rigidBody.linearVelocity = new Vector2(_arrowSpeed * direction, 0f);
    }
}
