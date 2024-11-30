using UnityEngine;

public class Wwise_LaunchInGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Onclick()
    {
        AkSoundEngine.PostEvent("InGame_Music", gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
