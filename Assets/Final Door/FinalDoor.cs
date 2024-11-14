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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (IsLockOpen())
            {
                Debug.Log("Door open!");
            } 
            else
            {
                //IDK, play a sound and/or animate the door
                Debug.Log("Door closed!");
            }
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
