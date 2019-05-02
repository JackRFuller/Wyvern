using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUnitInfo : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] private Button moveActionButton;
    [SerializeField] private Button attackActionButton;
    

    void Start()
    {
        PlayerInteraction.SelectedUnit += TurnOnUIUnitInfo;
        PlayerInteraction.DeselectedUnit += TurnOffUIUnitInfo;

        TurnOffUIUnitInfo();
    }

    private void TurnOffUIUnitInfo()
    {
        unitNameText.enabled = false;
        moveActionButton.gameObject.SetActive(false);
        attackActionButton.gameObject.SetActive(false);
    }

    private void TurnOnUIUnitInfo(UnitView unit)
    {
        unitNameText.text = unit.UnitData.unitName;
        unitNameText.enabled = true;

        //Set Delegates
        moveActionButton.onClick.AddListener(delegate { unit.UnitMovement.OnClickMoveUnit(); });

        moveActionButton.gameObject.SetActive(true);
        attackActionButton.gameObject.SetActive(true);
    }
}
