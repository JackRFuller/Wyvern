using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon",order = 2)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    [Range(1,8)]
    public int weaponRange;
    [Range(1,8)]
    public int weaponDamage;
    public LayerMask weaponTargetMask;
}
