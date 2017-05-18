using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map
{
    public static int NO_SCORE = -1;

    private static int sizeColumn, sizeRow, numberOfFields = 0;
    private static Cell[,] cells;
    private static Map m;

    public Map(int[,] map)
    {
        m = this;
        sizeRow = map.GetLength(0);
        sizeColumn = map.GetLength(1);
        cells = new Cell[sizeColumn, sizeRow];
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                if (map[j, i] == MapLoader.WALL_DESTRUCTIBLE)
                {
                    cells[i, j] = new Wall(this, i, j, true);
                }
                else if (map[j, i] == MapLoader.WALL_NOTDESTRUCTIBLE)
                {
                    cells[i, j] = new Wall(this, i, j, false);
                }
                else
                {
                    cells[i, j] = new Cell(this, i, j);
                    numberOfFields++;
                }
            }
        }
        initCells();
    }

    public static void notifyWallDestroyed(Vector3 position)
    {
        int[] pos = MapLoader.WorldCoordsToMapCoords(new Vector2(position.x, position.z));
        Wall w = (Wall)cells[pos[0], pos[1]];
        w.destruct();
        numberOfFields++;
        System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(getMap().initCells));
        t.Start();
    }

    public static Map getMap()
    {
        return m;
    }

    private void initCells()
    {
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                setVisibleCells(cells[i, j]);
            }
        }
    }

    private void resetCells()
    {
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                if (!cells[i, j].isBusy())
                {
                    cells[i, j].resetCell();
                }
            }
        }
    }

    private void setVisibleCells(Cell startCell)
    {
        List<Cell> visibleCells = new List<Cell>();
        if (startCell.isBusy())
        {
            startCell.setVisibleCells(visibleCells);
            return;
        }
        if (startCell.getColumn() == 6 && startCell.getRow() == 7)
        {
            int a = 6;
            a++;
        }
        for (double i = 0; i < Math.PI * 2; i += 0.02)
        {
            Line l = new Line(this, startCell.getColumn() + 0.5, startCell.getRow() + 0.5, Math.Cos(i) / 50, Math.Sin(i) / 50);
            l.addVisibleCells(visibleCells);
        }
        startCell.setVisibleCells(visibleCells);
    }

    public void calculateScores(Cell playerPosition)
    {
        resetCells();
        List<Cell> cellsToVisit = new List<Cell>();
        cellsToVisit.Add(playerPosition);
        playerPosition.setDistance(0);
        playerPosition.setScore(0);
        int counter = setScoresFromCell(playerPosition, 0);
        while (cellsToVisit.Count != 0 && counter < numberOfFields)
        {
            List<Cell> neighbours = cellsToVisit[0].getNotWallNeighbours();
            int distance = cellsToVisit[0].getDistance() + 1;
            foreach (Cell c in neighbours)
            {
                if (c != playerPosition && c.getDistance() == 0)
                {
                    c.setDistance(distance);
                    counter += setScoresFromCell(c, distance);
                    cellsToVisit.Add(c);
                }
            }
            cellsToVisit.RemoveAt(0);
        }
    }

    private int setScoresFromCell(Cell startCell, int score)
    {
        int cnt = 0;
        List<Cell> visibleCells = startCell.getVisibleCells();
        foreach (Cell c in visibleCells)
        {
            if (c.isBusy())
                continue;
            if (c.getScore() == NO_SCORE)
            {
                c.setScore(score);
                cnt++;
            }
        }
        return cnt;
    }

    public List<Cell> getGoodScoreCells(int score, int neighbourScore)
    {
        List<Cell> goodScoreCells = new List<Cell>();
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                if (cells[i, j].getScore() == score)
                {
                    if (neighbourScore == -1)
                    {
                        goodScoreCells.Add(cells[i, j]);
                        continue;
                    }
                    List<Cell> neighbours = cells[i, j].getNotWallNeighbours();
                    foreach (Cell c in neighbours)
                    {
                        if (c.getScore() == neighbourScore)
                        {
                            goodScoreCells.Add(cells[i, j]);
                            break;
                        }
                    }
                }
            }
        }
        return goodScoreCells;
    }

    public List<Cell> getBestScoreCells()
    {
        List<Cell> bestCells = new List<Cell>();
        int bestScore = 1;
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                if (cells[i, j].getScore() > bestScore)
                {
                    bestScore = cells[i, j].getScore();
                    bestCells.Clear();
                    bestCells.Add(cells[i, j]);
                }
                else if (cells[i, j].getScore() == bestScore)
                {
                    bestCells.Add(cells[i, j]);
                }
            }
        }
        return bestCells;
    }

    public Cell getCell(int columnPos, int rowPos)
    {
        if (columnPos < 0 || rowPos < 0 || columnPos >= sizeColumn || rowPos >= sizeRow)
            return null;
        return cells[columnPos, rowPos];
    }

    public int getSizeColumn()
    {
        return sizeColumn;
    }

    public int getSizeRow()
    {
        return sizeRow;
    }
}