using UnityEngine;

public class Keystone : MonoBehaviour
{
    [SerializeField]
    [Range(0, 6)]
    private int _glyph = 0;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private MonolythManager _monolythManager;

    private void Awake()
    {
        _monolythManager = Resources.Load<MonolythManager>("Monolyth Manager");
        _spriteRenderer.sprite = _monolythManager.GetKeystoneGlyph(_glyph);
    }

    public int Glyph
    {
        get { return _glyph; }
    }

    public void ActivateKeystone()
    {
        if (_glyph < 5)
        {
            _glyph++;
        }
        else
        {
            _glyph = 0;
        }

        _spriteRenderer.sprite = _monolythManager.GetKeystoneGlyph(_glyph);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            ActivateKeystone();
        }
    }
}
