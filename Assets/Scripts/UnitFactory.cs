using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public static class UnitFactory
{
    public static void CreateUnit(int unitID, (int x, int z)? position = null)
    {
        int x, z;
        if(position == null)
        {
            x = Random.Range(-10, 11);
            z = Random.Range(-10, 11);
        }
        else
        {
            x = position.Value.x;
            z = position.Value.z;
        }
        InitializeUnit(unitID, Object.Instantiate(Resources.Load("Prefabs/Unit") as GameObject, new Vector3(x, 0, z), Quaternion.Euler(new Vector3(90, 0, 0))));
    }

    private static void InitializeUnit(int unitID, GameObject unit)
    {
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        UnitBody ub = unit.GetComponent<UnitBody>();
        Unit unitType = Object.Instantiate(UnitDictionary.units[unitID]);
        ub.AssignedRole = unitType;
        sr.sprite = unitType.Sprite;
        unit.name = unitType.name;
    }
}
