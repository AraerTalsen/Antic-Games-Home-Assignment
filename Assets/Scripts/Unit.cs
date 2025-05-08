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

    private Transform transform;

    public Transform Transform {get => transform; set => transform = value;}

    public void Deconstruct(out Sprite Sprite, out string ID, out int Health, out int Speed)
    {
        Sprite = sprite;
        ID = id;
        Health = health;
        Speed = speed;
    }
}
