using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject[] doors;
    public GameObject floor;

    public RoomGeneration roomGeneration;
    public List<RoomGeneration.Room> dungeon;

    public int roomID;

    private Vector2 lockedDoorPos;
    public GameObject lockedDoor;
    public int majorTreasureID;

    public GameObject[] puzzles;
    public TreasureManager tm;
    public TimeCountdownScript tcs;

    public GameObject boss;
    public GameObject bossPuzzle;

    private void Awake()
    {
        roomGeneration = FindAnyObjectByType<RoomGeneration>();
        tm = FindAnyObjectByType<TreasureManager>();
        tcs = FindAnyObjectByType<TimeCountdownScript>();
        dungeon = roomGeneration.dungeon;

        majorTreasureID = 0;

        foreach (RoomGeneration.Room room in dungeon)
        {
            if (room.roomType == "Major Treasure")
            {
                majorTreasureID = room.roomID;
                break;
            }
        }
    }

    public void UpdateRoom(bool[] status, Vector2Int size, RoomGeneration.Room room)
    {
        roomID = room.roomID;

        for (int horizontal = 0; horizontal < size.x; horizontal++)
        {
            for (int vertical = 0; vertical < size.y; vertical++)
            {
                GameObject generatedFloor = Instantiate(floor, transform.position, Quaternion.identity, transform);

                generatedFloor.transform.parent = transform;
                generatedFloor.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - vertical * 4.0f - horizontal * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * vertical + 2.0f * horizontal));
            }
        }

        int doorUp = Random.Range(1, size.x + 1);
        for (int i = 1; i < size.x + 1; i++)
        {
            GameObject wall = Instantiate(walls[0], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (i - 1) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f + 2.0f * (i - 1)));

            if (status[0])
            {
                GameObject door = Instantiate(doors[0], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (i - 1) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f + 2.0f * (i - 1)));

                if (doorUp == i)
                {
                    if ((dungeon[roomID - (int)roomGeneration.size.x].unlocked == true) && (room.roomType != "Major Treasure"))
                    {
                        door.GetComponent<TilemapRenderer>().enabled = false;
                        door.GetComponent<CompositeCollider2D>().isTrigger = true;
                        door.GetComponent<CompositeCollider2D>().offset = new Vector2(0.5f, 0.25f);
                    }
                    else if (room.roomType == "Major Treasure")
                    {
                        Debug.Log("Boss Room");
                    }
                    else
                    {
                        lockedDoor = door;
                        lockedDoorPos = new Vector2(1.0f, 1.0f);
                    }

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID - (int)roomGeneration.size.x;
                    door.GetComponent<TeleportPlayer>().oppositeDoorID = 1;

                    room.spawns[0] = door;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[0], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (i - 1) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f + 2.0f * (i - 1)));
            }
        }

        int doorDown = Random.Range(1, size.x + 1);
        for (int j = 1; j < size.x + 1; j++)
        {
            GameObject wall = Instantiate(walls[1], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (j - 1) * 4.0f - (size.y - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f + 2.0f * (j - 1) - 2.0f * (size.y - 1.0f)));

            if (status[1])
            {
                GameObject door = Instantiate(doors[1], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (j - 1) * 4.0f - (size.y - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f + 2.0f * (j - 1) - 2.0f * (size.y - 1.0f)));

                if (doorDown == j)
                {
                    if (dungeon[roomID + (int)roomGeneration.size.x].unlocked == true)
                    {
                        door.GetComponent<TilemapRenderer>().enabled = false;
                        door.GetComponent<CompositeCollider2D>().isTrigger = true;
                        door.GetComponent<CompositeCollider2D>().offset = new Vector2(-0.5f, -0.25f);
                    }
                    else if ((room.roomType == "Major Treasure") && (room.roomType != "Major Treasure"))
                    {
                        Debug.Log("Boss Room");
                    }
                    else
                    {
                        lockedDoor = door;
                        lockedDoorPos = new Vector2(-1.0f, -1.0f);
                    }

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID + (int)roomGeneration.size.x;
                    door.GetComponent<TeleportPlayer>().oppositeDoorID = 0;

                    room.spawns[1] = door;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[1], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (j - 1) * 4.0f - (size.y - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f + 2.0f * (j - 1) - 2.0f * (size.y - 1.0f)));
            }
        }

        int doorRight = Random.Range(1, size.y + 1);
        for (int k = 1; k < size.y + 1; k++)
        {
            GameObject wall = Instantiate(walls[2], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (k - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * (k - 1.0f)));

            if (status[2])
            {
                GameObject door = Instantiate(doors[2], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (k - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * (k - 1.0f)));

                if (doorRight == k)
                {
                    if (dungeon[roomID + 1].unlocked == true)
                    {
                        door.GetComponent<TilemapRenderer>().enabled = false;
                        door.GetComponent<CompositeCollider2D>().isTrigger = true;
                        door.GetComponent<CompositeCollider2D>().offset = new Vector2(0.5f, -0.25f);
                    }
                    else if ((room.roomType == "Major Treasure") && (room.roomType != "Major Treasure"))
                    {
                        Debug.Log("Boss Room");
                    }
                    else
                    {
                        lockedDoor = door;
                        lockedDoorPos = new Vector2(1.0f, -1.0f);
                    }

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID + 1;
                    door.GetComponent<TeleportPlayer>().oppositeDoorID = 3;

                    room.spawns[2] = door;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[2], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (k - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * (k - 1.0f)));
            }
        }

        int doorLeft = Random.Range(1, size.y + 1);
        for (int l = 1; l < size.y + 1; l++)
        {
            GameObject wall = Instantiate(walls[3], transform.position, Quaternion.identity, transform);
            wall.transform.parent = transform;
            wall.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (l - 1) * 4.0f - (size.x - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * (l - 1) + 2.0f * (size.x - 1.0f)));

            if (status[3])
            {
                GameObject door = Instantiate(doors[3], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (l - 1) * 4.0f - (size.x - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * (l - 1) + 2.0f * (size.x - 1.0f)));

                if (doorLeft == l)
                {
                    if (dungeon[roomID - 1].unlocked == true)
                    {
                        door.GetComponent<TilemapRenderer>().enabled = false;
                        door.GetComponent<CompositeCollider2D>().isTrigger = true;
                        door.GetComponent<CompositeCollider2D>().offset = new Vector2(-0.5f, -0.25f);
                    }
                    else if ((room.roomType == "Major Treasure") && (room.roomType != "Major Treasure"))
                    {
                        Debug.Log("Boss Room");
                    }
                    else
                    {
                        lockedDoor = door;
                        lockedDoorPos = new Vector2(-1.0f, -1.0f);
                    }

                    door.GetComponent<TeleportPlayer>().connectedRoom = roomID - 1;
                    door.GetComponent<TeleportPlayer>().oppositeDoorID = 2;

                    room.spawns[3] = door;
                }
            }
            else
            {
                GameObject door = Instantiate(doors[3], transform.position, Quaternion.identity, transform);
                door.transform.parent = transform;
                door.transform.position = new Vector2(
                    transform.position.x + ((Mathf.Max(size.x, size.y) - 1.0f) * 4.0f - (Mathf.Abs(size.y - size.x)) * 2.0f - (l - 1) * 4.0f - (size.x - 1.0f) * 4.0f),
                    transform.position.y + ((size.y - size.x) * 1.0f - 2.0f * (l - 1) + 2.0f * (size.x - 1.0f)));
            }
        }

        Debug.Log(room.roomType);
        if (room.roomType == "Minor Treasure" || room.roomType == "Key Room")
        {
            GeneratePuzzle();
        }
    }

    public bool CheckForUnlock()
    {
        if (roomGeneration.keyRooms <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        if (dungeon[majorTreasureID].unlocked == false)
        {
            if ((CheckForUnlock() == true) && (lockedDoor != null))
            {
                lockedDoor.GetComponent<TilemapRenderer>().enabled = false;
                lockedDoor.GetComponent<CompositeCollider2D>().isTrigger = true;
                lockedDoor.GetComponent<CompositeCollider2D>().offset = new Vector2(0.5f * lockedDoorPos.x, -0.25f * lockedDoorPos.y);

                GameObject bossClone = Instantiate(boss, lockedDoor.GetComponent<TeleportPlayer>().oppositeDoor.transform.parent.position + new Vector3(0.0f, 0.0f, 100.0f), Quaternion.identity, transform);
                bossClone.transform.parent = lockedDoor.GetComponent<TeleportPlayer>().oppositeDoor.transform.parent;
                bossClone.GetComponent<Boss>().rb = lockedDoor.GetComponent<TeleportPlayer>().oppositeDoor.transform.parent.gameObject.GetComponent<RoomBehavior>();

                lockedDoor.GetComponent<TeleportPlayer>().Teleport(GameObject.FindGameObjectWithTag("Player"), lockedDoor.GetComponent<TeleportPlayer>().oppositeDoor, lockedDoor);


                dungeon[majorTreasureID].unlocked = true;

                if (roomGeneration.keyRooms != 0)
                {
                    tm.AddTreasure(100);
                    tcs.currentTime += 60;
                }
            }
        }
    }

    public void GeneratePuzzle()
    {
        GameObject puzzle = Instantiate(puzzles[Random.Range(0, puzzles.Length)], transform.position + new Vector3(0.0f, 0.0f, 100.0f), Quaternion.identity, transform);
        puzzle.transform.parent = transform;
    }

    public void GenerateBossPuzzle()
    {
        GameObject puzzle = Instantiate(bossPuzzle, transform.position + new Vector3(0.0f, 0.0f, 100.0f), Quaternion.identity, transform);
        puzzle.transform.parent = transform;
    }
}
