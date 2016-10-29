using UnityEngine;
using System.Collections;
using System;

public class MapLoader : MonoBehaviour
{

    public Transform floor;
    public GameObject wall;

    public static int FIELD = 0;
    public static int WALL = 1;
    public static int PLAYER = 2;
    public static int ENEMY = 3;

    public static int MapSizeX = 12;
    public static int MapSizeY = 12;
    public static int MapCellSize = 4;
    public static int[,] Map = new int[12, 12] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
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

    void Start()
    {
        CreateFloor();
        CreateWalls();
    }

    private void CreateWalls()
    {
        for (int row = 0; row < MapSizeY; row++)
        {
            for (int column = 0; column < MapSizeX; column++)
            {
                switch (Map[row, column])
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
                //if(m.isCellWorthReaching(row,column))
                //{
                //    Vector2 coords = MapCoordsToWorldCoords(column, row);
                //    Instantiate(wall, new Vector3(coords.x, 0.0f, coords.y), Quaternion.identity);
                //}
            }
        }
    }

    private void CreateFloor()
    {
        Transform floorTransform = (Transform)Instantiate(floor, new Vector3(0, -0.05f, 0), Quaternion.identity);
        Vector3 floorScale = floorTransform.localScale;
        floorTransform.localScale = new Vector3(floorScale.x * MapSizeX, floorScale.y, floorScale.z * MapSizeY);
    }

    public static Vector2 MapCoordsToWorldCoords(int x, int y)
    {
        return new Vector2(MapCellSize * (x + 0.5f - MapSizeX / 2f), MapCellSize * (-y - 0.5f + MapSizeY / 2f));
    }

    public static int[] WorldCoordsToMapCoords(Vector2 cellWorld)
    {
        int[] cellMap = new int[2];

        int x = Mathf.FloorToInt(cellWorld.x / (float)MapCellSize + MapSizeX / 2f);
        int y = Mathf.FloorToInt(MapSizeY / 2f - cellWorld.y / (float)MapCellSize);

        cellMap[0] = x;
        cellMap[1] = y;

        return cellMap;
    }

    public static Vector2 WorldCoordsToMapCoordsFloat(Vector2 cellWorld)
    {
        return new Vector2(cellWorld.x / (float)MapCellSize + MapSizeX / 2f, 
                            MapSizeY / 2f - cellWorld.y / (float)MapCellSize);
    }
}

//class Cell
//{
//    public static int FIELD = 0;
//    public static int WALL = 1;
//    public static int PLAYER = 2;
//    public static int ENEMY = 3;

//    private int x, y;
//    private int type;

//    public Cell(int x, int y, int type)
//    {
//        this.x = x;
//        this.y = y;
//        this.type = type;
//    }

//    public int getType()
//    {
//        return type;
//    }

//    public int getX()
//    {
//        return x;
//    }

//    public int getY()
//    {
//        return y;
//    }

//    public bool isWall()
//    {
//        return type == WALL;
//    }

//    private bool isBetween(int a, int b, double value)
//    {
//        if (a <= value && value <= b)
//            return true;
//        return false;
//    }

//    public bool intersect(Line l) // +getXY(x+delta,x+1-delta): hamis pozitívak kiszűrése
//    {
//        if (isBetween(y, y + 1, l.getY(x + 0.2)))
//            return true;
//        if (isBetween(y, y + 1, l.getY(x + 0.8)))
//            return true;
//        if (isBetween(x, x + 1, l.getX(y + 0.2)))
//            return true;
//        if (isBetween(x, x + 1, l.getX(y + 0.8)))
//            return true;
//        return false;
//    }
//}

//class Map
//{
//    private Cell[,] cells;
//    private int sizeX, sizeY;
//    private Cell playerPosition, enemyPosition;

//    public Map(int[,] cells)
//    {
//        sizeX = cells.GetLength(0);
//        sizeY = cells.GetLength(1);
//        this.cells = new Cell[sizeX, sizeY];
//        for (int i = 0; i < sizeX; i++)
//        {
//            for (int g = 0; g < sizeY; g++)
//            {
//                this.cells[i, g] = new Cell(i, g, cells[i, g]);
//                if (cells[i, g] == Cell.PLAYER)
//                    playerPosition = this.cells[i, g];
//                else if (cells[i, g] == Cell.ENEMY)
//                    enemyPosition = this.cells[i, g];
//            }
//        }
//    }

//    public int getSizeX()
//    {
//        return sizeX;
//    }

//    public int getSizeY()
//    {
//        return sizeY;
//    }

//    private int min(int i1, int i2)
//    {
//        if (i1 < i2)
//            return i1;
//        return i2;
//    }

//    private int max(int i1, int i2)
//    {
//        if (i1 > i2)
//            return i1;
//        return i2;
//    }

//    public void setPlayerPosition(int x, int y)
//    {
//        playerPosition = cells[x, y];
//    }

//    public bool isCellWorthReaching(int x, int y)
//    {
//        if (x >= sizeX || y >= sizeY)
//            return false;

//        Cell cell = cells[x, y];

//        if (cell.isWall() || cell == playerPosition)
//            return false;

//        Line l = new Line(playerPosition.getX() + 0.5, playerPosition.getY() + 0.5, cell.getX() + 0.5, cell.getY() + 0.5);

//        int minX = min(playerPosition.getX(), x);
//        int maxX = max(playerPosition.getX(), x) + 1;
//        int minY = min(playerPosition.getY(), y);
//        int maxY = max(playerPosition.getY(), y) + 1;

//        //Itt nem muszáj az összes cellát végignézni, lehet csökkenteni ha figyelembe vesszük az egyenes meredekségét
//        for (int i = minX; i < maxX; i++)
//        {
//            for (int g = minY; g < maxY; g++)
//            {
//                if (cells[i, g].isWall() && cells[i, g].intersect(l))
//                    return true;
//            }
//        }
//        return false;
//    }
//}



