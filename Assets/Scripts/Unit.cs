using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private string id; 
    [SerializeField]
    private int health; 
    [SerializeField]
    private int speed; 

    public Sprite Sprite {get => sprite;}
    public string ID {get => id;}
    public int Health {get => health;}
    public int Speed {get => speed;}
}
