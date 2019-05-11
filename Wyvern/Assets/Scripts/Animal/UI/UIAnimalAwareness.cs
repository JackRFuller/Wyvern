using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimalAwareness : MonoBehaviour
{
    private AnimalView m_animalView;

    [Header("UI Elements")]
    [SerializeField] private Image m_meterFillImage;
    [SerializeField] private Image m_meterBGImage;
    [SerializeField] private Image m_fullyAlertImage;

    private float m_meterFillTarget;
    private float m_meterFillRate = 1f;
    
    private void Start()
    {
        TurnOffAwarenessMeter();
        TurnOffFullAlertIcon();
    }    

    public void SetupUIElement(AnimalView animalView)
    {
        m_animalView = animalView;
        m_animalView.AnimalAwareness.AnimalAwarenessChanged += AwarenessMeterChanged;
    }

    private void AwarenessMeterChanged(float awarnessValue)
    {
        m_meterFillTarget = awarnessValue / 10.0f;
        m_meterFillImage.fillAmount = m_meterFillTarget;

        if(m_meterFillTarget >= 1.0f)
        {
            TurnOffAwarenessMeter();
            TurnOnFullAlertIcon();
        }
        else
        {
            TurnOnAwarenessMeter();
        }

        
    }   

    private void TurnOnFullAlertIcon()
    {
        m_fullyAlertImage.enabled = true;
    }

    private void TurnOffFullAlertIcon()
    {
        m_fullyAlertImage.enabled = false;
    }

    private void TurnOnAwarenessMeter()
    {
        m_meterFillImage.enabled = true;
        m_meterBGImage.enabled = true;
    }

    private void TurnOffAwarenessMeter()
    {
        m_meterFillImage.enabled = false;
        m_meterBGImage.enabled = false;
       
        this.enabled = false;
    }
}
