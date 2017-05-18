using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MapLoader : MonoBehaviour
{

    public Transform floor;
    public GameObject wallDestructible;
    public GameObject wallNotDestructible;

    public const int FIELD = -1;
    public const int WALL_DESTRUCTIBLE = -2;
    public const int WALL_NOTDESTRUCTIBLE = -3;

    private static int mapSizeX = 12;
    private static int mapSizeY = 12;
    private static int mapCellSize = 5;
    private static int[,] map;

    void Start()
    {
        ReadMap();
        EnemyController.Map = new Map(map);
        CreateFloor();
        CreateWalls();
    }

    void ReadMap()
    {
        StreamReader sr = new StreamReader("Maps/map.txt");
        String[] mapData = sr.ReadLine().Split(',');
        mapSizeX = Convert.ToInt32(mapData[0]);
        mapSizeY = Convert.ToInt32(mapData[1]);
        map = new int[mapSizeY, mapSizeX];
        mapCellSize = Convert.ToInt32(mapData[2]);
        for (int row = 0; row < mapSizeY; row++)
        {
            String[] data = sr.ReadLine().Split(',');
            for (int column = 0; column < mapSizeX; column++)
            {
                map[row, column] = Convert.ToInt32(data[column]);
            }
        }
    }

    private void CreateWalls()
    {
        Vector3 wallScale = new Vector3(mapCellSize / 5.0f, 1, mapCellSize / 5.0f);
        wallDestructible.transform.localScale = wallScale;
        wallNotDestructible.transform.localScale = wallScale;
        for (int row = 0; row < mapSizeY; row++)
        {
            for (int column = 0; column < mapSizeX; column++)
            {
                if (map[row, column] == WALL_DESTRUCTIBLE)
                {
                    Vector2 coords = MapCoordsToWorldCoords(column, row);
                    Instantiate(wallDestructible, new Vector3(coords.x, 1, coords.y), Quaternion.identity);
                }
                if (map[row, column] == WALL_NOTDESTRUCTIBLE)
                {
                    Vector2 coords = MapCoordsToWorldCoords(column, row);
                    Instantiate(wallNotDestructible, new Vector3(coords.x, 1, coords.y), Quaternion.identity);
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


