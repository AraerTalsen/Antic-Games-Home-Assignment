using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public static class UnitFactory
{
    public static void CreateUnit()
    {
        InitializeUnit(Object.Instantiate(Resources.Load("Prefabs/Unit") as GameObject));

    }

    private static void InitializeUnit(GameObject unit)
    {
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        UnitController uc = unit.GetComponent<UnitController>();
        Unit unitType = Resources.Load("ScriptableObjects/Units/AntDefender") as Unit;
        (sr.sprite, uc.ID, uc.MaxHealth, uc.Speed) = unitType;
        unit.name = unitType.name;
    }
}
