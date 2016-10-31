using UnityEngine;
using System.Collections.Generic;
using System;

public class HidingFromPlayer : MonoBehaviour
{
    
    public float MovementSpeed;
    public float RotationSpeed;

    public Transform player;

    private Enemy enemyMover;

    // Use this for initialization
    void Start()
    {
        enemyMover = new Enemy(this);
    }

    void Update()
    {
        enemyMover.moveEnemy();
    }

    public void moveEnemy(int nextColumn, int nextRow)
    {
        Vector3 currentPosition = transform.position;
        Vector2 nextPositionXY = MapLoader.MapCoordsToWorldCoordsFloat(new Vector2(nextColumn + 0.5f, nextRow + 0.5f));
        Vector3 nextPosition = new Vector3(nextPositionXY.x, currentPosition.y, nextPositionXY.y);
        Vector3 distanceVector = nextPosition - currentPosition;

        transform.forward = Vector3.RotateTowards(transform.forward, distanceVector, RotationSpeed * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, currentPosition + transform.forward, MovementSpeed * Time.deltaTime);
    }
}

public class Enemy
{
    private Transform player;
    private Transform enemy;
    private HidingFromPlayer enemyControl;

    private CellList pathToTarget;

    private int positionRow, positionColumn;
    private int playerPositionRow, playerPositionColumn;


    public Enemy(HidingFromPlayer hfp)
    {
        this.player = hfp.player;
        this.enemy = hfp.transform;
        enemyControl = hfp;

        updatePosition();
        updatePlayerPosition();
        pathToTarget = getClosestCellPath(positionColumn, positionRow);
    }

    private void updateRoute()
    {
        if (!isTargetRouteAvailable())
        {
            pathToTarget = getClosestCellPath(positionColumn, positionRow);
        }
    }

    public void moveEnemy()
    {
        updatePosition();
        updatePlayerPosition();
        updateRoute();
        
        if (pathToTarget.getParentCell() != null)
        {
            enemyControl.moveEnemy(pathToTarget.getParentCell().getColumn(), pathToTarget.getParentCell().getRow());
        }
        else if(pathToTarget != null && !isEnemyInPosition(pathToTarget.getColumn(), pathToTarget.getRow()))
        {
            enemyControl.moveEnemy(pathToTarget.getColumn(), pathToTarget.getRow());
        }
    }

    private bool isEnemyInPosition(int column, int row)
    {
        Vector2 current = MapLoader.MapCoordsToWorldCoordsFloat(new Vector2(column + 0.5f, row + 0.5f));
        Vector2 distance = new Vector2(current.x - enemy.position.x, current.y - enemy.position.z);
        if (distance.magnitude < 0.1)
        {
            return true;
        }
        return false;
    }

    private bool isTargetRouteAvailable()
    {
        CellList targetCell = pathToTarget.getRootCell(); //TODO: Dont compute the route if the plan is going forward
        if (pathToTarget.getColumn() == positionColumn && pathToTarget.getRow() == positionRow && isCellWorthReaching(targetCell.getColumn(), targetCell.getRow()))
            return true;
        return false;
    }

    private void updatePosition()
    {
        int[] enemyPos = MapLoader.WorldCoordsToMapCoords(new Vector2(enemy.position.x, enemy.position.z));
        positionColumn = enemyPos[0];
        positionRow = enemyPos[1];
    }

    private void updatePlayerPosition()
    {
        int[] playerPos = MapLoader.WorldCoordsToMapCoords(new Vector2(player.position.x, player.position.z));
        playerPositionColumn = playerPos[0];
        playerPositionRow = playerPos[1];
    }

    private bool isCellWorthReaching(int cellColumn, int cellRow)
    {
        
        if (cellColumn >= MapLoader.MapSizeX || cellRow >= MapLoader.MapSizeY)
            return false;

        if (MapLoader.Map[cellRow, cellColumn] == MapLoader.WALL)
            return false;


        Vector2 playerPositionFloat = MapLoader.WorldCoordsToMapCoordsFloat(new Vector2(player.position.x, player.position.z));

        Line l = new Line(playerPositionFloat.x, playerPositionFloat.y, cellColumn + 0.5, cellRow + 0.5);

        int minColumn = min(playerPositionColumn, cellColumn);
        int maxColumn = max(playerPositionColumn, cellColumn) + 1;
        int minRow = min(playerPositionRow, cellRow);
        int maxRow = max(playerPositionRow, cellRow) + 1;

        for (int i = minRow; i < maxRow; i++)
        {
            for (int j = minColumn; j < maxColumn; j++)
            {
                if (MapLoader.Map[i, j] == MapLoader.WALL && l.intersect(j, i))
                {
                    return true;
                }
                    
            }
        }
        return false;
    }

    private CellList getClosestCellPath(int startColumn, int startRow)
    {
        List<CellList> cellsToVisit = new List<CellList>();
        CellList rootCell = new CellList(null, startColumn, startRow);
        if (isCellWorthReaching(startColumn, startRow))
        {
            return rootCell;
        }
        cellsToVisit.Add(rootCell);
        while(cellsToVisit.Count != 0)
        {
            List<CellList> children = cellsToVisit[0].createNeighbourCellList();
            foreach(CellList c in children)
            {
                cellsToVisit.Add(c);
                if(isCellWorthReaching(c.getColumn(), c.getRow())) {
                    return buildBackwardCellList(c);
                }
            }
            cellsToVisit.RemoveAt(0);
        }
        return rootCell;
    }

    private CellList buildBackwardCellList(CellList c)
    {
        CellList backwardList = null;
        while(c != null)
        {
            backwardList = new CellList(backwardList, c.getColumn(), c.getRow());
            c = c.getParentCell();
        }
        return backwardList;
        
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

public class CellList
{
    int column, row;
    private CellList parent;

    public CellList(CellList parent, int column, int row)
    {
        if(column < 0 || row < 0 || column >= MapLoader.MapSizeX || row >= MapLoader.MapSizeY || MapLoader.Map[row,column] == MapLoader.WALL)
        {
            throw new Exception();
        }
        this.parent = parent;
        this.column = column;
        this.row = row;
    }

    public int getColumn()
    {
        return column;
    }

    public int getRow()
    {
        return row;
    }

    public bool isRootCell()
    {
        return parent == null;
    }

    public CellList getRootCell()
    {
        if (parent == null)
            return this;
        return parent.getRootCell();
    }

    public CellList getParentCell()
    {
        return parent;
    }

    public List<CellList> createNeighbourCellList()
    {
        List<CellList> neighbourList = new List<CellList>();
        int[] neighbours = { 0, -1, -1, 0, 1, 0, 0, 1 };
        for (int i = 0; i < neighbours.Length; i += 2)
        {
            try
            {
                neighbourList.Add(new CellList(this, column + neighbours[i], row + neighbours[i + 1]));
            }
            catch (Exception)
            {
                // The cell is not on the map or wall
            }
        }
        return neighbourList;
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

    private bool isBetween(int a, int b, double value)
    {
        if (a <= value && value <= b)
            return true;
        return false;
    }

    public bool intersect(int cellX, int cellY)
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
