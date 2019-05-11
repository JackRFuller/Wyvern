using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Determines if character is inside animal's field of view
public class AnimalFOV : AnimalComponent
{    
    private Bounds m_fovBounds;
    private Transform m_animalMesh;

    protected override void Start()
    {
        base.Start();

        m_animalMesh = m_animalView.AnimalMesh.transform;
      
        GameManager.Instance.UnitManager.UnitPositionUpdated += CheckIfUnitIsInsideFOV;

        CreateFOVBounds();
    }

    private void CreateFOVBounds()
    {
        Vector3 fovSize = Vector3.one;
        Vector3 fovOffset = Vector3.zero;

        float animalRotation = m_animalMesh.eulerAngles.y;

        if (animalRotation == 0 || animalRotation == 180)       
            fovSize = new Vector3(m_animalView.AnimalData.animalFieldOfView.x, 1, m_animalView.AnimalData.animalFieldOfView.y);

        if(animalRotation == 90 || animalRotation == 270)
            fovSize = new Vector3(m_animalView.AnimalData.animalFieldOfView.y, 1, m_animalView.AnimalData.animalFieldOfView.x);

        float xOffset = 0;
        float zOffset = 0;
                
        if(animalRotation == 0)
        {
            xOffset = transform.position.x + m_animalView.AnimalData.animalFieldOfViewOffset.x;
            zOffset = transform.position.z + m_animalView.AnimalData.animalFieldOfViewOffset.z;
        }
        else if(animalRotation == 180)
        {
            xOffset = transform.position.x + -m_animalView.AnimalData.animalFieldOfViewOffset.x;
            zOffset = transform.position.z + -m_animalView.AnimalData.animalFieldOfViewOffset.z;
        }
        else if(animalRotation == 90)
        {
            xOffset = transform.position.x + m_animalView.AnimalData.animalFieldOfViewOffset.z;
            zOffset = transform.position.z + m_animalView.AnimalData.animalFieldOfViewOffset.x;
        }
        else if (animalRotation == 270)
        {
            xOffset = transform.position.x + -m_animalView.AnimalData.animalFieldOfViewOffset.z;
            zOffset = transform.position.z + -m_animalView.AnimalData.animalFieldOfViewOffset.x;
        }

        fovOffset = new Vector3(xOffset, 0, zOffset);
        m_fovBounds = new Bounds(fovOffset, fovSize);
    }

    

    private void CheckIfUnitIsInsideFOV(Vector3 unitPosition)
    {   
        if (m_fovBounds.Contains(unitPosition))
        {
            //Check if we have line of sight
            Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Vector3 rayDirection = unitPosition - transform.position;

            Ray ray = new Ray(rayOrigin, rayDirection);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction, Color.blue, 5f);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.collider.CompareTag("Unit"))
                {
                    //Work Out Distance To Unit
                    float distance = Vector3.Distance(unitPosition, transform.position);
                    m_animalView.AnimalAwareness.AnimalSpottedUnit(distance);
                }
            }            

            
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(m_fovBounds.center, m_fovBounds.size);
    }

    public float GetObjectZWorldRotation(Transform objectRot)
    {
        float rotation = Mathf.Abs(Quaternion.Angle(objectRot.rotation, Quaternion.identity));       
        rotation = Mathf.Round(rotation);
        return rotation;
    }

    float AngleAboutY(Transform obj)
    {
        Vector3 objFwd = obj.forward;
        float angle = Vector3.Angle(objFwd, Vector3.forward);
        float sign = Mathf.Sign(Vector3.Cross(objFwd, Vector3.forward).y);
        return angle * sign;
    }
} 
