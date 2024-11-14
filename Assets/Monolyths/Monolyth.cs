using UnityEngine;

public class Monolyth : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    private int _monolythNumber;

    private int _glyph;

    void Start()
    {
        var manager = Resources.Load<MonolythManager>("Monolyth Manager");

        switch (_monolythNumber)
        {
            case 0:
                _glyph = manager.FirstMonolyth;
                break;
            case 1:
                _glyph = manager.SecondMonolyth;
                break;
            case 2:
                _glyph = manager.ThirdMonolyth;
                break;
            default:
                _glyph = manager.FourthMonolyth;
                break;
        }
    }
}
