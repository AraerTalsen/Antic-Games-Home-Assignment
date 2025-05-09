using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBody : MonoBehaviour
{
    private Unit assignedRole;

    public Unit AssignedRole {set => assignedRole = value;}
    private UnitVitality uv;

    private void Start()
    { 
        assignedRole.Transform = transform;
        InitializeVitality();
        CreateController();
        
        /*if(assignedRole.IsMobile)
        {
            CreateUnitController();
        }*/
    }

    private void CreateController()
    {
        uv = GetComponent<UnitVitality>();
        UnitController uc = gameObject.AddComponent<UnitController>();
        uc.AssignedRole = assignedRole;
        uc.Speed = assignedRole.Speed;

        uc.UnitVitality = uv;
        uv.UnitController = uc;
    }

    private void InitializeVitality()
    {
        uv = GetComponent<UnitVitality>();
        uv.MaxHealth = assignedRole.Health;
        uv.Damage = assignedRole.Damage;
    }
}
