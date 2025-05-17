//Configures player units to utilize player controls (mouse, targeting UI, and AI mode)
public class PlayerController : UnitBody
{
    protected override void Start()
    {
        base.Start();
        if (AssignedRole.UnitTag.CompareTo("Flag") != 0)
        {
            PlayerUnitManager.Instance.AddContainer(AssignedRole.TargetContainer);
            AssignedRole.TargetContainer.IsAutoMode = TargetingUI.Instance.IsAutoMode;
        }
        else
        {
            AssignedRole.TargetContainer.IsFlag = true;
        }
    }

    public override void UnregisterUnit()
    {
        PlayerUnitManager.Instance.RemoveContainer(AssignedRole.TargetContainer);
        base.UnregisterUnit();
    }
}
