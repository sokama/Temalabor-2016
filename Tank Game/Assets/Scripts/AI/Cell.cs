using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell
{

    private int columnPosition, rowPosition;
    private int score, distance;
    private Map Map;
    private List<Cell> visibleCells;

    public Cell(Map map, int column, int row)
    {
        this.Map = map;
        this.columnPosition = column;
        this.rowPosition = row;
        this.score = -1;
    }

    public int getColumn()
    {
        return columnPosition;
    }

    public int getRow()
    {
        return rowPosition;
    }

    public void resetCell()
    {
        distance = 0;
        score = Map.NO_SCORE;
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public virtual int getCost()
    {
        return 1;
    }

    public int getDistance()
    {
        return distance;
    }

    public void setDistance(int distance)
    {
        this.distance = distance;
    }

    public List<Cell> getVisibleCells()
    {
        return visibleCells;
    }

    public void setVisibleCells(List<Cell> cells)
    {
        visibleCells = cells;
    }


    public virtual bool isBusy()
    {
        return false;
    }

    public List<Cell> getNotWallNeighbours()
    {
        List<Cell> notWallNeughbours = new List<Cell>();
        int[] neighbours = { 0, -1, -1, 0, 1, 0, 0, 1 };
        for (int i = 0; i < neighbours.Length; i += 2)
        {
            Cell c = Map.getCell(columnPosition + neighbours[i], rowPosition + neighbours[i + 1]);
            if (c != null && !c.isBusy())
                notWallNeughbours.Add(c);
        }
        return notWallNeughbours;
    }

    public List<Cell> getNeighbours()
    {
        List<Cell> neighbourList = new List<Cell>();
        int[] neighbours = { 0, -1, -1, 0, 1, 0, 0, 1 };
        for (int i = 0; i < neighbours.Length; i += 2)
        {
            Cell c = Map.getCell(columnPosition + neighbours[i], rowPosition + neighbours[i + 1]);
            if (c != null)
                neighbourList.Add(c);
        }
        return neighbourList;
    }

    public static int min(int i1, int i2)
    {
        if (i1 < i2)
            return i1;
        return i2;
    }

    public static int max(int i1, int i2)
    {
        if (i1 > i2)
            return i1;
        return i2;
    }
}