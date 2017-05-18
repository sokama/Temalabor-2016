using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder
{

    public Route searchRoute(Cell startCell, List<Cell> endCells, bool walls)
    {
        Route pathToEnd = null;
        List<Cell> visitedCells = new List<Cell>();
        List<Route> openCellList = new List<Route>();
        openCellList.Add(new Route(startCell));
        visitedCells.Add(startCell);
        while (true)
        {
            Route minRoute = getBestRoute(openCellList, endCells);
            if (minRoute == null)
            {
                return null;
            }
            if (endCells.Contains(minRoute.getLastCell()))
            {
                pathToEnd = minRoute;
                break;
            }
            visitedCells.Add(minRoute.getLastCell());
            List<Route> nextRoutes = minRoute.makeNeighbourRoutes(walls);
            foreach (Route r in nextRoutes)
            {
                if (!visitedCells.Contains(r.getLastCell()))
                {
                    openCellList.Add(r);
                }
            }
            openCellList.Remove(minRoute);
        }
        return pathToEnd;
    }

    private Route getBestRoute(List<Route> routes, List<Cell> endCells)
    {
        if (routes.Count == 0)
            return null;
        int min = 0;
        for (int i = 1; i < routes.Count; i++)
        {
            if (routes[i].getF(endCells) < routes[min].getF(endCells))
            {
                min = i;
            }
        }
        return routes[min];
    }
}
