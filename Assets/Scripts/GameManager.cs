using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitFactory.CreateUnit(0, (-5, 0));
        UnitFactory.CreateUnit(0, (1, 0));
        UnitFactory.CreateUnit(1, (-1, 0));
    }
}
