using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public int MaxHealth {set;}

    public abstract void ChangeHealth(int amount, Unit other);
}
