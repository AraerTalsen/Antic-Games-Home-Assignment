using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Responsible for unit movement and grid updates
public class UnitController : Grid<List<Unit>>
{
    private TargetUnit tu;
    private Tween moveTween;
    private Vector3 lastTargetPos = new Vector3(100, 0, 100);

    public Unit AssignedRole { get; set; }
    public float Speed { get; set; }
    public TargetContainer TC { get; set; }

    private void Start()
    {
        tu = GetComponent<TargetUnit>();
        tu.UC = this;
        tu.TC = TC;
        tu.AssignedRole = AssignedRole;
        tu.SightRadius = AssignedRole.SightRadius;
        RegisterUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (AssignedRole.IsMobile)
        {
            if (TC.Distance > TC.Reach)
            {
                MoveUnit();
            }
            else
            {
                StopUnit();
            }
        }

        if (GameManager.endGame)
        {
            UnregisterUnit();
        }
    }

    private void StopUnit()
    {
        if (moveTween != null && moveTween.IsPlaying())
        {
            moveTween.Kill();
        }
    }

    protected override void RegisterUnit()
    {
        (int i, int j) position = WorldToGridPos(transform.position);
        List<Unit> units = GetCell(position.i, position.j);
        AddUnitToCell(position, units);
        TC.GridPos = (position.i, position.j);
    }

    public override void UnregisterUnit()
    {
        (int i, int j) position = AssignedRole.TargetContainer.GridPos;
        List<Unit> units = GetCell(position.i, position.j);
        RemoveUnitFromCell(position, units);
        moveTween.Kill();
        GameManager.removeUnit(gameObject);
        Destroy(AssignedRole);
        Destroy(gameObject);
    }

    private void AddUnitToCell((int i, int j) position, List<Unit> units)
    {
        if (units == null)
        {
            units = new List<Unit> { AssignedRole };
            AddCell(position.i, position.j, units);
        }
        else
        {
            units.Add(AssignedRole);
        }
    }

    private void RemoveUnitFromCell((int i, int j) position, List<Unit> units)
    {

        if (units != null && units.Count > 0 && units.Contains(AssignedRole))
        {
            units.Remove(AssignedRole);
            if (units.Count == 0)
            {
                DeleteCell(position.i, position.j);
            }
        }
        else
        {
            Debug.Log($"Unit {AssignedRole} is either stored to the wrong position or does not exist");
        }
    }

    public bool CheckIfCellHasUnit((int i, int j) position, Unit target)
    {
        List<Unit> units = GetCell(position.i, position.j);
        return units != null && units.Contains(target);
    }

    protected override void Move()
    {
        (int i, int j) position = AssignedRole.TargetContainer.GridPos;
        List<Unit> units = GetCell(position.i, position.j);
        RemoveUnitFromCell(position, units);

        position = WorldToGridPos(transform.position);
        units = GetCell(position.i, position.j);
        AddUnitToCell(position, units);

        TC.GridPos = (position.i, position.j);
    }

    private (int i, int j) WorldToGridPos(Vector3 position)
    {
        int i = (int)position.x, j = (int)position.z;
        return (i, j);
    }

    private void MoveUnit()
    {
        if (TC.Target != null)
        {
            CheckIfTargetMoved();
            Move();
        }
        else
        {
            StopUnit();
        }
    }

    protected virtual void CheckIfTargetMoved()
    {
        if (Vector3.Distance(lastTargetPos, TC.Target.Transform.position) >= 1 || (TC.Distance > TC.Reach && !moveTween.IsPlaying()))
        {
            lastTargetPos = TC.Target.Transform.position;
            moveTween?.Kill();
            StartMoving();
        }
    }

    private void StartMoving()
    {
        if (AngleFromFaceToTarget() < 1)
        {
            AnimateMovement();
        }
        else
        {
            AnimateRotate();
            AnimateMovement();
        }
    }

    private void AnimateMovement()
    {
        moveTween = transform.DOMove(TC.Target.Transform.position, Speed).SetSpeedBased(true).SetEase(Ease.Linear);
    }

    private void AnimateRotate()
    {
        transform.DOLookAt(TC.Target.Transform.position, 0.5f);
    }

    private float AngleFromFaceToTarget()
    {
        Vector3 directionToTarget = (TC.Target.Transform.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, directionToTarget);
    }
}