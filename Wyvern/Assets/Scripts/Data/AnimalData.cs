using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Data/Animal", order = 3)]
public class AnimalData : ScriptableObject
{
    public string animalName;

    public Vector3 animalFieldOfViewOffset;
    public Vector2 animalFieldOfView;

    [Header("Awareness")]
    public float awarenessFactor = 1; //Determines how easy the animal is to spook
}
