using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GridManager
{
    private static Dictionary<UnitController, (int, int)> registedUnits = new();

    public static void AddUnit(UnitController unit, (int, int) position)
    {
        registedUnits.Add(unit, position);
    }
}
