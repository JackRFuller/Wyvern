using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitMovement : UnitComponent
{
    public Action UnitMoving;
    public Action UnitStoppedMoving;

    private Transform m_unitMesh;

    private const float speed = 1;
    Vector3[] path;
    int targetIndex;

    private bool m_hasUnitMoved = false;
    public bool HasMoved { get { return m_hasUnitMoved; } }

    protected override void Start()
    {
        base.Start();
        this.enabled = false;

        GameManager.Instance.TurnManager.NewPlayerTurn += ResetTurnAttributes;
        
        m_unitMesh = transform.GetChild(0);
    }

    private void Update()
    {
        ChooseMoveTile();

        CancelMovement();
    }

    public void MovementInit()
    {
        if (!m_hasUnitMoved)
        {
            UnitAction unitAction = new UnitAction(m_unitView, true, m_unitView.UnitData.unwalkableTiles);
            GameManager.Instance.ActionTiles.ShowActionTiles(unitAction);
            m_unitView.SetUnitActionState(true);

            this.enabled = true;
        }        
    }  

    private void CancelMovement()
    {
        if(!m_hasUnitMoved)
        {
            if(Input.GetMouseButton(1))
            {
                this.enabled = false;

                PlayerInteraction.MovementTargetMarker.TurnOffMovementTargetMarker();
                GameManager.Instance.ActionTiles.HideActionTiles(m_unitView);
                m_unitView.SetUnitActionState(false);

                if (UnitStoppedMoving != null)
                    UnitStoppedMoving.Invoke();
            }                  
        }
    }

    private void ChooseMoveTile()
    {
        Ray ray = PlayerInteraction.PlayerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("ActionTile"))
            {              
                Vector3 startPosition = transform.position;
                Vector3 targetPosition = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);

                PathRequestManager.RequestPath(startPosition, targetPosition, OnPathFound);

                if (Input.GetMouseButtonDown(0))
                {
                    StopCoroutine("FollowPath");

                    StartCoroutine("FollowPath");

                    this.enabled = false;
                }
            }
        }
    }

    private void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            PlayerInteraction.MovementTargetMarker.SetMovementTargetPath(transform.localPosition,newPath);
        }
    }

    IEnumerator FollowPath()
    {
        if (UnitMoving != null)
            UnitMoving.Invoke();

        Vector3 lastPosition = transform.position;

        m_hasUnitMoved = true;
        Vector3 currentWaypoint = path[0];        

        targetIndex = 0;

        UpdateMeshRotation(currentWaypoint);

        for(int i =0; i < path.Length;i++)
        {
            path[i].y = 0;
        }

        while (true)
        {
            if (HasUnitMovedAWholeSquare(lastPosition) || targetIndex >= path.Length)
            {
                lastPosition = transform.position;                
                GameManager.Instance.UnitManager.UpdateUnitPosition(transform.position);
            }

            if(transform.position == currentWaypoint)
            {             
                targetIndex++;

                if(targetIndex >= path.Length)
                {
                    StopMoving();                  
                    yield break;
                }

                currentWaypoint = path[targetIndex];

                UpdateMeshRotation(currentWaypoint);
            }           

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            yield return null;
        }
    } 

    private bool HasUnitMovedAWholeSquare(Vector3 lastPosition)
    { 
        if (transform.position.x >= lastPosition.x + 1 || transform.position.x <= lastPosition.x - 1)        
            return true;

        if (transform.position.z >= lastPosition.z + 1 || transform.position.z <= lastPosition.z - 1)
            return true;

        return false;

    }

    private void StopMoving()
    {
        if (UnitStoppedMoving != null)
            UnitStoppedMoving.Invoke();

        GameManager.Instance.ActionTiles.HideActionTiles(m_unitView);
        PlayerInteraction.MovementTargetMarker.TurnOffMovementTargetMarker();
        m_unitView.SetUnitActionState(false);

        this.enabled = false;
    }

    private void UpdateMeshRotation(Vector3 currentWaypoint)
    {
        if(currentWaypoint - transform.position != Vector3.zero)
            m_unitMesh.rotation = Quaternion.LookRotation(currentWaypoint - transform.position);
    }

    private void ResetTurnAttributes()
    {
        m_hasUnitMoved = false;
    }
}
