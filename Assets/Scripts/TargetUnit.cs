using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnit : MonoBehaviour
{
    private static Dictionary<Unit, (int, int)> registedUnits = new();

    private Unit self;

    public Unit Target { get => FindTarget();}

    private void Start()
    {
        self = GetComponent<UnitController>().RoleAssignment;
    }
    
    public void AddUnit(int i, int j, Unit unit)
    {
        registedUnits.Add(unit, (i, j));
    }

    private Unit FindTarget()
    {
        Unit selectedTarget = null;
        int highestPriority = 0;
        
        foreach(KeyValuePair<Unit, (int, int)> cell in registedUnits)
        {
            int unitPriority = MeasurePriorityLevel(cell.Value);
            if(unitPriority > highestPriority)
            {
                highestPriority = unitPriority;
                selectedTarget = cell.Key;
            } 
        }

        return selectedTarget;
        
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

    public int DistanceFrom(Unit target)
    {
        (int i, int j) = registedUnits[target];
        (int x, int z) = registedUnits[self];
        return Mathf.Abs(i - x) + Mathf.Abs(j - z);
    }
}
