using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitController : Grid<Unit>
{
    private int maxHealth, currentHealth;
    private int speed;
    private string id;
    private Unit roleAssignment;
    private TargetUnit tu;
    private Unit target;

    public Unit RoleAssignment {get => roleAssignment; set => roleAssignment = value;}
    public int MaxHealth {set => maxHealth = value;}
    public int Speed {set => speed = value;}
    public string ID {set => id = value;}

    void Start()
    {
        currentHealth = maxHealth;
        roleAssignment.Transform = transform;
        tu.GetComponent<TargetUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        if(target == null)
        {
            target = tu.Target;
        }
        transform.DOMove(target.Transform.position, tu.DistanceFrom(target) / speed * Time.deltaTime);
    }
}
