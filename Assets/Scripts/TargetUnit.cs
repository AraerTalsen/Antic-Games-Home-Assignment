using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnit : MonoBehaviour
{
    private static Dictionary<Transform, (int, int)> registeredUnits = new();

    private Unit self;

    public Transform Target { get => FindTarget();}

    private void Start()
    {
        self = GetComponent<UnitController>().AssignedRole;
    }
    
    public void AddUnit(int i, int j, Transform unit)
    {
        registeredUnits.Add(unit, (i, j));
    }

    public void UpdateUnit(int i, int j, Transform unit)
    {
        registeredUnits[unit] = (i, j);
    }

    public void DeleteUnit(Transform unit)
    {
        registeredUnits.Remove(unit);
    }

    private Transform FindTarget()
    {        
        Transform selectedTarget = null;
        int highestPriority = -1;
        
        foreach(KeyValuePair<Transform, (int, int)> cell in registeredUnits)
        {
            Unit u = cell.Key.gameObject.GetComponent<UnitBody>().AssignedRole;
            if(u.UnitTag.CompareTo(self.FlaggedUnits) == 0)
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

    private Transform FindTarget(List<Transform> targets)
    {
        Transform selectedTarget = null;
        int highestPriority = -1;

        foreach(Transform t in targets)
        {
            Unit u = t.gameObject.GetComponent<UnitBody>().AssignedRole;
            (int, int) position = registeredUnits[t];
            if(u.UnitTag.CompareTo(self.FlaggedUnits) == 0)
            {
                int unitPriority = MeasurePriorityLevel(position);
                if(unitPriority > highestPriority)
                {
                    highestPriority = unitPriority;
                    selectedTarget = t;
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
        (int x, int z) = registeredUnits[self.Transform];
        return Mathf.Abs(destination.i - x) + Mathf.Abs(destination.j - z);
    }

    //Find way to make this private to avoid null assignment errors
    public int DistanceFrom(Transform target)
    {
        (int i, int j) = registeredUnits[target];
        (int x, int z) = registeredUnits[self.Transform];
        return Mathf.Abs(i - x) + Mathf.Abs(j - z);
    }

    public int DistanceBetween((int x, int z) pos1, (int x, int z) pos2)
    {
        return Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.z - pos2.z);
    }

    public Transform GetNewTarget(List<Transform> attackers = null)
    {
        if(attackers == null || attackers.Count == 0)
        {
            return Target;
        }
        else
        {
            return FindTarget(attackers);
        }
    }
}
