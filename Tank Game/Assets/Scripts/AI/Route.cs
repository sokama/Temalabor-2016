using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Route
{
    List<Cell> route;
    public Route(Cell startCell)
    {
        route = new List<Cell>();
        route.Add(startCell);
    }

    public Route(List<Cell> r, Cell newCell)
    {
        route = new List<Cell>();
        route.AddRange(r);
        route.Add(newCell);
    }

    public List<Route> makeNeighbourRoutes(bool walls)
    {
        List<Cell> neighbours;
        if (walls)
            neighbours = getLastCell().getNeighbours();
        else
            neighbours = getLastCell().getNotWallNeighbours();
        List<Route> neighbourRoutes = new List<Route>();
        foreach (Cell c in neighbours)
        {
            neighbourRoutes.Add(new Route(route, c));
        }
        return neighbourRoutes;
    }

    public Cell getLastCell()
    {
        return route[route.Count - 1];
    }

    public Cell getNextCell(Cell c)
    {
        int n = route.IndexOf(c);
        if (n == -1)
            return null;
        if (n + 1 == route.Count)
            return route[n];
        return route[n + 1];
    }

    public int getH(List<Cell> endCells)
    {
        if (endCells.Count == 0)
            return -1;
        int min = 0;
        for (int i = 1; i < endCells.Count; i++)
        {
            if (endCells[i].getDistance() < endCells[min].getDistance())
                min = i;
        }
        return endCells[min].getDistance();
    }

    public int getF(List<Cell> endCells)
    {
        int cnt = 0;
        for (int i = 0; i < route.Count; i++)
        {
            cnt += route[i].getCost();
        }
        return cnt - 2 + getH(endCells);
    }
}

