using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private bool isPlayerAutoMode = false;
    [SerializeField]
    private int enemySwarmSize;

    public bool IsPlayerAutoMode {get => isPlayerAutoMode; set => isPlayerAutoMode = value;}
    public int EnemySwarmSize {get => enemySwarmSize; set => enemySwarmSize = value;}
}
