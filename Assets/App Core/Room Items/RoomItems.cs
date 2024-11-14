using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Items", menuName = "ScriptableObjects/Room Items/New Room Items", order = 2)]
public class RoomItems : ScriptableObject
{
    [SerializeField]
    public List<RoomItem> Items = new List<RoomItem>();

    public void ResetRoomItems()
    {
        foreach (var item in Items)
        {
            item.Available = true;
        }
    }

    /*public void HidePickedUpItems()
    {
        foreach (var item in _items)
        {
            if (!item.Available)
            {
                item.Item.SetActive(false);
            }
        }
    }

    public void PickUpItem(GameObject pickedUp)
    {
        foreach (var item in _items)
        {
            if (pickedUp == item.Item)
            {
                item.Available = false;
                break;
            }
        }
    }*/
}

[Serializable]
public class RoomItem
{
    //public GameObject Item;
    public bool Available = true;
}