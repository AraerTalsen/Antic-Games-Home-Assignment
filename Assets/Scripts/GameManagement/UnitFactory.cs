using UnityEngine;

//Unit generation logic
public abstract class UnitFactory
{
    public delegate void ChangeEnemyCount(int qty = -1);

    //For communicating with the game manager on new enemy creation
    public static ChangeEnemyCount changeEnemyCount;

    protected string directory = "Prefabs/Unit";

    public virtual void CreateUnits(int qty = 0, (int x, int z)? position = null)
    {
        for (int i = 0; i < qty; i++)
        {
            Vector3 spawnPos = GenerateSpawnPoint();

            InitializeUnit(ChooseUnitID(), InstantiateUnit(spawnPos));
        }
    }

    protected GameObject InstantiateUnit(Vector3 spawnPos)
    {
        return Object.Instantiate(Resources.Load(directory) as GameObject, spawnPos, Quaternion.Euler(Vector3.zero));
    }

    protected abstract Vector3 GenerateSpawnPoint();
    protected abstract int ChooseUnitID();

    protected void InitializeUnit(int unitID, GameObject unit)
    {
        SpriteRenderer sr = unit.transform.GetChild(0).GetComponent<SpriteRenderer>();
        UnitBody ub = unit.GetComponent<UnitBody>();
        Unit unitType = Object.Instantiate(UnitDictionary.units[unitID]);
        ub.AssignedRole = unitType;
        sr.sprite = unitType.Sprite;
        unit.name = unitType.name;
    }
}
