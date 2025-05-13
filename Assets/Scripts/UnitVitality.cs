using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitVitality : MonoBehaviour, IHealth
{
    private int maxHealth;
    private int damage;
    private Unit assignedRole;
    private bool isAttacking = false;
    private TargetContainer tc;
    private StatContainer sc;

    public delegate void MyDelegate();
    public MyDelegate WhenDead;

    public int MaxHealth { set => maxHealth = value; }
    public int Damage {set => damage = value;}
    public Unit AssignedRole {set => assignedRole = value;}
    public bool IsAttacking {get => isAttacking; set => isAttacking = value;}
    public TargetContainer TC {set => tc = value;}
    public StatContainer SC {set => sc = value;}

    private void Start()
    {
        sc.CurrentHealth = maxHealth;   
    }

    private void Update()
    {
        if(assignedRole.IsCombatant && tc.Distance <= 1)
        {
            Fight();
        }
    }

    public void ChangeHealth(int amount, Unit other)
    {
        sc.CurrentHealth -= amount;
        CheckIfAlive();
        tc.AddAttacker(other);
    }

    private void CheckIfAlive()
    {
        if(sc.CurrentHealth <= 0)
        {
            WhenDead();
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
        tc.Target.UnitVitality.ChangeHealth(damage, assignedRole);
    }
}
