using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIEndTurnButton : MonoBehaviour
{
    private Button m_button;
    private Image m_buttonImage;
    private TMP_Text m_buttonText;

    // Start is called before the first frame update
    void Start()
    {
        m_button = GetComponent<Button>();
        m_buttonImage = GetComponent<Image>();
        m_buttonText = GetComponent<TMP_Text>();

        m_button.onClick.AddListener(delegate { GameManager.Instance.TurnManager.StartNewPlayerTurn(); });
    }

    private void TurnOffEndTurnButton()
    {
        m_button.enabled = false;
        m_buttonImage.enabled = false;
        m_buttonText.enabled = false;
    }

    private void TurnOnEndTurnButton()
    {
        m_button.enabled = true;
        m_buttonImage.enabled = true;
        m_buttonText.enabled = true;
    }
}
