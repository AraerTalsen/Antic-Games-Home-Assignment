using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitVitality : MonoBehaviour, IHealth
{
    private UnitController unitController;
    public UnitController UnitController {get => unitController; set => unitController = value;}

    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    private int damage;

    public int CurrentHealth {get => currentHealth; set => currentHealth = value;}
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Damage {get => damage; set => damage = value;}

    private void Start()
    {
        CurrentHealth = maxHealth;   
    }

    public void ChangeHealth(int amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0)
        {
            unitController.UnregisterUnit();
        }
    }

    public void Attack()
    {
        unitController.TargetVitality.ChangeHealth(Damage);
    }
}
