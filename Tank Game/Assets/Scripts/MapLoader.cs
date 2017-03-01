using UnityEngine;
using System.Collections;
using System;

public class MapLoader : MonoBehaviour
{

    public Transform floor;
    public GameObject wall;

    public const int FIELD = -1;
    public const int WALL = -2;

    private static int mapSizeX = 12;
    private static int mapSizeY = 12;
    private static int mapCellSize = 5;
    private static int[,] map = { { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 },
                                { -2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2 },
                                { -2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2 },
                                { -2, -1, -1, -1, -2, -2, -2, -2, -1, -1, -1, -2 },
                                { -2, -1, -1, -1, -2, -1, -1, -2, -1, -2, -2, -2 },
                                { -2, -1, -2, -1, -2, -1, -1, -2, -1, -2, -1, -2 },
                                { -2, -1, -1, -1, -2, -1, -1, -2, -1, -1, -1, -2 },
                                { -2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -2 },
                                { -2, -1, -1, -2, -1, -1, -1, -1, -2, -1, -1, -2 },
                                { -2, -1, -1, -1, -2, -1, -1, -2, -1, -1, -1, -2 },
                                { -2, -1, -1, -1, -2, -1, -1, -2, -1, -1, -1, -2 },
                                { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 }};


    void Start()
    {
        HidingFromPlayer.Map = new Map(map);
        CreateFloor();
        CreateWalls();
    }

    private void CreateWalls()
    {
        //wall.transform.localScale = new Vector3(2*mapCellSize/4.0f,1, 2 * mapCellSize / 4.0f);
        for (int row = 0; row < mapSizeY; row++)
        {
            for (int column = 0; column < mapSizeX; column++)
            {
                if(map[row, column] == WALL)
                {
                    Vector2 coords = MapCoordsToWorldCoords(column, row);              
                    Instantiate(wall, new Vector3(coords.x, 1, coords.y), Quaternion.identity);
                }
            }
        }
    }

    private void CreateFloor()
    {
        Transform floorTransform = (Transform)Instantiate(floor, new Vector3(0, -0.05f, 0), Quaternion.identity);
        Vector3 floorScale = floorTransform.localScale;
        floorTransform.localScale = new Vector3(floorScale.x * mapSizeX * (mapCellSize / 4.0f), floorScale.y * (mapCellSize / 4.0f), floorScale.z * mapSizeY * (mapCellSize / 4.0f));
    }

    public static Vector2 MapCoordsToWorldCoords(int x, int y)
    {
        return new Vector2(mapCellSize * (x + 0.5f - mapSizeX / 2f), mapCellSize * (-y - 0.5f + mapSizeY / 2f));
    }

    public static int[] WorldCoordsToMapCoords(Vector2 cellWorld)
    {
        int[] cellMap = new int[2];

        int x = Mathf.FloorToInt(cellWorld.x / (float)mapCellSize + mapSizeX / 2f);
        int y = Mathf.FloorToInt(mapSizeY / 2f - cellWorld.y / (float)mapCellSize);

        cellMap[0] = x;
        cellMap[1] = y;

        return cellMap;
    }

    public static Vector2 WorldCoordsToMapCoordsFloat(Vector2 cellWorld)
    {
        return new Vector2(cellWorld.x / (float)mapCellSize + mapSizeX / 2f, 
                            mapSizeY / 2f - cellWorld.y / (float)mapCellSize);
    }

    public static Vector2 MapCoordsToWorldCoordsFloat(Vector2 cellMap)
    {
        return new Vector2(mapCellSize * (cellMap.x - mapSizeX / 2f), mapCellSize * (-cellMap.y + mapSizeY / 2f));
    }
}


