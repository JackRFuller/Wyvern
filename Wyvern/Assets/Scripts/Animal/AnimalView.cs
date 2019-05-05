using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalView : MonoBehaviour
{
    [SerializeField] private AnimalData m_animalData;

    private AnimalFOV m_animalFOV;

    public AnimalData AnimalData { get { return m_animalData; } }


    private void Start()
    {
        m_animalFOV = this.gameObject.AddComponent<AnimalFOV>();
    }
}
