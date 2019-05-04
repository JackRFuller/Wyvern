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

        PlayerInteraction.SelectedUnit += MovementInit;

        m_unitMesh = transform.GetChild(0);
    }

    private void Update()
    {
        ChooseMoveTile();
    }

    private void MovementInit(UnitView unitView)
    {
        if(unitView == m_unitView)
        {
            if (!m_hasUnitMoved)
            {                
                //Enable Mouse Click To Pick Moveable Tile
                this.enabled = true;
            }
        }
    }  

    private void CancelMovement()
    {
        if(!m_hasUnitMoved)
        {
            if (this.enabled)
            {
                this.enabled = false;
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
            PlayerInteraction.MovementTargetMarker.SetMovementTargetPath(newPath);
        }
    }

    IEnumerator FollowPath()
    {
        if (UnitMoving != null)
            UnitMoving.Invoke();

        Vector3 currentWaypoint = path[0];        
        targetIndex = 0;

        UpdateMeshRotation(currentWaypoint);

        for(int i =0; i < path.Length;i++)
        {
            path[i].y = 0;
        }

        while (true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;

                if(targetIndex >= path.Length)
                {
                    StopMoving();

                    yield break;
                }
              
                currentWaypoint = new Vector3(path[targetIndex].x,0,path[targetIndex].z);

                UpdateMeshRotation(currentWaypoint);
            }           

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            yield return null;
        }
    }

    private void StopMoving()
    {
        if (UnitStoppedMoving != null)
            UnitStoppedMoving.Invoke();

        PlayerInteraction.MovementTargetMarker.TurnOffMovementTargetMarker();

        m_hasUnitMoved = true;
        this.enabled = false;
    }

    private void UpdateMeshRotation(Vector3 currentWaypoint)
    {
        m_unitMesh.rotation = Quaternion.LookRotation(currentWaypoint - transform.position);
    }
}
