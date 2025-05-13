using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class UnitController : Grid<List<Unit>>
{
    private int speed;
    private Unit assignedRole;
    private TargetUnit tu;
    private Tween moveTween;
    private TargetContainer tc;
    private int lastDistance = -1;

    public Unit AssignedRole {get => assignedRole; set => assignedRole = value;}
    public int Speed {set => speed = value;}
    public TargetContainer TC {set => tc = value;}

    private void Start()
    {
        tu = GetComponent<TargetUnit>();
        tu.UC = this;
        tu.TC = tc;
        tu.SightRadius = assignedRole.SightRadius;
        RegisterUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if(assignedRole.IsMobile)
        {
            if(tc.Distance > 1)
            {
                MoveUnit();
            }
            else
            {
                if(moveTween.IsPlaying())
                {
                    moveTween.Kill();
                }
            }
        }
    }

    protected override void RegisterUnit()
    {
        (int i, int j) position = WorldToGridPos(transform.position);
        List<Unit> units = GetCell(position.i, position.j);
        AddUnitToCell(position, units);
        tc.GridPos = (position.i, position.j);
    }

    public override void UnregisterUnit()
    {
        (int i, int j) position = assignedRole.TargetContainer.GridPos;
        List<Unit> units = GetCell(position.i, position.j);
        RemoveUnitFromCell(position, units);
        moveTween.Kill();
        Destroy(assignedRole);
        Destroy(gameObject);
    }

    private void AddUnitToCell((int i, int j) position, List<Unit> units)
    {
        if(units == null)
        {
            units = new List<Unit> {assignedRole};
            AddCell(position.i, position.j, units);
        }
        else
        {
            units.Add(assignedRole);
            //print($"Role now includes assignedRole: {GetCell(position.i, position.j).Contains(assignedRole)}");
        }
    }
    
    private void RemoveUnitFromCell((int i, int j) position, List<Unit> units)
    {
       
        if(units != null && units.Count > 0 && units.Contains(assignedRole))
        {
            units.Remove(assignedRole);
            if(units.Count == 0)
            {
                DeleteCell(position.i, position.j);
            }
        }
        else
        {
            Debug.Log($"Unit {assignedRole} is either stored to the wrong position or does not exist");
        }
    }

    protected override void Move()
    {
        (int i, int j) position = assignedRole.TargetContainer.GridPos;
        List<Unit> units = GetCell(position.i, position.j);
        RemoveUnitFromCell(position, units);

        position = WorldToGridPos(transform.position);
        units = GetCell(position.i, position.j);
        AddUnitToCell(position, units);

        tc.GridPos = (position.i, position.j);
    }

    private (int i, int j) WorldToGridPos(Vector3 position)
    {
        int i = (int)position.x, j = (int)position.z;
        return (i, j);
    }

    private void MoveUnit()
    {
        if(tc.Target != null)
        {
            CheckForMoveUpdate();
            Move();
        }
        else if(moveTween != null && moveTween.IsPlaying())
        {
            moveTween.Kill();
        }
    }

    protected virtual void CheckForMoveUpdate()
    {
        if(lastDistance < tc.Distance)
        { 
            moveTween?.Kill();
            StartMoving();
        }
    }

    private void StartMoving()
    {
        moveTween = transform.DOMove(tc.Target.Transform.position, speed).SetSpeedBased(true).SetEase(Ease.Linear);
    }
}