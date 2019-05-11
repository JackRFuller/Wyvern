using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUnitInfo : MonoBehaviour
{
    private UnitView m_currentUnit;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text unitNameText; 
    [SerializeField] private Button attackActionButton;

    [Header("Move Button")]
    [SerializeField] private Button moveActionButton;
    [SerializeField] private Image moveActionImage;
    

    void Start()
    {
        PlayerInteraction.SelectedUnit += SetUnitInfo;
        PlayerInteraction.DeselectedUnit += TurnOffUIUnitInfo;

        TurnOffUIUnitInfo();
    }

    private void SetUnitInfo(UnitView unit)
    {
        m_currentUnit = unit;
        unitNameText.text = m_currentUnit.UnitData.unitName;

        //Set Delegates
        moveActionButton.onClick.AddListener(delegate { m_currentUnit.UnitMovement.MovementInit(); });
        attackActionButton.onClick.AddListener(delegate { m_currentUnit.UnitAttack.AttackInit(); });

        m_currentUnit.UnitMovement.UnitMoving += TurnOffUIUnitInfo;
        m_currentUnit.UnitMovement.UnitStoppedMoving += TurnOnUIUnitInfo;

        TurnOnUIUnitInfo();
    }

    private void TurnOnUIUnitInfo()
    {       
        unitNameText.enabled = true;

        moveActionImage.color = m_currentUnit.UnitMovement.HasMoved ? Color.gray : Color.black;
        moveActionButton.enabled = !m_currentUnit.UnitMovement.HasMoved;

        moveActionButton.gameObject.SetActive(true);
        attackActionButton.gameObject.SetActive(true);
    }

    private void TurnOffUIUnitInfo()
    {
        unitNameText.enabled = false;

        moveActionButton.onClick.RemoveListener(delegate { m_currentUnit.UnitMovement.MovementInit(); });
        attackActionButton.onClick.RemoveListener(delegate { m_currentUnit.UnitAttack.AttackInit(); });

        moveActionButton.gameObject.SetActive(false);
        attackActionButton.gameObject.SetActive(false);
    }

    
}
