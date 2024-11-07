using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private Transform _teleportLocation;
    [SerializeField]
    private List<RoomDoor> _doors = new List<RoomDoor>();

    void Start()
    {
        bool isTeleporting = AppCore.Instance.IsTeleporting();
        if (isTeleporting)
            CharacterController2D.Instance?.TeleportCharacter(_teleportLocation.position);
        else
        {
            int doorIndex = AppCore.Instance.GetLastDoorIndex();
            if ((doorIndex != -1) && (doorIndex < _doors.Count))
                CharacterController2D.Instance?.TeleportCharacter(_doors[doorIndex].TeleportLocation.position);
            else
                Debug.LogError($"This door had a Door index of {doorIndex}");
        }

        AppCore.Instance?.LoadingDone();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (_teleportLocation != null)
        {
            Gizmos.DrawWireSphere(_teleportLocation.position, 0.25f);
            Handles.Label(_teleportLocation.position, "Level Teleport");
        }

        Gizmos.color = Color.yellow;
        if (_doors.Count != 0)
        {
            for (int i = 0; i < _doors.Count; i++)
            {
                var door = _doors[i];
                if (door != null)
                {
                    Gizmos.DrawWireSphere(door.TeleportLocation.position, 0.25f);
                    Gizmos.DrawWireCube(door.transform.position, new Vector3(1, 1, 1));
                    Handles.Label(door.transform.position, $"Door {i}, To {door.LevelDestination} door {door.DestinationDoorIndex}");
                }
                else
                    Debug.LogError("Door in array should not be null");
            }
        }
    }
}
