using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TeleportPlayer : MonoBehaviour
{
    public RoomGeneration roomGeneration;
    public List<RoomGeneration.Room> dungeon;

    public int connectedRoom;
    public int oppositeDoorID;
    public GameObject oppositeDoor;

    public bool teleportCooldown;

    private void Start()
    {
        roomGeneration = FindAnyObjectByType<RoomGeneration>();
        dungeon = roomGeneration.dungeon;

        oppositeDoor = dungeon[connectedRoom].spawns[oppositeDoorID];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            teleportCooldown = GameObject.FindGameObjectWithTag("Player").GetComponent<CanTeleport>().teleportCooldown;

            if (!teleportCooldown)
            {
                Teleport(GameObject.FindGameObjectWithTag("Player"), oppositeDoor);
            }
        }
    }

    public void Teleport(GameObject player, GameObject door)
    {
        Vector2 offset = Vector2.zero;
        if (Mathf.Abs(this.transform.parent.position.x - door.transform.parent.position.x) > 50.0f)
        {
            if ((this.transform.parent.position.x - door.transform.parent.position.x) > 50.0f)
            {
                offset = new Vector2(1.3f, -0.2f);
            }
            else
            {
                offset = new Vector2(-1.3f, 0.6f);
            }
        }
        else
        {
            if ((this.transform.parent.position.y - door.transform.parent.position.y) > 50.0f)
            {
                offset = new Vector2(1.3f, 0.8f);
            }
            else
            {
                offset = new Vector2(-1.3f, -0.1f);
            }
        }

        Debug.Log(offset);

        player.transform.position = new Vector3(offset.x + door.transform.localPosition.x, offset.y + door.transform.localPosition.y, 4.0f) + door.transform.parent.position;
    }
}
