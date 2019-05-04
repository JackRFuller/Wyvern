using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTargetMarker : MonoBehaviour
{
    private SpriteRenderer m_targetLocationSprite;
    private LineRenderer m_targetPath;
    private Material m_targetPathMaterial;

    // Start is called before the first frame update
    void Start()
    {
        m_targetLocationSprite = GetComponentInChildren<SpriteRenderer>();
        m_targetPath = GetComponentInChildren<LineRenderer>();
        m_targetPathMaterial = m_targetPath.material;

        StartCoroutine(TurnOffTargetMarker());
    }      

    public void TurnOffMovementTargetMarker()
    {
        StartCoroutine(TurnOffTargetMarker());
    }

    IEnumerator TurnOffTargetMarker()
    {
        yield return new WaitForEndOfFrame();

        m_targetLocationSprite.enabled = false;
        m_targetPath.enabled = false;
    }

    public void SetMovementTargetPath(Vector3 startingPoint, Vector3[] wayPoints)
    {
        m_targetPath.positionCount = wayPoints.Length + 1;

        m_targetPath.SetPosition(0, startingPoint);

        for (int i = 0; i < wayPoints.Length; i++)
        {
            m_targetPath.SetPosition(i + 1, wayPoints[i]);
        }

        Vector3 targetLocation = new Vector3(wayPoints[wayPoints.Length - 1].x,
                                             0.01f,
                                             wayPoints[wayPoints.Length - 1].z);

        m_targetLocationSprite.transform.position = targetLocation;

        if (!m_targetLocationSprite.enabled)
            m_targetLocationSprite.enabled = true;

        if (!m_targetPath.enabled)
            m_targetPath.enabled = true;

    }
}
