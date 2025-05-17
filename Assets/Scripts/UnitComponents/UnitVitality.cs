using System.Collections;using UnityEngine;
using TMPro;

//A unit's health and combat functionality
public class UnitVitality : MonoBehaviour, IHealth
{
    [SerializeField]
    private TMP_Text health;

    public delegate void MyDelegate();
    public MyDelegate WhenDead;

    public int MaxHealth { get; set; }
    public int Damage { get; set; }
    public Unit AssignedRole { get; set; }
    public bool IsAttacking { get; set; }
    public TargetContainer TC { get; set; }
    public StatContainer SC { get; set; }

    private void Start()
    {
        SC.CurrentHealth = MaxHealth;
        health.text = SC.CurrentHealth + "/" + MaxHealth;
        IsAttacking = false;
    }

    private void Update()
    {
        if (AssignedRole.IsCombatant && TC.Distance <= TC.Reach)
        {
            Fight();
        }
    }

    public void ChangeHealth(int amount, Unit other)
    {
        SC.CurrentHealth -= amount;
        health.text = SC.CurrentHealth + "/" + MaxHealth;
        CheckIfAlive(other);
        if (!TC.IgnoreAttackers)
        {
            TC.ResetTarget();
            TC.AddAttacker(other);
        }
    }
    
    //Callback from killed unit to killer unit to let it know that it won
    public void KilledTarget()
    {
        if (AssignedRole.UnitTag.CompareTo("EnemyUnit") != 0)
        {
            ScoreManager.Instance.Score += TC.Target.PointValue;
        }
        TC.RestoreTarget();
    }

    private void CheckIfAlive(Unit other)
    {
        if (SC.CurrentHealth <= 0)
        {
            other.UnitVitality.KilledTarget();
            WhenDead();
        }
    }

    private void Fight()
    {
        if (!IsAttacking)
        {
            StartCoroutine("InitiateCombatAction");
        }
    }

    private IEnumerator InitiateCombatAction()
    {
        IsAttacking = true;
        CombatAction();
        yield return new WaitForSeconds(1);
        IsAttacking = false;
    }

    private void CombatAction()
    {
        TC.Target.UnitVitality.ChangeHealth(Damage, AssignedRole);
    }
}
