using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit",menuName = "Data/Unit",order = 1)]
public class UnitData : ScriptableObject
{
    public string unitName;
    [Range(1, 10)]
    public int healthPoints;
    [Range(1,5)]
    public int movementRange;
}
