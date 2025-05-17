using System.Collections.Generic;

//Contains target information relevant to multiple components of a unit prefab. Keeps all relevant components informed of
//any changes to their relevant attributes

public class TargetContainer
{
    private Unit target;
    public Unit Target
    {
        get => target;
        set
        {
            target = value;
            if (IsFlag)
            {
                TargetingUI.Instance.BestTarget = target;
            }
        }
    }
    public Unit StoredTarget { get; set; }
    public Unit InjectedTarget { get; set; }
    public float Distance { get; set; }
    public float Reach { get; set; }
    public List<Unit> Attackers { get; set; }
    public (int, int) GridPos { get; set; }
    public bool IgnoreAttackers { get; set; }
    public bool IsFlag { get; set; }
    public int SpriteRadius { get; set; }
    public bool IsAutoMode { get; set; }


    public TargetContainer()
    {
        Distance = 100;
        Attackers = new();
        IgnoreAttackers = false;
        IsFlag = false;
        IsAutoMode = true;
    }

    public void AddAttacker(Unit unit)
    {
        if (!Attackers.Contains(unit))
        {
            Attackers.Add(unit);
        }
    }

    public void RemoveAttacker(Unit unit)
    {
        if (!Attackers.Contains(unit))
        {
            Attackers.Remove(unit);
        }
    }

    public void ClearAttackers()
    {
        Attackers.Clear();
    }

    public void InjectTarget(Unit target, bool ignoreAttackers)
    {
        StoreTargetForLater(target);
        InjectedTarget = target;
        IgnoreAttackers = ignoreAttackers;
    }

    public void StoreTargetForLater(Unit newTarget)
    {
        if (newTarget != null)
        {
            StoredTarget = target;
            Target = newTarget;

            Reach = SpriteRadius + newTarget.SpriteRadius;
        }
    }

    public void RestoreTarget()
    {
        if (StoredTarget == null)
        {
            ResetTarget();
        }
        else
        {
            Target = StoredTarget;
            StoredTarget = null;

            Reach = SpriteRadius + target.SpriteRadius;
        }
    }

    public void ResetTarget()
    {
        Target = null;
        Distance = 100;
        Reach = SpriteRadius;
    }
}
