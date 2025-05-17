using UnityEngine;

//Player unit generation logic
public class AntFactory : UnitFactory
{
    public AntFactory()
    {
        directory = "prefabs/PlayerUnit";
    }

    public override void CreateUnits(int qty, (int x, int z)? position = null)
    {
        InitializeUnit(0, InstantiateUnit(GenerateSpawnPoint()));

        base.CreateUnits(qty, position);
    }

    protected override int ChooseUnitID()
    {
        return 1;
    }

    protected override Vector3 GenerateSpawnPoint()
    {
        return Vector3.zero;
    }
}
