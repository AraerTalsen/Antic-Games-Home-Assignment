using System.Collections.Generic;
using UnityEngine;

public abstract class Grid<TCell> : MonoBehaviour
{
    //Units and whatever else TCell gets defined to
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
        (int, int)? key = null;
        foreach (KeyValuePair<(int, int), TCell> pair in registeredUnits)
        {
            if (pair.Value.Equals(cell))
            {
                key = pair.Key;
                break;
            }
        }

        if (key != null)
        {
            registeredUnits.Remove(key.Value);
        }
        else
        {
            Debug.Log($"{cell} does not currently exist in the grid");
        }
    }

    protected void DeleteCell(int i, int j)
    {
        registeredUnits.Remove((i, j));
    }

    protected abstract void RegisterUnit();
    public abstract void UnregisterUnit();

    protected abstract void Move();
}
