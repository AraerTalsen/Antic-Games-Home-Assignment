using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UnitBody : MonoBehaviour
{
    private Unit assignedRole;

    public Unit AssignedRole {get => assignedRole; set => assignedRole = value;}
    private UnitVitality uv;
    private UnitController uc;
    private ClickableObject co;

    private void Start()
    { 
        assignedRole.Transform = transform;
        assignedRole.TargetContainer = new TargetContainer();
        assignedRole.StatContainer = new StatContainer();

        InitializeController();
        InitializeVitality();
        InitializeClickable();
    }

    private void InitializeClickable()
    {
        co = GetComponent<ClickableObject>();
        co.AssignedRole = assignedRole;
        co.UnitSilhouette = assignedRole.Silhouette;
    }

    private void InitializeVitality()
    {
        uv = GetComponent<UnitVitality>();
        assignedRole.UnitVitality = uv;
        uv.MaxHealth = assignedRole.Health;
        uv.Damage = assignedRole.Damage;
        uv.AssignedRole = assignedRole;
        uv.SC = assignedRole.StatContainer;
        uv.TC = assignedRole.TargetContainer;
        uv.WhenDead = UnregisterUnit;

    }
    
    private void InitializeController()
    {
        uc = GetComponent<UnitController>();
        uc.AssignedRole = assignedRole;
        uc.Speed = assignedRole.Speed;
        uc.TC = assignedRole.TargetContainer;
    }

    private void UnregisterUnit()
    {
        uc.UnregisterUnit();
    }
}
