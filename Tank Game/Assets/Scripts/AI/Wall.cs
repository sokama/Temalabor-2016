using UnityEngine;
using System.Collections;

public class Wall : Cell
{
    public static int WallCost = 4;

    bool destructible;
    bool destructed = false;

    public Wall(Map map, int column, int row, bool destructible) : base(map, column, row)
    {
        this.destructible = destructible;
    }

    public void destruct()
    {
        if (destructible)
            destructed = true;
    }

    public override bool isBusy()
    {
        return !destructed;
    }

    public override int getCost()
    {
        if (!destructible)
            return 5000;
        if (destructed)
            return 1;
        else
            return WallCost;
    }
}