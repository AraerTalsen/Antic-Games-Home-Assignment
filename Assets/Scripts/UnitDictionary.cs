using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitDictionary
{
    public static Dictionary<int, Unit> units = new Dictionary<int, Unit>
    {
        {0, Resources.Load("ScriptableObjects/Units/Flag") as Unit},
        {1, Resources.Load("ScriptableObjects/Units/AntDefender") as Unit},
        {2, Resources.Load("ScriptableObjects/Units/Aphid") as Unit}
    };


}
