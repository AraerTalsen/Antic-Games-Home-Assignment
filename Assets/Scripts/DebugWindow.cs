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
            Dictionary<string, object> debugInfo = currentUnit.DebugData();

            debugInfoText[0].text = currentUnit.name;

            for(int i = 0; i < debugInfoText.Length && i < debugInfo.Count; i++)
            {
                KeyValuePair<string, object> element = debugInfo.ElementAt(i);
                debugInfoText[i + 1].text = $"{element.Key}: {element.Value}";
            }
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
