using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private string unitTag;
    [SerializeField]
    private string flaggedUnits;  
    [SerializeField]
    private int health; 
    [SerializeField]
    private int speed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool isMobile = true;
    [SerializeField]
    private bool isCombatant = true;

    private Transform transform;

    public Transform Transform {get => transform; set => transform = value;}
    public string UnitTag {get => unitTag;}
    public string FlaggedUnits {get => flaggedUnits;}
    public Sprite Sprite {get => sprite;}
    public bool IsMobile {get => isMobile;}
    public bool IsCombatant {get => isCombatant;}
    public int Health {get => health;}
    public int Speed {get => speed;}
    public int Damage {get => damage;}

    /*public void Deconstruct(out int Health, out int Speed)
    {
        Health = health;
        Speed = speed;
    }*/
}
