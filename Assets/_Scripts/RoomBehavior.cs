using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;

    public RoomGeneration roomGeneration;
    public List<RoomGeneration.Room> dungeon;

    public int roomID;

    private void Awake()
    {
        roomGeneration = FindAnyObjectByType<RoomGeneration>();
        dungeon = roomGeneration.dungeon;
    }

    public void UpdateRoom(bool[] status, Vector2Int size, RoomGeneration.Room room)
    {
        roomID = room.roomID;

        int doorUp = Random.Range(1, size.x + 1);
        for (int i = 1; i < size.x + 1; i++)
        {
            GameObject wall = Instantiate(walls[0], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(transform.position.x + ((size.x + 1) * -6.0f + i * 12.0f), transform.position.y + (size.y * 6.0f - 0.5f));

            if (status[0])
            {
                GameObject door = Instantiate(doors[0], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + ((size.x + 1) * -6.0f + i * 12.0f), transform.position.y + (size.y * 6.0f - 0.5f));

                if (doorUp == i)
                {
                    door.GetComponent<SpriteRenderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 1.1f);
                    door.GetComponent<BoxCollider2D>().isTrigger = true;

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID - (int)roomGeneration.size.x;
                    door.GetComponent<TeleportPlayer>().oppositeDoor = 1;

                    room.spawns[0] = door.transform.position;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[0], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + ((size.x + 1) * -6.0f + i * 12.0f), transform.position.y + (size.y * 6.0f - 0.5f));
            }
        }

        int doorDown = Random.Range(1, size.x + 1);
        for (int j = 1; j < size.x + 1; j++)
        {
            GameObject wall = Instantiate(walls[1], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(transform.position.x + ((size.x + 1) * -6.0f + j * 12.0f), transform.position.y + (size.y * -6.0f + 0.5f));

            if (status[1])
            {
                GameObject door = Instantiate(doors[1], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + ((size.x + 1) * -6.0f + j * 12.0f), transform.position.y + (size.y * -6.0f + 0.5f));

                if (doorDown == j)
                {
                    door.GetComponent<SpriteRenderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, -1.1f);
                    door.GetComponent<BoxCollider2D>().isTrigger = true;

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID + (int)roomGeneration.size.x;
                    door.GetComponent<TeleportPlayer>().oppositeDoor = 0;

                    room.spawns[1] = door.transform.position;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[1], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + ((size.x + 1) * -6.0f + j * 12.0f), transform.position.y + (size.y * -6.0f + 0.5f));
            }
        }

        int doorRight = Random.Range(1, size.y + 1);
        for (int k = 1; k < size.y + 1; k++)
        {
            GameObject wall = Instantiate(walls[2], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(transform.position.x + (size.x * 6.0f - 0.5f), transform.position.y + ((size.y + 1) * -6.0f + k * 12.0f));

            if (status[2])
            {
                GameObject door = Instantiate(doors[2], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + (size.x * 6.0f - 0.5f), transform.position.y + ((size.y + 1) * -6.0f + k * 12.0f));

                if (doorRight == k)
                {
                    door.GetComponent<SpriteRenderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().offset = new Vector2(1.1f, 0.0f);
                    door.GetComponent<BoxCollider2D>().isTrigger = true;

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID + 1;
                    door.GetComponent<TeleportPlayer>().oppositeDoor = 3;

                    room.spawns[2] = door.transform.position;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[2], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + (size.x * 6.0f - 0.5f), transform.position.y + ((size.y + 1) * -6.0f + k * 12.0f));
            }
        }

        int doorLeft = Random.Range(1, size.y + 1);
        for (int l = 1; l < size.y + 1; l++)
        {
            GameObject wall = Instantiate(walls[3], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(transform.position.x + (size.x * -6.0f + 0.5f), transform.position.y + ((size.y + 1) * -6.0f + l * 12.0f));

            if (status[3])
            {
                GameObject door = Instantiate(doors[3], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + (size.x * -6.0f + 0.5f), transform.position.y + ((size.y + 1) * -6.0f + l * 12.0f));

                if (doorLeft == l)
                {
                    door.GetComponent<SpriteRenderer>().enabled = false;
                    door.GetComponent<BoxCollider2D>().offset = new Vector2(-1.1f, 0.0f);
                    door.GetComponent<BoxCollider2D>().isTrigger = true;

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID - 1;
                    door.GetComponent<TeleportPlayer>().oppositeDoor = 2;

                    room.spawns[3] = door.transform.position;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[3], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(transform.position.x + (size.x * -6.0f + 0.5f), transform.position.y + ((size.y + 1) * -6.0f + l * 12.0f));
            }
        }

        if (room.roomType != "Standard")
        {
            Debug.Log(room.roomID.ToString() + " " + room.roomType);
        }
    }
}
