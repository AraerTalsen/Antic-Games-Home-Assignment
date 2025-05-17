using UnityEngine;

//Manages all component setup for a unit
public class UnitBody : MonoBehaviour
{
    public Unit AssignedRole { get; set; }
    private UnitVitality uv;
    private UnitController uc;
    private ClickableObject co;

    protected virtual void Start()
    {
        GameManager.addUnit(gameObject);

        AssignedRole.Transform = transform;
        AssignedRole.SpriteRadius = Mathf.CeilToInt((AssignedRole.Sprite.rect.size / AssignedRole.Sprite.pixelsPerUnit).x / 2);
        AssignedRole.TargetContainer = new TargetContainer();
        AssignedRole.TargetContainer.Reach = AssignedRole.SpriteRadius;
        AssignedRole.TargetContainer.SpriteRadius = AssignedRole.SpriteRadius;
        AssignedRole.StatContainer = new StatContainer();

        InitializeController();
        InitializeVitality();
        InitializeClickable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && AssignedRole.UnitTag.CompareTo("EnemyUnit") != 0)
        {
            print($"{AssignedRole.name} is targeting {AssignedRole.TargetContainer.Target} at {AssignedRole.TargetContainer.Target.Transform.position}");
        }
    }

    private void InitializeClickable()
    {
        co = GetComponent<ClickableObject>();
        co.AssignedRole = AssignedRole;
        co.UnitSilhouette = AssignedRole.Silhouette;
    }

    private void InitializeVitality()
    {
        uv = GetComponent<UnitVitality>();
        AssignedRole.UnitVitality = uv;
        uv.MaxHealth = AssignedRole.Health;
        uv.Damage = AssignedRole.Damage;
        uv.AssignedRole = AssignedRole;
        uv.SC = AssignedRole.StatContainer;
        uv.TC = AssignedRole.TargetContainer;
        uv.WhenDead = UnregisterUnit;

    }

    private void InitializeController()
    {
        uc = GetComponent<UnitController>();
        uc.AssignedRole = AssignedRole;
        uc.Speed = AssignedRole.Speed;
        uc.TC = AssignedRole.TargetContainer;
    }

    //Helps pass communication about the unit's death to the UnitController
    public virtual void UnregisterUnit()
    {
        uc.UnregisterUnit();
    }
}
