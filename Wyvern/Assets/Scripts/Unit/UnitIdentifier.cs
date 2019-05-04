using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdentifier : MonoBehaviour
{
    private UnitView m_unitView;
    private SpriteRenderer m_unitID;

    private void Start()
    {
        m_unitView = transform.parent.GetComponent<UnitView>();
        m_unitID = GetComponent<SpriteRenderer>();

        PlayerInteraction.SelectedUnit += TurnOnUnitID;
        PlayerInteraction.DeselectedUnit += TurnOffUnitID;

        TurnOffUnitID();

    }

    private void TurnOnUnitID(UnitView unitView)
    {
        if(unitView == m_unitView)
        {
            m_unitID.enabled = true;
        }
    }

    private void TurnOffUnitID()
    {
        m_unitID.enabled = false;
    }
}
