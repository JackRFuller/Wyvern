using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTiles : MonoBehaviour
{
    [SerializeField] private GameObject pathFindingTilePrefab;
    
    private static GameObject[,] m_tileGrid;
    private static UnitView m_unitView;

    private static List<GameObject> m_activatedTiles = new List<GameObject>();

    [SerializeField] private LayerMask unWalkableTileMask;

    // Start is called before the first frame update
    void Start()
    {
        CreatePathfindingTiles();

        PlayerInteraction.SelectedUnit += ShowPotenitalMovementTiles;
        PlayerInteraction.DeselectedUnit += HidePotentialMovementTiles;
    }

    private void CreatePathfindingTiles()
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

    public void ShowPotenitalMovementTiles(UnitView selectedUnit)
    {
        if(!selectedUnit.UnitMovement.HasMoved)
        {
            m_unitView = selectedUnit;
            m_unitView.UnitMovement.UnitStoppedMoving += HidePotentialMovementTiles;

            Vector3 unitPosition = selectedUnit.transform.position;
            int movementCount = selectedUnit.UnitData.movementRange;

            m_activatedTiles.Clear();

            int unitX = Mathf.RoundToInt(unitPosition.x);
            int unitY = Mathf.RoundToInt(unitPosition.z);

            for (int x = -movementCount; x <= movementCount; x++)
            {
                for (int y = -movementCount; y <= movementCount; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    if (Mathf.Abs(x) + Mathf.Abs(y) > movementCount)
                        continue;

                    int xPos = unitX + x;
                    int yPos = unitY + y;

                    //Vector3 tilePosition = new Vector3(xPos, 0.5f, yPos);

                    //bool walkable = !(Physics.CheckSphere(tilePosition, 0.5f, unWalkableTileMask));

                    if (xPos >= 0 && xPos < 8 && yPos >= 0 && yPos < 8)
                    {
                        m_tileGrid[xPos, yPos].SetActive(true);
                        m_activatedTiles.Add(m_tileGrid[xPos, yPos]);
                    }

                    //if (walkable)
                    //{
                        
                    //}
                }
            }
        }
    }

    public static void HidePotentialMovementTiles()
    {
        if(m_unitView != null)
            m_unitView.UnitMovement.UnitStoppedMoving -= HidePotentialMovementTiles;

        for (int i = 0; i < m_activatedTiles.Count; i++)
        {
            m_activatedTiles[i].SetActive(false);
        }
    }
}
