using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public abstract class Grid<TCell> : MonoBehaviour
{    
    public static Dictionary<TCell, (int, int)> registeredUnits = new();
    
    protected void AddCell(int i, int j, TCell cell)
    {
        registeredUnits.Add(cell, (i, j));
    }

    protected (int, int) GetCell(TCell cell)
    {
        return registeredUnits[cell];
    }

    protected bool HasCell(TCell cell)
    {
        return registeredUnits.ContainsKey(cell);
    }

    /*protected TCell GetCell(int i, int j)
    {
        
    }*/
    
    protected void UpdateCell(int i, int j, TCell cell)
    {
        registeredUnits[cell] = (i, j);   
    }

    protected void DeleteCell(TCell cell)
    {
        registeredUnits.Remove(cell);
    }

    protected void DeleteCell(int i, int j)
    {
        //registeredUnits.Remove(registeredUnits.);
    }

    protected abstract void RegisterUnit();
    public abstract void UnregisterUnit();

    protected abstract void Move();
}
