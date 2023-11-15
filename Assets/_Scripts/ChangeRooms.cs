using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{
    public void teleportPlayer(GameObject player, Vector2 door, Vector2 spawn)
    {
        Vector2 offset = new Vector2(player.transform.position.x - door.x, player.transform.position.y - door.y);

        player.transform.position = new Vector2(spawn.x + offset.x, spawn.y + offset.y);
    }
}
