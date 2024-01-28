using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    #region Create variables
    /// Create the room class
    public class Room
    {
        /// Boolean determining if this room has been visited yet
        public bool visited = false;

        /// Booleans that state if the room opens up, down, right, or left
        public bool[] status = new bool[4];

        /// Size of the room (width and height)
        public Vector2Int roomSize = new Vector2Int(Random.Range(2, 3), Random.Range(2, 3));

        public GameObject[] spawns = new GameObject[4];

        public int roomID;

        public string roomType = "Standard";

        public bool unlocked = true;
    }

    /**
     * roomObject is the actual room object in Unity
     * 
     * size contains the width and height of the dungeon
     * startPos is the inital starting room from which the dungeon will be generated
     * offset is the size of each room to give adequate spacing
     * 
     * dungeon is a list containing every single room
     */
    public GameObject roomObject;

    public Vector2Int size;
    public int startPos = 0;
    public Vector2 offset;

    public List<Room> dungeon;

    private bool generating = true;
    private int recentEnd;
    private int minibossGeneration = 0;

    public int keyRooms = 0;
    #endregion

    /// I really hope that this comment is unnecessary, but this is called before the first update (when this script is loaded)
    private void Start()
    {
        /// Start the dungeon generator script
        DungeonGenerator();
    }

    /// This script generates the dungeon
    void DungeonGenerator()
    {
        #region Base dungeon creation
        /// Create the dungeon list
        dungeon = new List<Room>();

        /// Fill the dungeon with rooms
        for (int h = 0; h < size.y; h++)
        {
            for (int w = 0; w < size.x; w++)
            {
                dungeon.Add(new Room());
                dungeon[h * size.x + w].roomID = h * size.x + w;
            }
        }

        /// Set the current cell to be the initial starting position
        int currentRoom = startPos;

        /// Create a new stack for backtracking when we hit a dead end
        Stack<int> path = new Stack<int>();
        #endregion
            
        #region Main Loop
        /// Ensure that the program will kill itself if it gets out of hand
        int k = 0;
        while (k < size.x * size.y * 1000)
        {
            k++;

            /// Sets the room to have been visited
            dungeon[currentRoom].visited = true;

            /// Creates a list of the room's neighbors
            List<int> neighbors = CheckNeighbors(currentRoom);

            /// This room has no neighbors, meaning that it is stuck
            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    dungeon[recentEnd].roomType = "Major Treasure";
                    dungeon[recentEnd].roomSize = new Vector2Int(Random.Range(3, 5), Random.Range(3, 5));
                    dungeon[recentEnd].unlocked = false;

                    /// Break out of the loop once all rooms have been generated
                    break;                    
                }
                else
                {
                    if (generating)
                    {
                        if (Random.Range(0, minibossGeneration) == 0)
                        {
                            dungeon[currentRoom].roomType = "Key Room";
                            dungeon[currentRoom].roomSize = new Vector2Int(Random.Range(2, 4), Random.Range(2, 4));
                            minibossGeneration++;
                            keyRooms++;
                        }
                        else
                        {
                            dungeon[currentRoom].roomType = "Minor Treasure";
                            dungeon[currentRoom].roomSize = new Vector2Int(Random.Range(2, 3), Random.Range(2,3));
                            minibossGeneration = 0;
                        }
                            
                        recentEnd = currentRoom;
                    }

                    generating = false;

                    /// Remove this cell and continue to the cell behind it
                    currentRoom = path.Pop();
                }
            }
            /// This room has available neighbors to generate into
            else
            {
                generating = true;

                #region Generate a neighbor of the current room and select the neighbor room
                /// Add the current room to the stack
                path.Push(currentRoom);

                /// Choose a random neighbor to generate
                int newRoom = neighbors[Random.Range(0, neighbors.Count)];

                /// Checks to see if the chosen neighbor is below or to the right of the current room
                if (newRoom > currentRoom)
                {
                    /// Checks if the chosen neighbor is right of the current room
                    if (newRoom - 1 == currentRoom)
                    {
                        /// Properly sets the status of the current room and next room
                        dungeon[currentRoom].status[2] = true;
                        dungeon[newRoom].status[3] = true;

                        /// Switches the current cell to the neighbor cell
                        currentRoom = newRoom;
                    }
                    /// Checks if the chosen neighbor is below the current room
                    else if (newRoom - size.x == currentRoom)
                    {
                        dungeon[currentRoom].status[1] = true;
                        dungeon[newRoom].status[0] = true;

                        currentRoom = newRoom;
                    }
                }
                /// Checks if the chosen neighbor is to the left or above of the current room
                else if (newRoom < currentRoom)
                {
                    /// Checks if the chosen neighbor is left of the current room
                    if (newRoom + 1 == currentRoom)
                    {
                        dungeon[currentRoom].status[3] = true;
                        dungeon[newRoom].status[2] = true;

                        currentRoom = newRoom;

                    }
                    /// Checks if the chosen neighbor is above the current room
                    else if (newRoom + size.x == currentRoom)
                    {
                        dungeon[currentRoom].status[0] = true;
                        dungeon[newRoom].status[1] = true;

                        currentRoom = newRoom;
                    }
                }
                #endregion
            }
        }
        #endregion

        /// Generates all the rooms after making the dungeon list
        GenerateRoom();
    }

    /// Generate and create each room in Unity
    void GenerateRoom()
    {
        /// Generates the bare room for each room in the dungeon
        #region Loop throughout the dungeon
        for (int h = 0; h < size.y; h++)
        {
            for (int w = 0; w < size.x; w++)
            {
                /// Set the current room to be the corresponding room in the dungeon list
                Room currentRoom = dungeon[(h * size.x + w)];

                /// Ensure that the current room has been visited and should be generated
                if (currentRoom.visited)
                {
                    /// Instantiate the room with the proper name for labeling purposes
                    GameObject newRoom = Instantiate(roomObject, new Vector2(w * offset.x, -h * offset.y), Quaternion.identity);
                    
                    if (currentRoom.roomID == 0)
                    {
                        currentRoom.roomType = "Start Room";
                    }

                    if (currentRoom.roomType == "Standard")
                    {
                        if (Random.Range(0, 10) == 0)
                        {
                            currentRoom.roomType = "Minor Treasure";
                        }
                    }

                    /// Debug.Log(currentRoom.roomID);
                    newRoom.GetComponent<RoomBehavior>().UpdateRoom(currentRoom.status, currentRoom.roomSize, currentRoom);
                    newRoom.name += " " + w + "-" + h;
                }
            }
        }
        #endregion
    }

    /// Given a cell, checks to see the availability of its neighbors for potential generation
    List<int> CheckNeighbors(int room)
    {
        /// Creates a list that will contain potential neighbors
        List<int> neighbors = new List<int>();

        /// Checks each neighbor of the cell to determine if it can be generated into without causing issues
        #region Check the neighbors of the cell
        /// Checks to make sure that the cell is not on the top of the dungeon and that the cell has not been visited yet
        if (room - size.x >= 0 && !dungeon[(room - size.x)].visited)
        {
            /// Adds the neighbor to the list of potential neighbors
            neighbors.Add((room - size.x));
        }

        /// Checks to make sure that the cell is not on the bottom of the dungeon and that the cell has not been visited yet
        if (room + size.x < dungeon.Count && !dungeon[(room + size.x)].visited)
        {
            neighbors.Add((room + size.x));
        }

        /// Checks to make sure that the cell is not on the left of the dungeon and that the cell has not been visited yet
        if ((room + 1) % size.x != 0 && !dungeon[(room + 1)].visited)
        {
            neighbors.Add((room + 1));
        }

        /// Checks to make sure that the cell is not on the right of the dungeon and that the cell has not been visited yet
        if (room % size.x != 0 && !dungeon[(room - 1)].visited)
        {
            neighbors.Add((room - 1));
        }
        #endregion

        /// Returns the list of potential neighbors
        return neighbors;
    }
}