using UnityEngine;
using TMPro;
using System.Linq;

public class DebugWindow : MonoBehaviour
{
    public static DebugWindow Instance {get; private set;}
    
    private TMP_Text[] debugInfoText;
    private Unit currentUnit;

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

        debugInfoText = transform.Cast<Transform>().Select(child => child.GetComponent<TMP_Text>()).ToArray();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateDebugInfo();
    }
    
    public void LoadDebugInfo(Unit u)
    {
        currentUnit = u;
        UpdateDebugInfo();
    }

    private void UpdateDebugInfo()
    {
        if(currentUnit != null)
        {
            debugInfoText[0].text = "Current Target: " + currentUnit.name;
            debugInfoText[1].text = "Distance: " + currentUnit.TargetContainer.Distance.ToString();
            debugInfoText[2].text = "Value: " + currentUnit.PointValue;
            debugInfoText[3].text = "Flag Risk: " + currentUnit.FlagRiskLevel;
        }
        else
        {
            for(int i = 0; i < debugInfoText.Length; i++)
            {
                debugInfoText[i].text = "";
            }

            debugInfoText[0].text = "None Selected";
        }
    }
}
