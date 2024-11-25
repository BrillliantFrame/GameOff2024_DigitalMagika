#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private Transform _teleportLocation;
    [SerializeField]
    private List<RoomDoor> _doors = new List<RoomDoor>();

    [SerializeField]
    private List<GameObject> _items = new List<GameObject>();
    [SerializeField]
    private RoomItems _roomItems;

    protected void Start()
    {
        AppCore.Instance?.SetRoomManager(this);
        bool isTeleporting = AppCore.Instance?.IsTeleporting() ?? true;
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

        if (_roomItems != null)
        {
            HidePickedUpItems();
        }

        AppCore.Instance?.LoadingDone();
    }

    private void HidePickedUpItems()
    {
        for (int i = 0; i < _roomItems.Items.Count; i++)
        {
            var item = _roomItems.Items[i];
            if (!item.Available)
            {
                _items[i].SetActive(false);
            }
        }
    }

    public void PickUpItem(GameObject pickedUp)
    {
        for (int i = 0; i < _roomItems.Items.Count; i++)
        {
            var item = _roomItems.Items[i];

            if (pickedUp == _items[i])
            {
                item.Available = false;
                i = _roomItems.Items.Count;
            }
        }
    }

#if UNITY_EDITOR
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
#endif
}
