using UnityEngine;

public class Monolyth : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    private int _monolythNumber;

    private int glyph;

    void Start()
    {
        switch (_monolythNumber)
        {
            case 0:
                glyph = Resources.Load<MonolythManager>("Monolyth Manager").FirstMonolyth;
                break;
            case 1:
                glyph = Resources.Load<MonolythManager>("Monolyth Manager").SecondMonolyth;
                break;
            case 2:
                glyph = Resources.Load<MonolythManager>("Monolyth Manager").ThirdMonolyth;
                break;
            default:
                glyph = Resources.Load<MonolythManager>("Monolyth Manager").FourthMonolyth;
                break;
        }
    }
}
