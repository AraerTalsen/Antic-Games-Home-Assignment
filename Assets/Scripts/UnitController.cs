using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class UnitController : Grid<Transform>
{
    private int speed;
    private Unit assignedRole;
    private TargetUnit tu;
    private Transform target;
    private (int x, int z) lastGridPos;
    private int distToTarget = 100;
    private UnitVitality unitVitality, targetVitality;
    private Tween moveTween;
    private bool isAttacking = false, isInCombat = false;
    private List<Transform> attackers = new();

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
        if(assignedRole.IsMobile && !isInCombat)
        {
            Move();
        }
        else if(assignedRole.IsCombatant && isInCombat && !isAttacking)
        {
            StartCoroutine("Fight");
        }
    }

    protected override void RegisterUnit()
    {
        Vector3 currentPos = transform.position;
        int i = (int)currentPos.x, j = (int)currentPos.z;
        AddCell(i, j, transform);
        tu.AddUnit(i, j, transform);
    }

    public override void UnregisterUnit()
    {
        DeleteCell(transform);
        tu.DeleteUnit(transform);
        moveTween.Kill();
        Destroy(gameObject);
    }

    protected override void Move()
    {
        CheckForTargetUpdate();
        if(target != null)
        {
            CheckForMoveUpdate();
            isInCombat = tu.DistanceFrom(target) <= 1;
        }
    }

    protected void CheckForTargetUpdate()
    {
        if(target == null || !HasCell(target))
        {
            CheckIfInCombat();
            SetTarget();
        }
    }
    
    private void SetTarget()
    {
        target = tu.GetNewTarget(attackers);
        targetVitality = target != null ? target.gameObject.GetComponent<UnitVitality>() : null;
    }

    private void CheckIfInCombat()
    {
        if(target != null && !HasCell(target))
        {
            if(attackers.Contains(target))
            {
                attackers.Remove(target);
            }

            if(attackers.Count == 0)
            {
                isInCombat = false;
            }
        }
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
        (int i, int j) = GetCell(transform);
        Vector3 current = transform.position;
        if(Mathf.Abs((int)current.x - i) > 0 || Mathf.Abs((int)current.z - j) > 0)
        {
            UpdateCell((int)current.x, (int)current.z, transform);
            tu.UpdateUnit((int)current.x, (int)current.z, transform);
        }
    }

    private void StartMoving()
    {
        moveTween = transform.DOMove(target.position, speed).SetSpeedBased(true).SetEase(Ease.Linear);
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

    public void AddAttacker(Transform unitTrans)
    {
        if(!isInCombat)
        { 
            isInCombat = true;
            target = null; //Change in the future to store old target so unit can continue where it left off?
            CheckForTargetUpdate();
        }
        
        if(!attackers.Contains(unitTrans))
        {
            attackers.Add(unitTrans);
        }
    }
}
