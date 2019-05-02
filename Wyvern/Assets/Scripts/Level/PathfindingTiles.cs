using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTiles : MonoBehaviour
{
    [SerializeField] private GameObject pathFindingTilePrefab;
    
    private static GameObject[,] m_tileGrid;

    // Start is called before the first frame update
    void Start()
    {
        CreatePathfindingTiles();
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

    public static void ShowPotenitalMovementTiles(Vector3 unitPosition, int movementCount)
    {
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

                if (xPos >= 0 && xPos < 8 && yPos >= 0 && yPos < 8)
                {
                    m_tileGrid[xPos, yPos].SetActive(true);
                }
            }
        }
    }
}
