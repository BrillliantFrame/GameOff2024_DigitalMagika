using UnityEngine;

public class Keystone : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    private int _glyph = 0;
    public int Glyph
    {
        get { return _glyph; }
    }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private int _connectedMonolyth = 0;

    public void SetMonolyth(int connectedMonolyth)
    {
        _connectedMonolyth = connectedMonolyth;
        _spriteRenderer.sprite = Resources.Load<MonolythManager>("Monolyth Manager").GetKeystoneGlyph(_connectedMonolyth, _glyph);
    }

    public void ActivateKeystone()
    {
        if (_glyph < 3)
        {
            _glyph++;
        }
        else
        {
            _glyph = 0;
        }

        _spriteRenderer.sprite = Resources.Load<MonolythManager>("Monolyth Manager").GetKeystoneGlyph(_connectedMonolyth, _glyph);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            AkSoundEngine.PostEvent("Player_InteractionMonolith", gameObject);
            ActivateKeystone();
        }
    }
}
