using System;
using System.Collections.Generic;
using UnityEngine;

//Measures the priority level for a unit found in the BFS relative to the searching unit's priorities
public static class PriorityAssessment
{
    private static Dictionary<int, Func<object, int>> priorityChecks = new Dictionary<int, Func<object, int>>
    {
        {0, obj =>
            {
                int distance;

                if(obj is int)
                {
                    distance = (int)obj;
                }
                else
                {
                    (Vector3 currentPos, Vector3 enemyPos) = ((Vector3, Vector3))obj;
                    distance = (int)Vector3.Distance(currentPos, enemyPos);
                }

                int priority = 3 - (int)Mathf.Clamp(distance * 0.1f, 0, 3);
                return priority;
            }
        },
        {1, obj =>
            {
                Unit unit = (Unit)obj;
                int healthP = (int)Mathf.Clamp(unit.Health * 0.2f, 0, 3) + 1;
                int damageP = unit.Damage;
                int speedP = (int)Mathf.Clamp(unit.Speed * 2, 0, 3);

                return (healthP + damageP + speedP) / 3;
            }
        },
        {2, obj =>
            {
                Unit unit = (Unit)obj;
                return (int)(unit.PointValue * 0.1f) - 1;
            }
        }
    };

    public static int MeasurePriorityLevel(List<int> priorities, (object, Unit) context)
    {
        int unitTotalPriority = 0;
        for (int i = 0; i < priorities.Count; i++)
        {
            int index = priorities[i];
            object obj = index == 0 ? context.Item1 : context.Item2;
            unitTotalPriority += priorityChecks[index](obj);
        }
        return unitTotalPriority > 0 ? unitTotalPriority / priorities.Count : 0;
    }
}
