using UnityEngine;

public class Keystone : MonoBehaviour
{
    [SerializeField]
    [Range(0, 6)]
    private int _glyph = 0;
    public int Glyph
    {
        get { return _glyph; }
    }

    public void ActivateKeystone()
    {
        if (_glyph < 6)
        {
            _glyph++;
        }
        else
        {
            _glyph = 0;
        }
    }
}
