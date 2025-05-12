using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetUnit : MonoBehaviour
{
    private Unit self;
    private TargetContainer tc;
    private UnitController uc;
    private int sightRadius;

    public TargetContainer TC {set => tc = value;}
    public Unit Self {set => self = value;}
    public UnitController UC {set => uc = value;}
    public int SightRadius {set => sightRadius = value;}

    private void Start()
    {
        self = GetComponent<UnitController>().AssignedRole;
    }

    private void Update()
    {
        if(tc.Target == null)
        {
            //tc.Target = GetNewTarget(tc.Attackers);
        }
    }

    public Unit FindNewTarget()
    {
        Vector3 currentPos = transform.position;
        Unit selectedUnit = null;
        
        BFSFromPosition(currentPos, ref selectedUnit);

        return selectedUnit;
    }

    private Unit BFSFromPosition(Vector3 currentPos, ref Unit selectedUnit)
    {
        int topPriority = -1;
        Vector3[] dirs = {Vector3.forward, Vector3.right, Vector3.back, Vector3.left};

        List<Vector3> cells = new() {currentPos};
        List<Vector3> allCells = cells.ToList();

        int i;

        do
        {
            List<Vector3> tempCells = new();

            for(i = 0; i < cells.Count; i++)
            {
                for(int j = 0; j < dirs.Length; j++)
                {
                    Vector3 cell = cells[i];
                    cell += dirs[j];

                    (Unit unit, int priority)? cellContent = CheckCellForUnit(cell, ref allCells, ref tempCells);

                    if(cellContent != null && topPriority < cellContent.Value.priority)
                    {
                        selectedUnit = cellContent.Value.unit;
                        topPriority = cellContent.Value.priority;
                    }
                }
            }
            cells = tempCells;
            allCells = cells.ToList();
        }
        while(Vector3.Distance(currentPos, cells[i]) < sightRadius);

        return selectedUnit;
    }

    private (Unit, int)? CheckCellForUnit(Vector3 lookAtCell, ref List<Vector3> allCells, ref List<Vector3> tempCells)
    {
        (Unit, int)? cellContent = null;

        if(ContainsVector(lookAtCell, allCells))
        {
            tempCells.Add(lookAtCell);
            allCells.Add(lookAtCell);

            cellContent = CheckIfUnit(lookAtCell);
        }

        return cellContent;
    }

    private (Unit, int) CheckIfUnit(Vector3 cell)
    {
        int priority = -1;
        int x = (int)cell.x, z = (int)cell.z;

        Unit u = uc.GetCell(x, z);

        bool isNull = u == null;
        bool isFlagged = !isNull && u.UnitTag.CompareTo(self.FlaggedUnits) == 0;

        if(isFlagged)
        {
            priority = MeasurePriorityLevel((x, z));
        }
        
        return (u, priority);
    }

    private bool ContainsVector(Vector3 v, List<Vector3> items)
    {
        foreach(Vector3 itm in items)
        {
            if(Vector3.Distance(itm, v) < 1)
            {
                return true;
            }
        }
        return false;
    }

    private int MeasurePriorityLevel((int, int) position)
    {
        int distance = 3 - (int)Mathf.Ceil(Mathf.Clamp(DistanceFrom(position), 0, 9) / 3);
        return distance;
    }

    private int DistanceFrom((int i, int j) destination)
    {
        Vector3 currentPos = transform.position;
        int x = (int)currentPos.x, z = (int)currentPos.z;
        return Mathf.Abs(destination.i - x) + Mathf.Abs(destination.j - z);
    }

    public int DistanceBetween((int x, int z) pos1, (int x, int z) pos2)
    {
        return Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.z - pos2.z);
    }
}
