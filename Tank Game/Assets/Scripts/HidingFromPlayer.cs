using UnityEngine;
using System.Collections;

public class HidingFromPlayer : MonoBehaviour {

    public Transform player;
    public GameObject wall; //DEBUG

    private ArrayList debugWalls = new ArrayList();

    // Use this for initialization
    void Start()
    {
        
    }

    void Update()
    {
        //DEBUG
        foreach (Object wall in debugWalls)
        {
            Destroy((GameObject)wall);
        }
        debugWalls.Clear();

        for (int row = 0; row < MapLoader.MapSizeY; row++)
        {
            for (int column = 0; column < MapLoader.MapSizeX; column++)
            {
                if (isCellWorthReaching(row, column))
                {
                    Vector2 coords = MapLoader.MapCoordsToWorldCoords(column, row);
                    debugWalls.Add(Instantiate(wall, new Vector3(coords.x, 0.0f, coords.y), Quaternion.identity));
                }
            }
        }
    }

    private bool isBetween(int a, int b, double value)
    {
        if (a <= value && value <= b)
            return true;
        return false;
    }

    public bool intersect(Line l, int cellX, int cellY)
    {
        if (isBetween(cellY, cellY + 1, l.getY(cellX + 0.2)))
            return true;
        if (isBetween(cellY, cellY + 1, l.getY(cellX + 0.8)))
            return true;
        if (isBetween(cellX, cellX + 1, l.getX(cellY + 0.2)))
            return true;
        if (isBetween(cellX, cellX + 1, l.getX(cellY + 0.8)))
            return true;
        return false;
    }

    public bool isCellWorthReaching(int cellX, int cellY)
    {
        if (cellX >= MapLoader.MapSizeX || cellY >= MapLoader.MapSizeY)
            return false;

        if (MapLoader.Map[cellX,cellY] == MapLoader.WALL)
            return false;

        //TODO x és y playerPosition-ben fel van cserélve! Vissza kell cserélni.
        int[] playerPosTemp = MapLoader.WorldCoordsToMapCoords(new Vector2(player.position.x, player.position.z));
        int[] playerPosition = { playerPosTemp[1], playerPosTemp[0] };  

        Line l = new Line(playerPosition[0] + 0.5, playerPosition[1] + 0.5, cellX + 0.5, cellY + 0.5);

        int minX = min(playerPosition[0], cellX);
        int maxX = max(playerPosition[0], cellX) + 1;
        int minY = min(playerPosition[1], cellY);
        int maxY = max(playerPosition[1], cellY) + 1;

        //Itt nem muszáj az összes cellát végignézni, lehet csökkenteni ha figyelembe vesszük az egyenes meredekségét
        for (int i = minX; i < maxX; i++)
        {
            for (int g = minY; g < maxY; g++)
            {
                if (MapLoader.Map[i, g] == MapLoader.WALL && intersect(l, i, g))
                    return true;
            }
        }
        return false;
    }

    private int min(int i1, int i2)
    {
        if (i1 < i2)
            return i1;
        return i2;
    }

    private int max(int i1, int i2)
    {
        if (i1 > i2)
            return i1;
        return i2;
    }
}

public class Line
{
    private double a, b;

    public Line(double x1, double y1, double x2, double y2)
    {
        if (y1 == y2)
        {
            a = double.PositiveInfinity;
            b = y1;
        }
        else
        {
            a = (x1 - x2) / (y1 - y2);
            b = x1 - a * y1;
        }
    }

    public double getX(double y)
    {
        if (a == double.PositiveInfinity)
        {
            return -1.0;
        }
        return a * y + b;

    }

    public double getY(double x)
    {
        if (a == 0)
        {
            return -1;
        }
        else if (a == double.PositiveInfinity)
        {
            return b;
        }
        return (x - b) / a;
    }
}
