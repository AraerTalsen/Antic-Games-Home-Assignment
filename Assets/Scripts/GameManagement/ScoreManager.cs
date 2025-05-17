using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get; private set;}

    [SerializeField]
    private TMP_Text panelTxt;
    [SerializeField]
    private TMP_Text winTxt;
    [SerializeField]
    private TMP_Text loseTxt;

    private int score;

    public int Score 
    {
        get => score; 
        set
        {
            score = value;
            panelTxt.text = score.ToString();
            winTxt.text = "Score: " + score.ToString();
            loseTxt.text = "Score: " + score.ToString();
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
