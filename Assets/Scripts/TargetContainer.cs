using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetContainer
{
    private Unit target;
    private List<Unit> attackers = new();
    private int distance = 100;
    private (int x, int y) gridPos;

    public Unit Target {get => target; set => target = value;}
    public int Distance {get => distance; set => distance = value;}
    public List<Unit> Attackers {get => attackers;}
    public (int, int) GridPos {get => gridPos; set => gridPos = value;}

    public void AddAttacker(Unit unit)
    {
        if(!attackers.Contains(unit))
        {
            attackers.Add(unit);
        }
    }

    public void RemoveAttacker(Unit unit)
    {
        if(!attackers.Contains(unit))
        {
            attackers.Remove(unit);
        }
    }
}
