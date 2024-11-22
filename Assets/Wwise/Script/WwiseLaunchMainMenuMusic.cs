using UnityEngine;

public class WwiseLaunchMain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (this.gameObject.activeInHierarchy)
        {
            AkSoundEngine.PostEvent("MainMenu_Music", gameObject); 
        }
        else
        {
            Debug.LogError("GameObject is inactive!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
