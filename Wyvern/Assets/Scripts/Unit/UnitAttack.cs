using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : UnitOperation
{
    public void AttackInit()
    {
        if(!m_hasPerformedOperation)
        {
            UnitAction unitAction = new UnitAction(m_unitView, false, m_unitView.UnitData.weapon.weaponTargetMask);
            GameManager.Instance.ActionTiles.ShowActionTiles(unitAction);
        }
    }
}
