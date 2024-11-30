using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField]
    private Keystone _firstkeystone;
    [SerializeField]
    private Keystone _secondkeystone;
    [SerializeField]
    private Keystone _thirdkeystone;
    [SerializeField]
    private Keystone _fourthkeystone;

    private void Awake()
    {
        _firstkeystone.SetMonolyth(0);
        _secondkeystone.SetMonolyth(1);
        _thirdkeystone.SetMonolyth(2);
        _fourthkeystone.SetMonolyth(3);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Resources.Load<AvailableCheats>("Available Cheats").GameFinished = true;
            AppCore.Instance?.RollCredits();
            /*if (IsLockOpen())
            {
                Resources.Load<AvailableCheats>("Available Cheats").GameFinished = true;
                AppCore.Instance?.RollCredits();
            }
            else
            {
                //IDK, play a sound and/or animate the door
                Debug.Log("Door closed!");
            }*/
        }
    }

    private bool IsLockOpen()
    {
        var manager = Resources.Load<MonolythManager>("Monolyth Manager");
        return (_firstkeystone.Glyph == manager.FirstMonolyth) &&
            (_secondkeystone.Glyph == manager.SecondMonolyth) &&
            (_thirdkeystone.Glyph == manager.ThirdMonolyth) &&
            (_fourthkeystone.Glyph == manager.FourthMonolyth);
    }
}
