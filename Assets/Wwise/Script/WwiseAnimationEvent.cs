using UnityEngine;
using AK.Wwise;

public class WwiseAnimationEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    public void PlayWwiseEvent()
    {
        if (MyEvent != null)
        {
            MyEvent.Post(gameObject);
        }
        else
        {
            Debug.LogError("Not Assigned");
        }
        
    }
}
