using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    public Transform TeleportLocation;
    public MapScenes LevelDestination;
    public int DestinationDoorIndex;

    public void UseDoor()
    {
        AppCore.Instance?.MoveToRoom(LevelDestination, DestinationDoorIndex);
    }
}