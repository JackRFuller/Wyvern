using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalUI : MonoBehaviour
{
    private AnimalView m_animalView;
    private Transform m_playerCameraTransform;

    public AnimalView AnimalView { get { return m_animalView; } }

    private void Start()
    {
        m_animalView = transform.parent.GetComponent<AnimalView>();
        m_playerCameraTransform = PlayerInteraction.PlayerCamera.transform;

        GetComponentInChildren<UIAnimalAwareness>().SetupUIElement(m_animalView);

        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 lookAtVector = new Vector3(m_playerCameraTransform.position.x,
                                           transform.position.y,
                                           m_playerCameraTransform.position.z);

        transform.LookAt(lookAtVector);
    }
}
