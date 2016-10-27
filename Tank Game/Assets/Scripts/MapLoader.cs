using UnityEngine;
using System.Collections;
using System;

public class MapLoader : MonoBehaviour {

    public Transform floor;
    public Transform wall;

    int mapSizeX = 12;
    int mapSizeY = 12;
    int mapBlockSize = 4;
    int[,] map = new int[12, 12] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                   { 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1 },
                                   { 1, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1 },
                                   { 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1 },
                                   { 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1 },
                                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                   { 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1 },
                                   { 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1 },
                                   { 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1 },
                                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }};

	void Start () {
        CreateFloor();
        CreateWalls();
    }

    private void CreateWalls()
    {
        for (int row = 0; row < mapSizeY; row++)
        {
            for (int column = 0; column < mapSizeX; column++)
            {
                switch (map[row,column])
                {
                    case 0:
                        break;
                    case 1:
                        Vector2 coords = MapCoordsToWorldCoords(column, row);
                        Instantiate(wall, new Vector3(coords.x, 1, coords.y), Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void CreateFloor()
    {
        Transform floorTransform = (Transform) Instantiate(floor, new Vector3(0, -0.05f, 0), Quaternion.identity);
        Vector3 floorScale = floorTransform.localScale;
        floorTransform.localScale = new Vector3(floorScale.x * mapSizeX, floorScale.y, floorScale.z * mapSizeY);
    }

    public Vector2 MapCoordsToWorldCoords(int x, int y)
    {
        return new Vector2(mapBlockSize * (x + 0.5f - mapSizeX / 2f), mapBlockSize * (-y - 0.5f + mapSizeY / 2f));
    }
}
