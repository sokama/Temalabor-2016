using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Line
{
    private double x, y;
    private double mx, my;
    private Map map;

    public Line(Map m, double x, double y, double mx, double my)
    {
        map = m;
        this.x = x;
        this.y = y;
        this.mx = mx;
        this.my = my;
    }

    bool isEnd(double x, double y)
    {
        if (x < 0 || x > map.getSizeColumn())
        {
            return true;
        }
        if (y < 0 || y > map.getSizeRow())
        {
            return true;
        }
        int col = (int)Math.Floor(x);
        int row = (int)Math.Floor(y);
        if (map.getCell(col, row).isBusy())
            return true;
        return false;
    }

    public void addVisibleCells(List<Cell> list)
    {
        double xi = x, yi = y;
        int row, col;
        int prevCol = -1, prevRow = -1;
        while (!isEnd(xi, yi))
        {
            xi += mx;
            yi += my;
            col = (int)Math.Floor(xi);
            row = (int)Math.Floor(yi);
            if (prevCol != col || prevRow != row)
            {
                Cell c = map.getCell((int)Math.Floor(xi), (int)Math.Floor(yi));
                if (!list.Contains(c))
                {
                    list.Add(c);
                }
                prevRow = row;
                prevCol = col;
            }
        }
    }
}

public class LineWithPoint
{
    private double a, b;
    private bool intersectedByWall = false;

    public LineWithPoint(double x1, double y1, double x2, double y2)
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

    public void checkIntersect(int cellX, int cellY)
    {
        if (intersectedByWall)
            return;
        intersectedByWall = intersect(cellX, cellY);
    }

    public bool isIntersected()
    {
        return intersectedByWall;
    }

    private bool isBetween(int a, int b, double value)
    {
        if (a <= value && value <= b)
            return true;
        return false;
    }

    private bool intersect(int cellX, int cellY)
    {
        if (isBetween(cellX, cellX + 1, getX(cellY)))
            return true;
        if (isBetween(cellX, cellX + 1, getX(cellY + 1)))
            return true;
        if (isBetween(cellY, cellY + 1, getY(cellX)))
            return true;
        if (isBetween(cellY, cellY + 1, getY(cellX + 1)))
            return true;
        return false;
    }
}

