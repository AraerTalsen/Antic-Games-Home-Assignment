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
    private Unit assignedRole;
    private Unit target;
    private int distance;
    private bool isAttacking = false;
    private TargetContainer tc;

    public int CurrentHealth {get => currentHealth; set => currentHealth = value;}
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Damage {get => damage; set => damage = value;}
    public Unit AssignedRole {set => assignedRole = value;}
    public Unit Target {set => target = value;}
    public bool IsAttacking {get => isAttacking; set => isAttacking = value;}
    public TargetContainer TC {set => tc = value;}

    private void Start()
    {
        CurrentHealth = maxHealth;   
    }

    private void Update()
    {
        if(tc.Distance <= 1)
        {
            Fight();
        }
    }

    public void ChangeHealth(int amount, Unit other)
    {
        CurrentHealth -= amount;
        CheckIfAlive();
        tc.AddAttacker(other);
    }

    private void CheckIfAlive()
    {
        if(CurrentHealth <= 0)
        {
            unitController.UnregisterUnit();
        }
    }

    private void Fight()
    {
        if(!isAttacking)
        {
            StartCoroutine("InitiateCombatAction");
        }
    }

    private IEnumerator InitiateCombatAction()
    {
        isAttacking = true;
        AttackAction();
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    private void AttackAction()
    {
        tc.Target.UnitVitality.ChangeHealth(Damage, assignedRole);
    }
}
