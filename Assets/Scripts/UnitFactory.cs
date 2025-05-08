using System.Collections;
using System.Collections.Generic;
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
        sr.sprite = unitType.Sprite;
        uc.MaxHealth = unitType.Health;
        uc.Speed = unitType.Speed;
        uc.ID = unitType.ID;
        unit.name = unitType.name;
    }
}
