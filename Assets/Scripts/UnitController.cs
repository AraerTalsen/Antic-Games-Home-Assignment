using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class UnitController : Grid<Unit>
{
    private int speed;
    private Unit assignedRole;
    private TargetUnit tu;
    private (int x, int z) lastGridPos = (100, 100);
    private Tween moveTween;
    private TargetContainer tc;

    public Unit AssignedRole {get => assignedRole; set => assignedRole = value;}
    public int Speed {set => speed = value;}
    public TargetContainer TC {set => tc = value;}
    
    void Awake()
    {
        tu = GetComponent<TargetUnit>();
        tu.UC = this;
    }

    private void Start()
    {
        RegisterUnit();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUnitGridPos();
        if(assignedRole.IsMobile)
        {
            if(tc.Distance > 1)
            {
                Move();
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
        Vector3 currentPos = transform.position;
        int i = (int)currentPos.x, j = (int)currentPos.z;
        AddCell(i, j, assignedRole);
        assignedRole.GridPos = (i, j);
    }

    public override void UnregisterUnit()
    {
        DeleteCell(assignedRole);
        moveTween.Kill();
        Destroy(assignedRole);
        Destroy(gameObject);
    }

    protected override void Move()
    {
        if(tc.Target != null)
        {
            //CheckForMoveUpdate();
        }
        else if(moveTween.IsPlaying())
        {
            moveTween.Kill();
        }
    }

    /*protected virtual void CheckForMoveUpdate()
    {
        int dist = tu.DistanceBetween(lastGridPos, GetCell(tc.Target));
        if(dist > 0)
        { 
            if(moveTween != null)
            {
                moveTween?.Kill();
            }
            lastGridPos = GetCell(tc.Target);
            StartMoving();
        }
    }*/

    /*protected virtual void UpdateUnitGridPos()
    {
        (int i, int j) = GetCell(assignedRole);
        Vector3 current = transform.position;
        if(Mathf.Abs((int)current.x - i) > 0 || Mathf.Abs((int)current.z - j) > 0)
        {
            UpdateCell((int)current.x, (int)current.z, assignedRole);
            assignedRole.GridPos = ((int)current.x, (int)current.z);
        }
    }*/

    private void StartMoving()
    {
        moveTween = transform.DOMove(tc.Target.Transform.position, speed).SetSpeedBased(true).SetEase(Ease.Linear);
    } 
}
