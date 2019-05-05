using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Data/Animal", order = 3)]
public class AnimalData : ScriptableObject
{
    public string animalName;

    public Vector2 animalFieldOfView;
}
