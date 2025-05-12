using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetContainer : MonoBehaviour
{
    private Unit target;
    private List<Unit> attackers;
    private int distance;

    public Unit Target {get => target; set => target = value;}
    public int Distance {get => distance; set => distance = value;}
    public List<Unit> Attackers {get => attackers;}

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
