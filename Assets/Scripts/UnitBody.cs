using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBody : MonoBehaviour
{
    private Unit assignedRole;

    public Unit AssignedRole {get => assignedRole; set => assignedRole = value;}
    private UnitVitality uv;
    private ClickableObject co;
    private TargetContainer tc;

    private void Start()
    { 
        assignedRole.Transform = transform;
        tc = new TargetContainer();
        InitializeClickable();
        InitializeVitality();
        CreateController();
        
        /*if(assignedRole.IsMobile)
        {
            CreateUnitController();
        }*/
    }

    private void InitializeClickable()
    {
        co = GetComponent<ClickableObject>();
        co.UnitSilhouette = assignedRole.Silhouette;
    }

    private void InitializeVitality()
    {
        uv = GetComponent<UnitVitality>();
        uv.MaxHealth = assignedRole.Health;
        uv.Damage = assignedRole.Damage;
        uv.AssignedRole = assignedRole;
    }
    
    private void CreateController()
    {
        uv = GetComponent<UnitVitality>();
        UnitController uc = gameObject.AddComponent<UnitController>();
        uc.AssignedRole = assignedRole;
        uc.Speed = assignedRole.Speed;
        uv.UnitController = uc;
    }
}
