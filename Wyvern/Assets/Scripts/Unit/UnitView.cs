﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private UnitData m_unitData;
    private UnitMovement m_unitMovement;


    public UnitData UnitData { get { return m_unitData; } }
    public UnitMovement UnitMovement { get { return m_unitMovement; } }

    private void Awake()
    {
        m_unitMovement = this.gameObject.AddComponent<UnitMovement>();
    }
}
