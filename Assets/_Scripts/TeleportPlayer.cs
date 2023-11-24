using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public RoomGeneration roomGeneration;
    public List<RoomGeneration.Room> dungeon;

    public int connectedRoom;
    public int oppositeDoor;

    public Vector2 teleportPos;

    public bool teleportCooldown;

    private void Start()
    {
        roomGeneration = FindAnyObjectByType<RoomGeneration>();
        dungeon = roomGeneration.dungeon;

        teleportPos = dungeon[connectedRoom].spawns[oppositeDoor];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            teleportCooldown = GameObject.FindGameObjectWithTag("Player").GetComponent<CanTeleport>().teleportCooldown;

            if (!teleportCooldown)
            {
                Teleport(GameObject.FindGameObjectWithTag("Player"), gameObject.transform.position, teleportPos);
            }
        }
    }

    public void Teleport(GameObject player, Vector2 door, Vector2 spawn)
    {
        Vector2 offset = new Vector2(player.transform.position.x - door.x, player.transform.position.y - door.y);

        player.transform.position = new Vector2(spawn.x + offset.x, spawn.y + offset.y);
    }
}
