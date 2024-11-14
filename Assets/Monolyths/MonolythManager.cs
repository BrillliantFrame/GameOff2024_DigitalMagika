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

    public void ShuffleMonolyths()
    {
        _firstMonolyth = Random.Range(0, 7);
        _secondMonolyth = Random.Range(0, 7);
        _thirdMonolyth = Random.Range(0, 7);
        _fourthMonolyth = Random.Range(0, 7);
    }


}
