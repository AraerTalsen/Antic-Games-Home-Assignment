using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UnitFactory.CreateUnit(0, (0, 0));
        UnitFactory.CreateUnit(1, (0, 0));
        UnitFactory.CreateUnit(2, (10, 0));
        UnitFactory.CreateUnit(2, (-15, 0));
        UnitFactory.CreateUnit(2, (0, 20));
    }
}
