using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public static class UnitFactory
{
    public static void CreateUnit(int unitID)
    {
        int x = Random.Range(-10, 11);
        int z = Random.Range(-10, 11);
        InitializeUnit(unitID, Object.Instantiate(Resources.Load("Prefabs/Unit") as GameObject, new Vector3(x, 0, z), Quaternion.identity));
    }

    private static void InitializeUnit(int unitID, GameObject unit)
    {
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        UnitController uc = unit.GetComponent<UnitController>();
        Unit unitType = UnitDictionary.units[unitID];
        (sr.sprite, uc.MaxHealth, uc.Speed, uc.IsMobile) = unitType;
        uc.RoleAssignment = unitType;
        unit.name = unitType.name;
    }
}
