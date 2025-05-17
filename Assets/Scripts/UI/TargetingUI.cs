using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TargetingUI : MonoBehaviour
{    
    public static TargetingUI Instance {get; private set;}

    [SerializeField]
    private Image targetPortrait; 
    [SerializeField]
    private TMP_Text autoModeTxt;

    private Unit bestTarget;
    private bool isAutoMode = false;
 
    public bool IsAutoMode
    {
        get => isAutoMode;
        set
        {
            isAutoMode = value;
            GameManager.Instance.GameSettings.IsPlayerAutoMode = isAutoMode;
        }
    }
    public Unit BestTarget 
    {
        set
        {
            bestTarget = value;
            if(isAutoMode)
            {
                InstructTarget();
            }
            targetPortrait.sprite = value != null ? value.Sprite : null;
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

    private void Start()
    {
        IsAutoMode = GameManager.Instance.GameSettings.IsPlayerAutoMode;
        autoModeTxt.text = isAutoMode ? "AI Mode OFF" : "AI Mode ON";
    }

    public void InstructTarget()
    {
        PlayerUnitManager.Instance.SetTarget(bestTarget);
    }

    public void ToggleAutoMode()
    {
        IsAutoMode = !isAutoMode;
        PlayerUnitManager.Instance.UpdateAutoMode(isAutoMode);
        autoModeTxt.text = isAutoMode ? "AI Mode OFF" : "AI Mode ON";
    }
}
