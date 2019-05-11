using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Determines what state the animal is in and how they should behave for that turn
public class AnimalBehaviour : AnimalComponent
{
    private AnimalState m_animalState;

    public enum AnimalState
    {
        Idle,
        Cautious,
        Alert,
    }

    protected override void Start()
    {
        base.Start();

        m_animalView.AnimalAwareness.AnimalAwarenessChanged += SetAnimalState;
    }

    private void SetAnimalState(float animalAwareness)
    {
        Debug.Log(animalAwareness);

        if (animalAwareness < 5)
            m_animalState = AnimalState.Idle;
        else if (animalAwareness >= 5 && animalAwareness < 10)
            m_animalState = AnimalState.Cautious;
        else if (animalAwareness >= 10)
            m_animalState = AnimalState.Alert;

        Debug.Log(m_animalState);
    }
}
