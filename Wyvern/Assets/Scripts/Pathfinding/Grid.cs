using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private bool showGrid = false;
    [SerializeField] private Vector2 m_gridWorldSize;
    [SerializeField] private float m_nodeRadius;
    [SerializeField] private LayerMask m_unwalkableMask;

    private Node[,] grid;
    private float m_nodeDiameter;
    private int m_gridSizeX, m_gridSizeY;

    #region UnityMethods

    private void Awake()
    {
        m_nodeDiameter = m_nodeRadius * 2;
        m_gridSizeX = Mathf.RoundToInt(m_gridWorldSize.x / m_nodeDiameter);
        m_gridSizeY = Mathf.RoundToInt(m_gridWorldSize.y / m_nodeDiameter);

        CreateGrid();
    }

    public List<Node> path;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(m_gridWorldSize.x, 1, m_gridWorldSize.y));

        if(grid != null && showGrid)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = n.walkable ? Color.white : Color.red; 
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (m_nodeDiameter - .1f));
            }
        }
    }

    #endregion

    private void CreateGrid()
    {
        grid = new Node[m_gridSizeX, m_gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * m_gridWorldSize.x / 2 - Vector3.forward * m_gridWorldSize.y / 2;

        for (int x = 0; x < m_gridSizeX; x++)
        {
            for (int y = 0; y < m_gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * m_nodeDiameter + m_nodeRadius) + Vector3.forward * (y * m_nodeDiameter + m_nodeRadius);

                bool walkable = !(Physics.CheckSphere(worldPoint, m_nodeRadius,m_unwalkableMask));     
                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float posX = ((worldPosition.x - transform.position.x) + m_gridWorldSize.x * 0.5f) / m_nodeDiameter;
        float posY = ((worldPosition.z - transform.position.z) + m_gridWorldSize.y * 0.5f) / m_nodeDiameter;

        posX = Mathf.Clamp(posX, 0, m_gridWorldSize.x - 1);
        posY = Mathf.Clamp(posY, 0, m_gridWorldSize.y - 1);

        int x = Mathf.FloorToInt(posX);
        int y = Mathf.FloorToInt(posY);

        return grid[x, y];
    }    

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighBours = new List<Node>();

        List<Vector2> nsew = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };

        foreach(Vector2 direction in nsew)
        {
            int checkX = node.gridX + (int)direction.x;
            int checkY = node.gridY + (int)direction.y;

            if (checkX >= 0 && checkX < m_gridSizeX && checkY >= 0 && checkY < m_gridSizeY)
            {
                neighBours.Add(grid[checkX, checkY]);
            }
        }      

        return neighBours;
    }


}
