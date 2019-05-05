using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTiles : MonoBehaviour
{
    [SerializeField] private GameObject pathFindingTilePrefab;
    
    private GameObject[,] m_tileGrid;
    private UnitView m_unitView;

    private List<GameObject> m_activatedTiles = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        CreateActionTiles();
    }

    private void CreateActionTiles()
    {       
        m_tileGrid = new GameObject[8, 8];

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject tile = Instantiate(pathFindingTilePrefab, Vector3.zero, Quaternion.identity);
                tile.name = $"Tile {x},{y}";

                tile.transform.parent = this.transform;

                Vector3 localPositon = new Vector3(x, 0, y);
                tile.transform.localPosition = localPositon;

                m_tileGrid[x, y] = tile;
                tile.SetActive(false);
            }
        }
    }

    //If We're not moving we assume we're attacking
    public void ShowActionTiles(UnitAction unitAction)
    {
        m_unitView = unitAction.unit;

        HideActionTiles(m_unitView);

        Vector3 unitPosition = unitAction.unit.transform.position;
        
        m_activatedTiles.Clear();

        int unitX = Mathf.RoundToInt(unitPosition.x);
        int unitY = Mathf.RoundToInt(unitPosition.z);

        int actionRange = unitAction.isMoving ? m_unitView.UnitData.movementRange : m_unitView.UnitData.weapon.weaponRange;
               
        for (int x = -actionRange; x <= actionRange; x++)
        {
            for (int y = -actionRange; y <= actionRange; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                if (Mathf.Abs(x) + Mathf.Abs(y) > actionRange)
                    continue;

                int xPos = unitX + x;
                int yPos = unitY + y;

                Vector3 worldPoint = new Vector3(xPos, 0, yPos);

                if(IsPositionWithinBoard(xPos,yPos))
                {
                    bool actionable = !(Physics.CheckSphere(worldPoint, 0.4f, unitAction.collisionMask));

                    if(unitAction.isMoving)
                    {
                        if (actionable)
                        {
                            PathRequestManager.RequestPath(unitPosition, new Vector3(xPos, 0, yPos), OnPathFound);
                        }
                    }
                    else if (!unitAction.isMoving)
                    {
                        actionable = !actionable; //Reverse collision bool as we're looking for tiles with animals

                        if (actionable)
                        {
                            m_tileGrid[xPos, yPos].SetActive(true);
                            m_activatedTiles.Add(m_tileGrid[xPos, yPos]);
                        }
                    }
                }
            }
        }
    }

    private bool IsPositionWithinBoard(int xPosition, int yPosition)
    {
        if (xPosition >= 0 && xPosition < 8 && yPosition >= 0 && yPosition < 8)
            return true;
        else
            return false;
    }    

    private void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful && newPath.Length <= m_unitView.UnitData.movementRange)
        {
            int xPos = (int)newPath[newPath.Length - 1].x;
            int yPos = (int)newPath[newPath.Length - 1].z;

            m_tileGrid[xPos, yPos].SetActive(true);
            m_activatedTiles.Add(m_tileGrid[xPos, yPos]);
        }
    }

    public void HideActionTiles(UnitView unitView)
    {   
        if(m_unitView == unitView)
        {
            for (int i = 0; i < m_activatedTiles.Count; i++)
            {
                m_activatedTiles[i].SetActive(false);
            }
        }
    }
}

public class UnitAction
{
    public UnitView unit;
    public bool isMoving; //If We're Not Moving Assume We're Attacking
    public LayerMask collisionMask;

    public UnitAction(UnitView _unit, bool _isMoving, LayerMask _collisionMask)
    {
        unit = _unit;
        isMoving = _isMoving;
        collisionMask = _collisionMask;
    }
}
