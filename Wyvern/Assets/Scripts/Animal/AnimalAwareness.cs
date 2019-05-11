using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimalAwareness : AnimalComponent
{
    public Action<float> AnimalAwarenessChanged;

    private float awarenessMeter = 0;

    public void AnimalSpottedUnit(float distanceToUnit)
    {
        distanceToUnit = Mathf.Round(distanceToUnit);

        //Take into account the further away the unit is the less it causes spoopiness
        float awareness = 1 / distanceToUnit;
        float awarenessMeterIncrease = awareness * m_animalView.AnimalData.awarenessFactor;
        
        awarenessMeter += awarenessMeterIncrease;       

        if (AnimalAwarenessChanged != null)
            AnimalAwarenessChanged.Invoke(awarenessMeter);
    }
}
