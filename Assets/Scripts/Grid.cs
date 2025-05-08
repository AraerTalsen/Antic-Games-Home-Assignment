using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System;

public abstract class Grid<TCell> : MonoBehaviour
{    
    private static Dictionary<TCell, (int, int)> registedUnits = new();
    
    protected void AddCell(int i, int j, TCell cell)
    {
        registedUnits.Add(cell, (i, j));
    }

    protected abstract void RegisterUnit();

    protected void Move()
    {
        /*float x = Random.Range(-10, 11);
        float z = Random.Range(-10, 11);
        Vector3 move = new Vector3(x, 0, z);
        transform.DOMove(move, Vector3.Distance(transform.position, move));*/
    }
}
