using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    private const int m_levelWidth = 8;
    private const int m_levelHeight = 8;

    private static GameObject[,] m_levelTiles;
    public static GameObject[,] LevelTiles { get { return m_levelTiles; } }

    private void Awake()
    {
        GenerateLevel();
    }


    #region LevelGeneratorMethods
    public void GenerateLevel()
    {
        m_levelTiles = new GameObject[m_levelWidth, m_levelHeight];

        for (int x = 0; x < m_levelWidth; x++)
        {
            for (int y = 0; y < m_levelHeight; y++)
            {
                GameObject tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                tile.name = $"Tile {x},{y}";

                tile.transform.parent = this.transform;
                Vector3 localPositon = new Vector3(x, 0, y);
                tile.transform.localPosition = localPositon;

                m_levelTiles[x, y] = tile;
            }
        }
    }
    #endregion

}
