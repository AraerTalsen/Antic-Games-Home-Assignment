using UnityEngine;

//Component to attach to units to make them clickable
public class ClickableObject : MonoBehaviour, IClickable
{
    [SerializeField]
    private SpriteRenderer silhouette;
    [SerializeField]
    private Color highlight;
    [SerializeField]
    private Color standard;

    public Sprite UnitSilhouette { get; set; }
    public Unit AssignedRole { get; set; }

    private void Start()
    {
        silhouette.sprite = UnitSilhouette;
        AssignedRole.SilhouetteSR = silhouette;
    }

    public void OnHover()
    {
        silhouette.color = highlight;
    }

    public void OnHoverExit()
    {
        silhouette.color = standard;
    }

    public void OnClick()
    {
        DebugWindow.Instance.LoadDebugInfo(AssignedRole);
        PlayerUnitManager.Instance.SetTarget(AssignedRole);
    }
}
