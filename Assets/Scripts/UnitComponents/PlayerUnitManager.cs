using System.Collections.Generic;
using UnityEngine;

//Broadcasts updates to player units on what to target
public class PlayerUnitManager : MonoBehaviour
{
    public static PlayerUnitManager Instance { get; private set; }

    private List<TargetContainer> targetContainers = new();
    private Color highlight = new Color(255, 255, 0, 255);
    private Color standard = new Color(255, 255, 255, 0);
    private Unit lastTarget;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetTarget(Unit target)
    {
        if (target != null && target.UnitTag.CompareTo("EnemyUnit") == 0)
        {
            if (lastTarget != null)
            {
                lastTarget.SilhouetteSR.color = standard;
            }

            target.SilhouetteSR.color = highlight;
            lastTarget = target;

            foreach (TargetContainer c in targetContainers)
            {
                c.InjectTarget(target, false);
                c.ClearAttackers();
            }
        }
    }

    public void AddContainer(TargetContainer container)
    {
        targetContainers.Add(container);
    }

    public void RemoveContainer(TargetContainer container)
    {
        targetContainers.Remove(container);
    }

    public void ClearManager()
    {
        targetContainers = new();
    }

    public void UpdateAutoMode(bool isAutoMode)
    {
        foreach (TargetContainer c in targetContainers)
        {
            c.IsAutoMode = isAutoMode;
            c.ResetTarget();
        }
    }
}
