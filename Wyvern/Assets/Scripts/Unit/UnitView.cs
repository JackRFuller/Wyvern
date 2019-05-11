using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private UnitData m_unitData;
    private UnitMovement m_unitMovement;
    private UnitAttack m_unitAttack;
    private UnitAnimation m_unitAnimation;


    public UnitData UnitData { get { return m_unitData; } }
    public UnitMovement UnitMovement { get { return m_unitMovement; } }
    public UnitAttack UnitAttack { get { return m_unitAttack; } }

    private bool m_unitPerformingAction; 
    public bool UnitPerformingAction { get { return m_unitPerformingAction; } }

    private void Awake()
    {
        m_unitMovement = this.gameObject.AddComponent<UnitMovement>();
        m_unitAnimation = this.gameObject.AddComponent<UnitAnimation>();
        m_unitAttack = this.gameObject.AddComponent<UnitAttack>();
    }

    public void SetUnitActionState(bool performingAction)
    {
        m_unitPerformingAction = performingAction;
    }
}
