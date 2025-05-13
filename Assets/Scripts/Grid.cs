using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public abstract class Grid<TCell> : MonoBehaviour
{    
    private static Dictionary<(int, int), TCell> registeredUnits = new();
    
    protected void AddCell(int i, int j, TCell cell)
    {
        registeredUnits.Add((i, j), cell);
    }

    public TCell GetCell(int i, int j)
    {
        return registeredUnits.ContainsKey((i, j)) ? registeredUnits[(i, j)] : default;
    }

    protected void DeleteCell(TCell cell)
    {
        //registeredUnits.Remove(cell);
    }

    protected void DeleteCell(int i, int j)
    {
        registeredUnits.Remove((i, j));
    }

    protected abstract void RegisterUnit();
    public abstract void UnregisterUnit();

    protected abstract void Move();
}
