using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public abstract class Grid<TCell> : MonoBehaviour
{    
    public static Dictionary<TCell, (int, int)> registedUnits = new();
    
    protected void AddCell(int i, int j, TCell cell)
    {
        registedUnits.Add(cell, (i, j));
    }

    protected abstract void RegisterUnit();

    protected abstract void Move();
}
