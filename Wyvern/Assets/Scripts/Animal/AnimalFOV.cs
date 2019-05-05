using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Determines if character is inside animal's field of view
public class AnimalFOV : AnimalComponent
{
    protected override void Start()
    {
        base.Start();

        GameManager.Instance.UnitManager.UnitPositionUpdated += CheckIfUnitIsInsideFOV;
    }

    private void CheckIfUnitIsInsideFOV(Vector3 unitPosition)
    {
        int fovX = 0;
        int fovY = 0;

        if (Mathf.Abs(transform.eulerAngles.y) == 90 || Mathf.Abs(transform.eulerAngles.y) == 180)
        {
            fovX = (int)m_animalView.AnimalData.animalFieldOfView.y;
            fovY = (int)m_animalView.AnimalData.animalFieldOfView.x;
        }
        else
        {
            fovX = (int)m_animalView.AnimalData.animalFieldOfView.x;
            fovY = (int)m_animalView.AnimalData.animalFieldOfView.y;
        } 
        //Check if Unit is Inside Field of View



        for(int x = -fovX; x <= fovX; x++)
        {
            for(int y = 0; y <= fovY; y++)
            {
                //Check if we have unbroken line of sight.
                //Vector3 lineOfSightPosition = 
            }
        }
    }
}
