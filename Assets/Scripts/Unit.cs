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
    private bool isMobile = true;

    private Transform transform;

    public Transform Transform {get => transform; set => transform = value;}
    public string UnitTag {get => unitTag;}
    public string FlaggedUnits {get => flaggedUnits;}

    public void Deconstruct(out Sprite Sprite, out int Health, out int Speed, out bool IsMobile)
    {
        Sprite = sprite;
        Health = health;
        Speed = speed;
        IsMobile = isMobile;
    }
}
