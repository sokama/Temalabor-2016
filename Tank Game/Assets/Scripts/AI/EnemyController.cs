using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Classes.Weapons;

public class EnemyController : MonoBehaviour
{

    public float MinimumWaitBetweenShoots = 0.5f;
    private float waitBeforeShoot = 0f;

    public int DestroyingWallCost = 1;

    public Transform player;


    private MovingStrategy strategy;
    private static int enemyHealth = 100;
    private static bool healthChanged = false;
    public static bool canshoot = false;

    public static Map Map;

    void Start()
    {
        Wall.WallCost = DestroyingWallCost;
    }

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
            rotateTower(player.position);
            if (GetComponent<WeaponHolder>().PrimaryWeaponCanShoot() && waitBeforeShoot <= 0f)
            {
                GetComponent<WeaponHolder>().FirePrimaryWeapon();
                waitBeforeShoot = MinimumWaitBetweenShoots;
            }
        }

        if (waitBeforeShoot > 0f)
        {
            waitBeforeShoot -= Time.deltaTime;
            if (waitBeforeShoot < 0f)
                waitBeforeShoot = 0f;
        }

        canshoot = (GetComponent<WeaponHolder>().PrimaryWeaponCanShoot() || GetComponent<WeaponHolder>().SecondaryWeaponCanShoot())
            && waitBeforeShoot <= 0f;
    }

    private void makeStrategy()
    {
        if (enemyHealth < 25)
        {
            strategy = new CriticalHealthStrategy(this);
        }
        else if (enemyHealth < 50)
        {
            strategy = new HidingStrategy(this);
        }
        else if (enemyHealth < 75)
        {
            strategy = new AttackingFromFarStrategy(this);
        }
        else
        {
            strategy = new FollowingStrategy(this);
        }
    }

    public bool canShootWallBreaker()
    {
        return GetComponent<WeaponHolder>().SecondaryWeaponCanShoot();
    }

    public static void notifyHealth(int health)
    {
        if (enemyHealth != health)
        {
            enemyHealth = health;
            healthChanged = true;
        }
    }

    public void destroyWall(int column, int row)
    {
        Vector2 target = MapLoader.MapCoordsToWorldCoords(column, row);
        rotateTower(new Vector3(target.x, player.position.y, target.y));
        if (GetComponent<WeaponHolder>().SecondaryWeaponCanShoot() && waitBeforeShoot <= 0f)
        {
            GetComponent<WeaponHolder>().FireSecondaryWeapon();
            waitBeforeShoot = MinimumWaitBetweenShoots;
        }
    }

    public void moveEnemy(int nextColumn, int nextRow)
    {
        Vector3 currentPosition = transform.position;
        Vector2 nextPositionXY = MapLoader.MapCoordsToWorldCoordsFloat(new Vector2(nextColumn + 0.5f, nextRow + 0.5f));
        Vector3 nextPosition = new Vector3(nextPositionXY.x, currentPosition.y, nextPositionXY.y);
        Vector3 distanceVector = nextPosition - currentPosition;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, currentPosition + transform.forward, GetComponent<TankMovementParameters>().TankMovementSpeed * Time.deltaTime);
        if (!vectorEquals(newPosition, transform.position))
        {
            transform.position = newPosition;
            transform.forward = Vector3.RotateTowards(transform.forward, distanceVector, GetComponent<TankMovementParameters>().TankRotationSpeed * Time.deltaTime, 0.0f);
        }
    }

    private void rotateTower(Vector3 targetPosition)
    {
        Transform tower = transform.FindChild("Graphics").FindChild("Tower");
        tower.rotation = Quaternion.LookRotation(targetPosition - transform.position);
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
    private EnemyController enemyControl;
    private bool couldshoot = true;

    protected Cell enemyPosition;
    protected Cell playerPosition;

    private Route targetRoute;

    private float waitTime = -1f;

    protected Map Map = EnemyController.Map;

    public MovingStrategy(EnemyController ec)
    {
        this.player = ec.player;
        this.enemy = ec.transform;
        enemyControl = ec;
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

        LineWithPoint l = new LineWithPoint(realPlayerPosition.x, realPlayerPosition.y, realEnemyPosition.x, realEnemyPosition.y);

        Cell minCell = Map.getCell(Cell.min(playerPosition.getColumn(), enemyPosition.getColumn()), Cell.min(playerPosition.getRow(), enemyPosition.getRow()));
        Cell maxCell = Map.getCell(Cell.max(playerPosition.getColumn(), enemyPosition.getColumn()) + 1, Cell.max(playerPosition.getRow(), enemyPosition.getRow()) + 1);

        for (int i = minCell.getColumn(); i < maxCell.getColumn(); i++)
        {
            for (int j = minCell.getRow(); j < maxCell.getRow(); j++)
            {
                if (Map.getCell(i, j).isBusy())
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

        if (targetRoute == null || couldshoot != EnemyController.canshoot)
        {
            PathFinder pf = new PathFinder();
            targetRoute = pf.searchRoute(enemyPosition, getEndCells(), enemyControl.canShootWallBreaker());
        }

        if (Time.time < waitTime)
        {
            return;
        }

        if (targetRoute != null)
        {
            Cell nextCell = targetRoute.getNextCell(enemyPosition);
            if (nextCell == null)
            {
                targetRoute = null;
            }
            else if (!isEnemyInPosition(nextCell))
            {
                if (nextCell.isBusy())
                {
                    enemyControl.destroyWall(nextCell.getColumn(), nextCell.getRow());
                    wait(2f);
                }
                else
                {
                    enemyControl.moveEnemy(nextCell.getColumn(), nextCell.getRow());
                }
            }
        }
        couldshoot = EnemyController.canshoot;
    }

    private void wait(float wait)
    {
        waitTime = Time.time + wait;
    }

    public abstract List<Cell> getEndCells();

}

public class FollowingStrategy : MovingStrategy
{
    public static int DistanceFromTarget = 2;
    public FollowingStrategy(EnemyController ec) : base(ec)
    {

    }

    public override List<Cell> getEndCells()
    {
        List<Cell> targetCells = Map.getGoodScoreCells(0, -1);
        for (int i = 0; i < targetCells.Count; i++)
        {
            if (targetCells[i].getDistance() != DistanceFromTarget)
            {
                targetCells.RemoveAt(i--);
            }
        }
        return targetCells;
    }
}

public class AttackingFromFarStrategy : MovingStrategy
{
    public AttackingFromFarStrategy(EnemyController ec) : base(ec)
    {

    }

    public override List<Cell> getEndCells()
    {
        if (EnemyController.canshoot)
            return Map.getGoodScoreCells(0, 1);
        else
            return Map.getGoodScoreCells(1, 0);
    }
}

public class CriticalHealthStrategy : MovingStrategy
{
    public CriticalHealthStrategy(EnemyController ec) : base(ec)
    {

    }

    public override List<Cell> getEndCells()
    {
        List<Cell> cellsSeenByPlayer = Map.getGoodScoreCells(0, -1);
        if (cellsSeenByPlayer.Contains(enemyPosition))
            return Map.getGoodScoreCells(1, -1);
        return Map.getBestScoreCells();
    }
}

public class HidingStrategy : MovingStrategy
{
    public HidingStrategy(EnemyController ec) : base(ec)
    {

    }

    public override List<Cell> getEndCells()
    {
        return Map.getBestScoreCells();
    }
}


