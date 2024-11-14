using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Items Manager", menuName = "ScriptableObjects/Room Items/New Game Items Manager", order = 1)]
public class GameItemsManager : ScriptableObject
{
    [SerializeField]
    private List<RoomItems> _roomsItemSetup = new List<RoomItems>();

    public void ResetGameItems()
    {
        foreach (var roomItems in _roomsItemSetup)
        {
            roomItems.ResetRoomItems();
        }
    }
}