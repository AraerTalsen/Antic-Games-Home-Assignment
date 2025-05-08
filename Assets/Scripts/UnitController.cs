using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitController : Grid<Unit>
{
    private int maxHealth, currentHealth;
    private int speed;
    private Unit roleAssignment;
    private TargetUnit tu;
    private Unit target;
    private bool isMobile;

    public Unit RoleAssignment {get => roleAssignment; set => roleAssignment = value;}
    public int MaxHealth {set => maxHealth = value;}
    public int Speed {set => speed = value;}
    public bool IsMobile {set => isMobile = value;}

    void Start()
    {
        currentHealth = maxHealth;
        roleAssignment.Transform = transform;
        tu = GetComponent<TargetUnit>();

        RegisterUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMobile && target == null)
        {
            Move();
        }
    }

    protected override void RegisterUnit()
    {
        Vector3 currentPos = transform.position;
        int i = (int)currentPos.x, j = (int)currentPos.z;
        AddCell(i, j, roleAssignment);
        tu.AddUnit(i, j, roleAssignment);
    }

    protected override void Move()
    {
        target = tu.Target;
        float dist = tu.DistanceFrom(target);
        transform.DOMove(target.Transform.position, dist / speed);
    }
}
