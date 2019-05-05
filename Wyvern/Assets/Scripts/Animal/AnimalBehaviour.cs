using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Determines what state the animal is in and how they should behave for that turn
public class AnimalBehaviour : MonoBehaviour
{
    private AnimalState m_animalState;

    public enum AnimalState
    {
        Idle,
        Cautious,
        Alert,
    }
}
