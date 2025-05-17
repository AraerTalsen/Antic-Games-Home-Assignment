using UnityEngine;

//Enemy unit generation logic
public class EnemyFactory : UnitFactory
{
    public EnemyFactory()
    {
        directory = "prefabs/EnemyUnit";
    }

    public override void CreateUnits(int qty = 0, (int x, int z)? position = null)
    {
        qty = qty == 0 ? GameManager.Instance.GameSettings.EnemySwarmSize : qty;
        base.CreateUnits(qty, position);
    }

    protected override int ChooseUnitID()
    {
        return Random.Range(2, 5);
    }

    protected override Vector3 GenerateSpawnPoint()
    {
        float x = Random.Range(-1.0f, 1.0f);
        float z = Random.Range(-1.0f, 1.0f);

        Vector3 direction = new Vector3(x, 0, z).normalized;

        float distance = Random.Range(20.0f, 50.0f);

        return direction * distance;
    }
}
