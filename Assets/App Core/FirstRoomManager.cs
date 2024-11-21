using UnityEngine;

public class FirstRoomManager : RoomManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        Debug.Log("============Start gameplay music here!!============");
    }

}
