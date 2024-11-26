using UnityEngine;
using AK.Wwise;

public class Wwise_UISounds : MonoBehaviour
{
    public AK.Wwise.Event clickEvent = null;

    void Start()
    {
        // Ensure the game object is registered with Wwise
        AkSoundEngine.RegisterGameObj(gameObject);
    }

    public void OnClick()
    {
        // Post the click event when the button is clicked
        if (clickEvent != null)
        {
            clickEvent.Post(gameObject);
        }
        else
        {
            Debug.LogError("Wwise click event not assigned or event ID not found.");
        }
    }

    void OnDestroy()
    {
        // Optionally, unregister the game object when it's destroyed
        AkSoundEngine.UnregisterGameObj(gameObject);
    }
}
