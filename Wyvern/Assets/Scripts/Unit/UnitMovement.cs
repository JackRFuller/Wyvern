using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : UnitComponent
{
    private const float speed = 1;
    Vector3[] path;
    int targetIndex;

    protected override void Start()
    {
        base.Start();
        this.enabled = false;

       
    }

    private void Update()
    {
        ChooseMoveTile();
    }

    public void OnClickMoveUnit()
    {
        //Show Tiles We Can Move To
        PathfindingTiles.ShowPotenitalMovementTiles(this.transform.position,m_unitView.UnitData.movementRange);

        //Enable Mouse Click To Pick Moveable Tile
        this.enabled = true;
    }

    private void ChooseMoveTile()
    {
        Ray ray = PlayerInteraction.PlayerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("ActionTile"))
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.transform.position);

                    Vector3 startPosition = transform.position;
                    Vector3 targetPosition = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);

                    PathRequestManager.RequestPath(startPosition, targetPosition, OnPathFound);
                }
                
            }
        }
    }

    private void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");

            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        targetIndex = 0;

        while (true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;

                if(targetIndex >= path.Length)
                {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
