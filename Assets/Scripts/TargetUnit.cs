using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnit : MonoBehaviour
{
    private static Dictionary<Unit, (int, int)> registeredUnits = new();

    private Unit self;

    public Unit Target { get => FindTarget();}

    private void Start()
    {
        self = GetComponent<UnitController>().AssignedRole;
    }
    
    public void AddUnit(int i, int j, Unit unit)
    {
        registeredUnits.Add(unit, (i, j));
    }

    public void UpdateUnit(int i, int j, Unit unit)
    {
        registeredUnits[unit] = (i, j);
    }

    public void DeleteUnit(Unit unit)
    {
        registeredUnits.Remove(unit);
    }

    private Unit FindTarget()
    {
        Unit selectedTarget = null;
        int highestPriority = -1;
        
        foreach(KeyValuePair<Unit, (int, int)> cell in registeredUnits)
        {
            if(cell.Key.UnitTag.CompareTo(self.FlaggedUnits) == 0)
            {
                int unitPriority = MeasurePriorityLevel(cell.Value);
                if(unitPriority > highestPriority)
                {
                    highestPriority = unitPriority;
                    selectedTarget = cell.Key;
                } 
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
        (int x, int z) = registeredUnits[self];
        return Mathf.Abs(destination.i - x) + Mathf.Abs(destination.j - z);
    }

    //Find way to make this private to avoid null assignment errors
    public int DistanceFrom(Unit target)
    {
        (int i, int j) = registeredUnits[target];
        (int x, int z) = registeredUnits[self];
        return Mathf.Abs(i - x) + Mathf.Abs(j - z);
    }

    public int DistanceBetween((int x, int z) pos1, (int x, int z) pos2)
    {
        return Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.z - pos2.z);
    }
}
