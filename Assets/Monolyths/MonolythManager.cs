using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monolyth Manager", menuName = "ScriptableObjects/Monolyth Items/New Monolyth Manager", order = 1)]
public class MonolythManager : ScriptableObject
{
    [SerializeField]
    private int _firstMonolyth = 0;
    public int FirstMonolyth
    {
        get { return _firstMonolyth; }
    }

    [SerializeField]
    private int _secondMonolyth = 0;
    public int SecondMonolyth
    {
        get { return _secondMonolyth; }
    }

    [SerializeField]
    private int _thirdMonolyth = 0;
    public int ThirdMonolyth
    {
        get { return _thirdMonolyth; }
    }

    [SerializeField]
    private int _fourthMonolyth = 0;
    public int FourthMonolyth
    {
        get { return _fourthMonolyth; }
    }

    [SerializeField]
    private List<Sprite> _redMonolythGlyphs;

    [SerializeField]
    private List<Sprite> _blueMonolythGlyphs;

    [SerializeField]
    private List<Sprite> _greenMonolythGlyphs;

    [SerializeField]
    private List<Sprite> _yellowMonolythGlyphs;

    public void ShuffleMonolythsAnswer()
    {
        _firstMonolyth = Random.Range(0, 4);
        _secondMonolyth = Random.Range(0, 4);
        _thirdMonolyth = Random.Range(0, 4);
        _fourthMonolyth = Random.Range(0, 4);
    }

    public Sprite GetKeystoneGlyph(int monolyth, int glyph)
    {
        switch (monolyth)
        {
            default:
                if (glyph < _redMonolythGlyphs.Count)
                    return _redMonolythGlyphs[glyph];
                return _redMonolythGlyphs[0];

            case 1:
                if (glyph < _blueMonolythGlyphs.Count)
                    return _blueMonolythGlyphs[glyph];
                return _blueMonolythGlyphs[0];
            case 2:
                if (glyph < _greenMonolythGlyphs.Count)
                    return _greenMonolythGlyphs[glyph];
                return _greenMonolythGlyphs[0];
            case 3:
                if (glyph < _yellowMonolythGlyphs.Count)
                    return _yellowMonolythGlyphs[glyph];
                return _yellowMonolythGlyphs[0];
        }
    }
}
