using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalView : MonoBehaviour
{
    [SerializeField] private AnimalData m_animalData;

    private AnimalFOV m_animalFOV;
    private AnimalAwareness m_animalAwareness;
    private AnimalMesh m_animalMesh;
    private AnimalBehaviour m_animalBehaviour;

    public AnimalData AnimalData { get { return m_animalData; } }
    public AnimalMesh AnimalMesh { get { return m_animalMesh; } }
    public AnimalAwareness AnimalAwareness { get { return m_animalAwareness; } }

    private void Awake()
    {
        m_animalFOV = this.gameObject.AddComponent<AnimalFOV>();
        m_animalMesh = GetComponentInChildren<AnimalMesh>();
        m_animalAwareness = gameObject.AddComponent<AnimalAwareness>();
        m_animalBehaviour = gameObject.AddComponent<AnimalBehaviour>();
    }
}
