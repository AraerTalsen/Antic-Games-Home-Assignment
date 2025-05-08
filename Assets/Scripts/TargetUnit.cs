using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnit : MonoBehaviour
{
    private static Dictionary<UnitController, (int, int)> registedUnits = new();

    private UnitController self;

    private void Start()
    {
        self = GetComponent<UnitController>();
    }
    
    public void AddUnit(int i, int j, UnitController unit)
    {
        registedUnits.Add(unit, (i, j));
    }

    public void FindTarget()
    {
        UnitController selectedTarget;
        int highestPriority = 0;

        foreach(KeyValuePair<UnitController, (int, int)> cell in registedUnits)
        {
            int unitPriority = MeasurePriorityLevel(cell.Value);
            if(unitPriority > highestPriority)
            {
                highestPriority = unitPriority;
                selectedTarget = cell.Key;
            } 
        }
    }

    private int MeasurePriorityLevel((int, int) position)
    {
        int distance = 3 - (int)Mathf.Ceil(Mathf.Clamp(DistanceFrom(position), 0, 9) / 3);
        return distance;
    }

    private int DistanceFrom((int i, int j) destination)
    {
        (int x, int z) = registedUnits[self];
        return Mathf.Abs(destination.i - x) + Mathf.Abs(destination.j - z);
    }
}
