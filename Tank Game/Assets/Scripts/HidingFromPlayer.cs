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
        if (isBetween(cellX, cellX + 1, l.getX(cellY)))
            return true;
        if (isBetween(cellX, cellX + 1, l.getX(cellY + 1)))
            return true;
        if (isBetween(cellY, cellY + 1, l.getY(cellX)))
            return true;
        if (isBetween(cellY, cellY + 1, l.getY(cellX + 1)))
            return true;
        return false;
    }

    public bool isCellWorthReaching(int cellX, int cellY)
    {
        if (cellX >= MapLoader.MapSizeX || cellY >= MapLoader.MapSizeY)
            return false;

        if (MapLoader.Map[cellX,cellY] == MapLoader.WALL)
            return false;

        int[] playerPosition = MapLoader.WorldCoordsToMapCoords(new Vector2(player.position.x, player.position.z));
        Vector2 playerPositionFloat = MapLoader.WorldCoordsToMapCoordsFloat(new Vector2(player.position.x, player.position.z));

        Line l = new Line(playerPositionFloat.y, playerPositionFloat.x, cellX + 0.5, cellY + 0.5);

        int minX = min(playerPosition[1], cellX);
        int maxX = max(playerPosition[1], cellX) + 1;
        int minY = min(playerPosition[0], cellY);
        int maxY = max(playerPosition[0], cellY) + 1;

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
        if (x1 == x2)
        {
            a = double.PositiveInfinity;
            b = x1;
        }
        else
        {
            a = (y1 - y2) / (x1 - x2);
            b = y1 - a * x1;
        }
    }
    public double getX(double y)
    {
        if (a == 0)
        {
            return -1;
        }
        else if (a == double.PositiveInfinity)
        {
            return b;
        }
        return (y - b) / a;
    }

    public double getY(double x)
    {
        if (a == double.PositiveInfinity)
        {
            return -1.0;
        }
        return a * x + b;

    }
}
