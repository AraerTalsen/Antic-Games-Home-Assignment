using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class DebugWindow : MonoBehaviour
{
    [SerializeField]
    private static TMP_Text[] debugInfoText;
    private static Unit currentUnit;

    private void Start()
    {
        debugInfoText = transform.Cast<Transform>().Select(child => child.GetComponent<TMP_Text>()).ToArray();
    }

    private void Update()
    {
        UpdateDebugInfo();
    }
    
    public static void LoadDebugInfo(Unit u)
    {
        currentUnit = u;
        UpdateDebugInfo();
    }

    private static void UpdateDebugInfo()
    {
        if(currentUnit != null)
        {
            debugInfoText[0].text = "Current Target: " + currentUnit.name;
            debugInfoText[1].text = "Distance: " + currentUnit.TargetContainer.Distance.ToString();
            debugInfoText[2].text = "Targeting: " + currentUnit.TargetContainer.Target.ToString();
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
