using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour, IClickable
{
    [SerializeField]
    private SpriteRenderer silhouette;
    [SerializeField]
    private Color highlight;
    [SerializeField]
    private Color standard;

    private Sprite unitSilhouette;
    private Unit assignedRole;

    public Sprite UnitSilhouette {set => unitSilhouette = value;}
    public Unit AssignedRole {set => assignedRole = value;}

    private void Start()
    {
        silhouette.sprite = unitSilhouette;
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
        DebugWindow.LoadDebugInfo(assignedRole);
    }
}
