using UnityEngine;

public class WalkInDoor : RoomDoor
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            UseDoor();
        }
    }
}