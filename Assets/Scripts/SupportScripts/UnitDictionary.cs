using System.Collections.Generic;
using UnityEngine;

public static class UnitDictionary
{
    public static Dictionary<int, Unit> units = new Dictionary<int, Unit>
    {
        {0, Resources.Load("ScriptableObjects/Units/Flag") as Unit},
        {1, Resources.Load("ScriptableObjects/Units/AntDefender") as Unit},
        {2, Resources.Load("ScriptableObjects/Units/Aphid") as Unit},
        {3, Resources.Load("ScriptableObjects/Units/Ladybug") as Unit},
        {4, Resources.Load("ScriptableObjects/Units/Beetle") as Unit},
        {5, Resources.Load("ScriptableObjects/Units/Dud") as Unit}
    };
}
