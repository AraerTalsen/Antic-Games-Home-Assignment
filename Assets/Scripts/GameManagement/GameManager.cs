using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public GameObject homePanel;
    public GameObject pausePanel;

    public delegate void MyDelegate(GameObject g);
    public static MyDelegate addUnit;
    public static MyDelegate removeUnit;
    public static bool pauseUnitActivity = false;
    public static bool endGame = false;

    private UnitTracker unitTracker;
    private AntFactory af;
    private EnemyFactory ef;

    public GameSettings GameSettings { get; set; }

    private void Awake()
    {
        TrySetInstance();
        
        unitTracker = new UnitTracker();
        af = new AntFactory();
        ef = new EnemyFactory();
        addUnit = unitTracker.AddUnit;
        removeUnit = unitTracker.RemoveUnit;
        unitTracker.endGame = TogglePause;
    }

    private void TrySetInstance()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            GameSettings = Resources.Load("ScriptableObjects/GameSettings") as GameSettings;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && unitTracker.EndState == 0)
        {
            TogglePause();
        }   
    }

    public void TogglePause()
    {
        Time.timeScale = !pauseUnitActivity ? 0 : 1;
        pauseUnitActivity = !pauseUnitActivity;
        pausePanel.SetActive(!pausePanel.activeSelf);
        GameObject pauseMenu = pausePanel.transform.GetChild(unitTracker.EndState).gameObject;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }


    public void PlayGame()
    {
        homePanel.SetActive(false);
        endGame = false;
        af.CreateUnits(1);
        ef.CreateUnits();
    }

    public void StopGame()
    {
        endGame = true;
        unitTracker.ClearTracker();
        PlayerUnitManager.Instance.ClearManager();
        ScoreManager.Instance.Score = 0;
        homePanel.SetActive(true);
    }

    public void RestartGame()
    {
        StopGame();
        StartCoroutine("DelayRestart");
    }

    //This extra frame lets all of the units have time to see the game has ended and self-destruct
    private IEnumerator DelayRestart()
    {
        yield return new WaitForNextFrameUnit();
        PlayGame();
    }
}
