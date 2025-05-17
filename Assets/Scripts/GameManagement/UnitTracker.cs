using UnityEngine;

//Assists game manager by tracking unit death and creation relevant to game state
public class UnitTracker
{
    public delegate void MyDelegate();
    public MyDelegate endGame;

    private int enemyInsectCount;
    private int endState = 0;

    //0 = pause, 1 = wind, 2 = lose
    public int EndState { get => endState; }

    private void ChangeEnemyCount(int qty)
    {
        enemyInsectCount += qty;

        if (enemyInsectCount <= 0)
        {
            endState = 1;
            endGame();
        }
    }

    public void ClearTracker()
    {
        enemyInsectCount = 0;
        endState = 0;
    }

    public void AddUnit(GameObject g)
    {
        CheckIfEnemy(g, 1);
    }

    public void RemoveUnit(GameObject g)
    {
        if (!GameManager.endGame)
        {
            CheckIfFlag(g);
            CheckIfEnemy(g, -1);
        }
    }

    private void CheckIfFlag(GameObject g)
    {
        if (g.GetComponent<UnitBody>().AssignedRole.UnitTag.CompareTo("Flag") == 0)
        {
            endState = 2;
            endGame();
        }
    }

    private void CheckIfEnemy(GameObject g, int qty)
    {
        if (g.GetComponent<UnitBody>().AssignedRole.UnitTag.CompareTo("EnemyUnit") == 0)
        {
            ChangeEnemyCount(qty);
        }
    }
}
