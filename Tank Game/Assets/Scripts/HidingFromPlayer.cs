using UnityEngine;
using System.Collections.Generic;
using System;

//TODO: Rename script (e.g EnemyController)
public class HidingFromPlayer : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;
    public float MinimumWaitBetweenShoots = 0.3f;
    private float waitBeforeShoot = 0f;

    public Transform player;


    int n = 0;

    //public GameObject Bullet;
    //public float BulletSpeed = 10f;
    private MovingStrategy strategy;
    private static int enemyHealth = 100;
    private static bool healthChanged = false;
    public static bool canshoot = false;

    public static Map Map;

    void Update()
    {
        if (player == null)
            return;
        if (strategy == null || healthChanged)
        {
            makeStrategy();
            healthChanged = false;
        }
        strategy.MoveEnemy();
        if (strategy.isPlayerShootable())
        {
            rotateTower();
            if (GetComponent<TankShoot>().CanShoot() && waitBeforeShoot <= 0f)
            {
                GetComponent<TankShoot>().Shoot();
                waitBeforeShoot = MinimumWaitBetweenShoots;
            }
        }

        if (waitBeforeShoot > 0f)
        {
            waitBeforeShoot -= Time.deltaTime;
            if (waitBeforeShoot < 0f)
                waitBeforeShoot = 0f;
        }

        canshoot = GetComponent<TankShoot>().CanShoot() && waitBeforeShoot <= 0f;
    }

    private void makeStrategy()
    {
        if(enemyHealth < 25)
        {
            strategy = new CriticalStrategy(this);
        }
        else if(enemyHealth < 50)
        {
            strategy = new HidingStrategy(this);
        }
        else if(enemyHealth < 75)
        {
            strategy = new AttackingFromFarStrategy(this);
        }
        else
        {
            strategy = new FollowingStrategy(this);
        }
    }

    public static void notifiyHealth(int health)
    {
        enemyHealth = health;
        healthChanged = true;
    }

    public void moveEnemy(int nextColumn, int nextRow)
    {
        Vector3 currentPosition = transform.position;
        Vector2 nextPositionXY = MapLoader.MapCoordsToWorldCoordsFloat(new Vector2(nextColumn + 0.5f, nextRow + 0.5f));
        Vector3 nextPosition = new Vector3(nextPositionXY.x, currentPosition.y, nextPositionXY.y);
        Vector3 distanceVector = nextPosition - currentPosition;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, currentPosition + transform.forward, MovementSpeed * Time.deltaTime);
        if (!vectorEquals(newPosition, transform.position))
        {
            transform.position = newPosition;
            transform.forward = Vector3.RotateTowards(transform.forward, distanceVector, RotationSpeed * Time.deltaTime, 0.0f);

        }
    }

    private void rotateTower()
    {
        Transform tower = transform.FindChild("Graphics").FindChild("Tower");
        tower.rotation = Quaternion.LookRotation(player.position - transform.position);
    }

    private bool floatEquals(float f1, float f2)
    {
        float d = 0.001f;
        if (f1 - d < f2 && f2 < f1 + d)
        {
            return true;
        }
        return false;
    }

    private bool vectorEquals(Vector3 v1, Vector3 v2)
    {
        if (!floatEquals(v1.x, v2.x))
            return false;
        if (!floatEquals(v1.y, v2.y))
            return false;
        if (!floatEquals(v1.z, v2.z))
            return false;
        return true;
    }
}

public abstract class MovingStrategy
{
    private Transform player;
    private Transform enemy;
    private HidingFromPlayer enemyControl;
    private bool couldshoot = true;

    protected Cell enemyPosition;
    protected Cell playerPosition;

    private Route targetRoute;

    protected Map Map = HidingFromPlayer.Map;

    public MovingStrategy(HidingFromPlayer hfp)
    {
        this.player = hfp.player;
        this.enemy = hfp.transform;
        enemyControl = hfp;
    }

    private void updatePosition()
    {
        int[] enemyPos = MapLoader.WorldCoordsToMapCoords(new Vector2(enemy.position.x, enemy.position.z));
        enemyPosition = Map.getCell(enemyPos[0], enemyPos[1]);
    }

    private bool updatePlayerPosition()
    {
        int[] playerPos = MapLoader.WorldCoordsToMapCoords(new Vector2(player.position.x, player.position.z));
        Cell newPlayerPosition = Map.getCell(playerPos[0], playerPos[1]);
        if (playerPosition == null || playerPosition.getColumn() != newPlayerPosition.getColumn() || playerPosition.getRow() != newPlayerPosition.getRow())
        {
            playerPosition = newPlayerPosition;
            return true;
        }
        return false;
    }
    public bool isPlayerShootable()
    {
        Vector2 realPlayerPosition = MapLoader.WorldCoordsToMapCoordsFloat(new Vector2(player.position.x, player.position.z));
        Vector2 realEnemyPosition = MapLoader.WorldCoordsToMapCoordsFloat(new Vector2(enemy.position.x, enemy.position.z));

        Line l = new Line(realPlayerPosition.x, realPlayerPosition.y, realEnemyPosition.x, realEnemyPosition.y);

        Cell minCell = Map.getCell(Cell.min(playerPosition.getColumn(), enemyPosition.getColumn()), Cell.min(playerPosition.getRow(), enemyPosition.getRow()));
        Cell maxCell = Map.getCell(Cell.max(playerPosition.getColumn(), enemyPosition.getColumn()) + 1, Cell.max(playerPosition.getRow(), enemyPosition.getRow()) + 1);

        for (int i = minCell.getColumn(); i < maxCell.getColumn(); i++)
        {
            for (int j = minCell.getRow(); j < maxCell.getRow(); j++)
            {
                if (Map.getCell(i, j).isWall())
                {
                    l.checkIntersect(i, j);
                }
            }
        }
        if (l.isIntersected())
            return false;
        return true;
    }

    private bool isEnemyInPosition(Cell c)
    {
        Vector2 current = MapLoader.MapCoordsToWorldCoordsFloat(new Vector2(c.getColumn() + 0.5f, c.getRow() + 0.5f));
        Vector2 distance = new Vector2(current.x - enemy.position.x, current.y - enemy.position.z);
        if (distance.magnitude < 0.1)
        {
            return true;
        }
        return false;
    }

    public void MoveEnemy()
    {
        updatePosition();
        if (updatePlayerPosition())
        {
            Map.calculateScores(playerPosition);
            targetRoute = null;
        }

        if(targetRoute == null || couldshoot != HidingFromPlayer.canshoot)
        {
            PathFinder pf = new PathFinder(Map.getIntArray());
            targetRoute = pf.searchRoute(enemyPosition, getEndCells());
        }

        if(targetRoute != null)
        {
            Cell nextCell = targetRoute.getNextCell(enemyPosition);
            if(nextCell == null)
            {
                targetRoute = null;
            }
            else if(!isEnemyInPosition(nextCell))
            {
                enemyControl.moveEnemy(nextCell.getColumn(), nextCell.getRow());
            }
        }
        couldshoot = HidingFromPlayer.canshoot;
    }

    public abstract List<Cell> getEndCells();

}

public class FollowingStrategy : MovingStrategy
{
    public static int DistanceFromTarget = 2;
    public FollowingStrategy(HidingFromPlayer hfp) : base(hfp)
    {

    }

    public override List<Cell> getEndCells()
    {
        List<Cell> targetCells = Map.getGoodScoreCells(0, -1);
        for(int i = 0;i<targetCells.Count;i++)
        {
            if(targetCells[i].getDistance() != DistanceFromTarget)
            {
                targetCells.RemoveAt(i--);
            }
        }
        return targetCells;
    }
}

public class AttackingFromFarStrategy : MovingStrategy
{
    public AttackingFromFarStrategy(HidingFromPlayer hfp) : base(hfp)
    {

    }

    public override List<Cell> getEndCells()
    {
        if (HidingFromPlayer.canshoot)
            return Map.getGoodScoreCells(0, 1);
        else
            return Map.getGoodScoreCells(1, 0);
    }
}

public class CriticalStrategy : MovingStrategy
{
    public CriticalStrategy(HidingFromPlayer hfp) : base(hfp)
    {

    }

    public override List<Cell> getEndCells()
    {
        List<Cell> cellsSeenByPlayer = Map.getGoodScoreCells(0, -1);
        if(cellsSeenByPlayer.Contains(enemyPosition))
            return Map.getGoodScoreCells(1,-1);
        return Map.getBestScoreCells();
    }
}

public class HidingStrategy : MovingStrategy
{
    public HidingStrategy(HidingFromPlayer hfp) : base(hfp)
    {

    }

    public override List<Cell> getEndCells()
    {
        return Map.getBestScoreCells();
    }
}

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

    public List<Route> makeNeighbourRoutes()
    {
        List<Cell> neighbours = getLastCell().getNotWallNeighbours();
        List<Route> neighbourRoutes = new List<Route>();
        foreach (Cell c in neighbours)
        {
            neighbourRoutes.Add(new Route(route,c));
        }
        return neighbourRoutes;
    }

    private Route makeNewRoute(Cell newCell)
    {
        return new Route(route, newCell);
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
        return route[n+1];
    }

    public int getH(List<Cell> endCells) {
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

    private int getDistance(Cell c1, Cell c2)
    {
        return Math.Abs(c1.getColumn() - c2.getColumn()) + Math.Abs(c1.getRow() - c2.getRow());
    }


    public int getF(List<Cell> endCells)
    {
        return route.Count - 2 + getH(endCells);
    }
}

public class PathFinder
{
    private int[,] map;

    public PathFinder(int[,] m)
    {
        map = m;
    }

    public Route searchRoute(Cell startCell, List<Cell> endCells)
    {
        Route pathToEnd = null;
        List<Cell> visitedCells = new List<Cell>();
        List<Route> openCellList = new List<Route>();
        openCellList.Add(new Route(startCell));
        visitedCells.Add(startCell);
        while(true)
        {
            Route minRoute = getBestRoute(openCellList, endCells);
            if(minRoute == null)
            {
                return null;
            }
            if (endCells.Contains(minRoute.getLastCell()))
            {
                pathToEnd = minRoute;
                break;
            }
            visitedCells.Add(minRoute.getLastCell());
            List<Route> nextRoutes = minRoute.makeNeighbourRoutes();
            foreach (Route r in nextRoutes)
            {
                if(!visitedCells.Contains(r.getLastCell()) && !r.getLastCell().isWall())
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
        for(int i = 1; i<routes.Count;i++)
        {
            if(routes[i].getF(endCells) < routes[min].getF(endCells))
            {
                min = i;
            }
        }
        return routes[min];
    }
}

public class Line
{
    private double a, b;
    private bool intersectedByWall = false;

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

public class Cell
{

    private int columnPosition, rowPosition;
    private int score, distance;
    private Map Map;
    private List<Cell> visibleCells;
    private bool wall;

    public Cell(Map map, int column, int row, bool wall)
    {
        this.Map = map;
        this.columnPosition = column;
        this.rowPosition = row;
        this.wall = wall;
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


    public bool isWall()
    {
        return wall;
    }

    public List<Cell> getNotWallNeighbours()
    {
        List<Cell> notWallNeughbours = new List<Cell>();
        int[] neighbours = { 0, -1, -1, 0, 1, 0, 0, 1 };
        for (int i = 0; i < neighbours.Length; i += 2)
        {
            Cell c = Map.getCell(columnPosition + neighbours[i], rowPosition + neighbours[i+1]);
            if (c != null && !c.isWall())
                notWallNeughbours.Add(c);
        }
        return notWallNeughbours;
    }

    public bool isVisible(Cell c)
    {
        float[] shifts = { 0.5f, 0.5f, 0.4f, 0.4f, 0.6f, 0.6f, 0.4f, 0.6f, 0.6f, 0.4f };

        if (c.isWall())
            return false;

        for (int i = 0; i < shifts.Length / 2; i++)
        {
            for (int j = 0; j < shifts.Length / 2; j++)
            {
                Line l = new Line(c.getColumn() + shifts[j*2], c.getRow() + shifts[j * 2 + 1], columnPosition + shifts[i * 2], rowPosition + shifts[i * 2 + 1]);
                if (isCellVisibleByLine(l, c))
                    return true;
            }
        }
        return false;
    }

    private bool isCellVisibleByLine(Line line, Cell cell)
    {
        Cell minCell = Map.getCell(min(getColumn(), cell.getColumn()), min(getRow(), cell.getRow()));
        Cell maxCell = Map.getCell(max(getColumn(), cell.getColumn()) + 1, max(getRow(), cell.getRow()) + 1);


        if (maxCell == null)
            maxCell = Map.getCell(max(getColumn(), cell.getColumn()), max(getRow(), cell.getRow())); // TODO: VALIDATE

        for (int i = minCell.getColumn(); i < maxCell.getColumn(); i++)
        {
            for (int j = minCell.getRow(); j < maxCell.getRow(); j++)
            {
                if (Map.getCell(i, j).isWall())
                {
                    line.checkIntersect(i, j);
                }
            }
        }
        if (line.isIntersected())
            return false;
        return true;
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

public class Map
{
    public static int NO_SCORE = -1;

    private int sizeColumn, sizeRow, numberOfFields = 0;
    private Cell[,] cells;

    public Map(int[,] map)
    {
        sizeRow = map.GetLength(0);
        sizeColumn = map.GetLength(1);
        cells = new Cell[sizeColumn, sizeRow];
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                cells[i, j] = new Cell(this, i, j, map[j, i] == MapLoader.WALL);
                if (map[i, j] != MapLoader.WALL)
                    numberOfFields++;
            }
        }
        initCells();
    }

    private void initCells()
    {
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                setVisibleCells(cells[i,j]);
            }
        }
    }

    private void resetCells()
    {
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                if (!cells[i, j].isWall())
                {
                    cells[i, j].resetCell();
                }
            }
        }
    }

    private void setVisibleCells(Cell startCell)
    {
        List<Cell> visibleCells = new List<Cell>();
        for (int i = 0; i < sizeColumn; i++)
        {
            for (int j = 0; j < sizeRow; j++)
            {
                if (startCell.isVisible(cells[i, j]))
                    visibleCells.Add(cells[i, j]);
            }
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
                if(cells[i,j].getScore() == score)
                {
                    if(neighbourScore == -1)
                    {
                        goodScoreCells.Add(cells[i, j]);
                        continue;
                    }
                    List<Cell> neighbours = cells[i, j].getNotWallNeighbours();
                    foreach(Cell c in neighbours)
                    {
                        if(c.getScore() == neighbourScore)
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
                if(cells[i,j].getScore() > bestScore)
                {
                    bestScore = cells[i, j].getScore();
                    bestCells.Clear();
                    bestCells.Add(cells[i, j]);
                }
                else if(cells[i, j].getScore() == bestScore)
                {
                    bestCells.Add(cells[i, j]);
                }
            }
        }
        return bestCells;
    }

    public int[,] getIntArray()
    {
        int[,] map = new int[sizeColumn, sizeRow];
        for(int i = 0;i<sizeColumn;i++)
        {
            for(int j = 0;j<sizeRow;j++)
            {
                map[i, j] = cells[i, j].isWall() ? MapLoader.WALL : MapLoader.FIELD;
            }
        }
        return map;
    }

    public Cell getCell(int columnPos, int rowPos)
    {
        if (columnPos < 0 || rowPos < 0 || columnPos >= sizeColumn || rowPos >= sizeRow)
            return null;
        return cells[columnPos, rowPos];
    }
}