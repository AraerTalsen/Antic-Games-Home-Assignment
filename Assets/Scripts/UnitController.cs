using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : Grid<UnitController>
{
    private int maxHealth, currentHealth;
    private int speed;
    private string id;
    private TargetUnit tu;

    public int MaxHealth {set => maxHealth = value;}
    public int Speed {set => speed = value;}
    public string ID {set => id = value;}

    void Start()
    {
        currentHealth = maxHealth;
        tu.GetComponent<TargetUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Move();
        }
    }

    protected override void RegisterUnit()
    {
        Vector3 currentPos = transform.position;
        int i = (int)currentPos.x, j = (int)currentPos.z;
        AddCell(i, j, this);
        tu.AddUnit(i, j, this);
    }
}
