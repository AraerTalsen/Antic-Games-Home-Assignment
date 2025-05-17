using System.Collections.Generic;
using UnityEngine;

//Search pattern for units to find their next target to move to if they haven't had a specific target injected by a command (e.g. mouse click)
public class TargetUnit : MonoBehaviour
{
    private static Vector2Int[] dirs = { new(0, 1), new(1, 0), new(0, -1), new(-1, 0) };
    private List<Vector2Int> cells = new();
    private List<Vector2Int> tempCells = new();
    private HashSet<Vector2Int> allCells = new();

    public TargetContainer TC { get; set; }
    public Unit AssignedRole { get; set; }
    public UnitController UC { get; set; }
    public int SightRadius { get; set; }
    public int SpriteRadius { get; set; }

    private void Start()
    {
        InvokeRepeating("CheckForRePriority", 3, 3);
    }

    private void Update()
    {
        TargetOperations();
    }

    private void TargetOperations()
    {
        CheckIfTargetsExist();

        if (TC.Target == null)
        {
            ChooseTarget();
        }

        if (TC.Target != null)
        {
            TC.Distance = DistanceFrom(TC.Target.Transform.position);
        }
    }

    private void CheckForRePriority()
    {
        if (TC.Attackers.Count == 0)
        {
            if (TC.IsAutoMode)
            {
                FindNewTarget(TC.Target);
            }
        }
        else
        {
            TC.StoreTargetForLater(ChooseAttacker());
        }
    }

    private void ChooseTarget()
    {
        if (!TC.IgnoreAttackers && TC.Attackers.Count > 0)
        {
            TC.StoreTargetForLater(ChooseAttacker());
        }
        else if (TC.InjectedTarget != null)
        {
            TC.RestoreTarget();
        }
        else if (TC.IsAutoMode)
        {
            Unit u = TC.StoredTarget;
            TC.Target = FindNewTarget(u);
        }

        if (TC.Target != null)
        {
            TC.Reach = SpriteRadius + TC.Target.SpriteRadius;
        }
    }

    private void CheckIfTargetsExist()
    {
        if (TC.Target != null && !UC.CheckIfCellHasUnit(TC.Target.TargetContainer.GridPos, TC.Target))
        {
            TC.ResetTarget();
        }

        if (TC.InjectedTarget != null && !UC.CheckIfCellHasUnit(TC.InjectedTarget.TargetContainer.GridPos, TC.InjectedTarget))
        {
            TC.RestoreTarget();
        }

        CheckIfAttackersExist();
    }

    private void CheckIfAttackersExist()
    {
        List<Unit> attackers = TC.Attackers;
        List<Unit> removeAttackers = new();
        if (attackers.Count > 0)
        {
            foreach (Unit u in attackers)
            {
                if (!UC.CheckIfCellHasUnit(u.TargetContainer.GridPos, u) || DistanceFrom(u.Transform.position) > (TC.Reach + u.SpriteRadius))
                {
                    removeAttackers.Add(u);
                }
            }

            for (int i = 0; i < removeAttackers.Count; i++)
            {
                attackers.Remove(removeAttackers[i]);
            }
        }
    }

    private Unit ChooseAttacker()
    {
        (Unit unit, _) = PriorityUnitOfList(TC.Attackers, 0, true);
        return unit;
    }

    private Unit FindNewTarget(Unit selectedUnit = null)
    {
        Vector3 currentPos = transform.position;
        cells.Clear();
        allCells.Clear();
        tempCells.Clear();

        BFSFromPosition(currentPos, ref selectedUnit);

        return selectedUnit;
    }

    private Unit BFSFromPosition(Vector3 currentPos, ref Unit selectedUnit)
    {
        int topPriority = selectedUnit != null ?
            PriorityAssessment.MeasurePriorityLevel(AssignedRole.PriorityID, ((currentPos, selectedUnit.Transform.position), selectedUnit)) : -1;
        int minPriorityThreshold = topPriority == -1 ? 2 : topPriority + 1;
        if (AssignedRole.UnitTag.CompareTo("Flag") == 0 && selectedUnit != null)
        {
            selectedUnit.FlagRiskLevel = topPriority;
        }

        Vector2Int currentCoords = new((int)currentPos.x, (int)currentPos.z);
        cells.Add(currentCoords);
        tempCells.Add(currentCoords);


        int searchDepth = 0;
        do
        {
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < dirs.Length; j++)
                {
                    Vector2Int cell = cells[i] + dirs[j];

                    (Unit unit, int priority)? cellContent = CheckCellForUnit(cell, searchDepth);

                    if (cellContent != null && topPriority < cellContent.Value.priority)
                    {
                        selectedUnit = cellContent.Value.unit;
                        topPriority = cellContent.Value.priority;
                        if (topPriority > minPriorityThreshold)
                        {
                            return selectedUnit;
                        }
                    }
                }
            }
            searchDepth++;

            var oldCells = cells;
            cells = new List<Vector2Int>(tempCells);
            tempCells = oldCells;
            tempCells.Clear();
        }
        while (searchDepth < SightRadius && SightRadius != 0);

        return selectedUnit;
    }

    private (Unit, int)? CheckCellForUnit(Vector2Int lookAtCell, int searchDepth)
    {
        (Unit, int)? cellContent = null;
        if (allCells.Add(lookAtCell))
        {
            tempCells.Add(lookAtCell);

            cellContent = PriorityUnitOfList(UC.GetCell(lookAtCell.x, lookAtCell.y), searchDepth);
        }

        return cellContent;
    }

    private (Unit, int) PriorityUnitOfList(List<Unit> units, int searchDepth, bool skipFlags = false)
    {
        int topPriority = -1;
        Unit selectedUnit = null;

        if (units != null)
        {
            foreach (Unit u in units)
            {
                bool isNull = u == null;
                bool isFlagged = skipFlags || (!isNull && u.UnitTag.CompareTo(AssignedRole.FlaggedUnits) == 0);

                if (isFlagged)
                {
                    int priority = PriorityAssessment.MeasurePriorityLevel(AssignedRole.PriorityID, (searchDepth, u));
                    if (AssignedRole.UnitTag.CompareTo("Flag") == 0)
                    {
                        u.FlagRiskLevel = priority;
                    }
                    if (topPriority < priority)
                    {
                        topPriority = priority;
                        selectedUnit = u;
                    }
                }
            }
        }

        return (selectedUnit, topPriority);
    }

    private float DistanceFrom(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination);
    }
}
