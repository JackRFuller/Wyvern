using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalComponent : MonoBehaviour
{
    protected AnimalView m_animalView;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_animalView = this.GetComponent<AnimalView>();
    }
}
