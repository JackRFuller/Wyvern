﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public static Action<UnitView> SelectedUnit;
    public static Action DeselectedUnit;

    private static Camera m_playerCamera;
    public static Camera PlayerCamera { get { return m_playerCamera; } }

    [SerializeField] private LayerMask unitLayerMask;

    //Unit
    private Transform m_unitTransform;
    private UnitView m_unitView;

    [Header("Markers")]
    [SerializeField] private GameObject movementTargetMarkerPrefab;

    public static MovementTargetMarker MovementTargetMarker;

    private void Awake()
    {
        m_playerCamera = GetComponent<Camera>();

        //Spawn In Markers
        GameObject movementMarker = Instantiate(movementTargetMarkerPrefab);
        MovementTargetMarker = movementMarker.GetComponent<MovementTargetMarker>();
    }  

    private void Update()
    {
        FindUnit();

        DeselectUnit();
    }

    private void FindUnit()
    {
        Ray ray = m_playerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,Mathf.Infinity,unitLayerMask))
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(m_unitTransform != hit.transform)
                {
                    SelectUnit(hit.transform);
                }
            }
        }
    }

    private void SelectUnit(Transform unit)
    {
        if(m_unitTransform != null)
        {
            if (DeselectedUnit != null)
                DeselectedUnit.Invoke();
        }

        m_unitTransform = unit;
        m_unitView = m_unitTransform.GetComponent<UnitView>();

        if (SelectedUnit != null)
            SelectedUnit.Invoke(m_unitView);
    }

    private void DeselectUnit()
    {
        if(Input.GetMouseButtonDown(1))
        {   
            if(m_unitView != null)
            {
                if(!m_unitView.UnitPerformingAction)
                {
                    m_unitTransform = null;
                    m_unitView = null;

                    if (DeselectedUnit != null)
                        DeselectedUnit.Invoke();
                }
            }
        }
    }
}
