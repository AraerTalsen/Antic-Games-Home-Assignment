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
    private Unit target;
    private (int x, int z) lastGridPos;
    private int distToTarget = 100;
    private UnitVitality unitVitality, targetVitality;
    private Tween moveTween;
    private bool isAttacking = false;

    public Unit AssignedRole {get => assignedRole; set => assignedRole = value;}
    public int Speed {set => speed = value;}
    public UnitVitality UnitVitality {get => unitVitality; set => unitVitality = value;}
    public UnitVitality TargetVitality {get => targetVitality; set => targetVitality = value;}

    void Start()
    {
        tu = GetComponent<TargetUnit>();

        RegisterUnit();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUnitGridPos();
        print(name + " " + (target != null) + " " + !isAttacking + " " + (distToTarget > 1));
        if(assignedRole.IsMobile && distToTarget > 1)
        {
            Move();
        }
        else if(assignedRole.IsCombatant && target != null && !isAttacking)
        {
            StartCoroutine("Fight");
        }
    }

    protected override void RegisterUnit()
    {
        Vector3 currentPos = transform.position;
        int i = (int)currentPos.x, j = (int)currentPos.z;
        AddCell(i, j, assignedRole);
        tu.AddUnit(i, j, assignedRole);
    }

    public override void UnregisterUnit()
    {
        DeleteCell(assignedRole);
        tu.DeleteUnit(assignedRole);
        moveTween.Kill();
        Destroy(gameObject);
    }

    protected override void Move()
    {
        CheckForTargetUpdate();
        if(target != null)
        {
            CheckForMoveUpdate();
            distToTarget = tu.DistanceFrom(target);
        }
    }

    protected void CheckForTargetUpdate()
    {
        if(target == null || !HasCell(target))
        {
            SetTarget();
        }
    }
    
    private void SetTarget()
    {
        target = tu.Target;
        targetVitality = target != null ? target.Transform.gameObject.GetComponent<UnitVitality>() : null;
    }

    protected virtual void CheckForMoveUpdate()
    {
        int dist = tu.DistanceBetween(lastGridPos, GetCell(target));

        if(dist > 0)
        { 
            if(moveTween != null)
            {
                moveTween?.Kill();
            }
            lastGridPos = GetCell(target);
            StartMoving();
        }
    }

    protected virtual void UpdateUnitGridPos()
    {
        (int i, int j) = GetCell(assignedRole);
        Vector3 current = transform.position;
        if(Mathf.Abs((int)current.x - i) > 0 || Mathf.Abs((int)current.z - j) > 0)
        {
            UpdateCell((int)current.x, (int)current.z, assignedRole);
            tu.UpdateUnit(i, j, assignedRole);
        }
    }

    private void StartMoving()
    {
        moveTween = transform.DOMove(target.Transform.position, speed).SetSpeedBased(true);
    }

    private IEnumerator Fight()
    {
        print("In");
        isAttacking = true;
        unitVitality.Attack();
        CheckForTargetUpdate();
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }
}
