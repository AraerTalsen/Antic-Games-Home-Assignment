using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private Sprite silhouette;
    [SerializeField]
    private string unitTag;
    [SerializeField]
    private string flaggedUnits;  
    [SerializeField]
    private int health; 
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int pointValue;
    [SerializeField]
    private int sightRadius;
    [SerializeField]
    private bool isMobile = true;
    [SerializeField]
    private bool isCombatant = true;
    [SerializeField]
    private List<int> priorityID;


    public string UnitTag {get => unitTag;}
    public string FlaggedUnits {get => flaggedUnits;}
    public Sprite Sprite {get => sprite;}
    public Sprite Silhouette {get => silhouette;}
    public bool IsMobile {get => isMobile;}
    public bool IsCombatant {get => isCombatant;}
    public int Health {get => health;}
    public float Speed {get => speed;}
    public int Damage {get => damage;}
    public int PointValue {get => pointValue;}
    public int SightRadius {get => sightRadius;}
    public List<int> PriorityID {get => priorityID;}
    public Transform Transform { get; set; }
    public UnitVitality UnitVitality { get; set; }
    public TargetContainer TargetContainer { get; set; }
    public StatContainer StatContainer { get; set; }
    public int SpriteRadius { get; set; }
    public SpriteRenderer SilhouetteSR { get; set; }
    public int FlagRiskLevel { get; set; }
}
