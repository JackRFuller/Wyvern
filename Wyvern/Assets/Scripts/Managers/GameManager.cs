using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private ActionTiles m_actionTiles;
    private TurnManager m_turnManager;
    private UnitManager m_unitManager;

    public ActionTiles ActionTiles { get { return m_actionTiles; } }
    public TurnManager TurnManager { get { return m_turnManager; } }
    public UnitManager UnitManager { get { return m_unitManager; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        m_actionTiles = this.GetComponentInChildren<ActionTiles>();
        m_turnManager = this.gameObject.AddComponent<TurnManager>();
        m_unitManager = this.gameObject.AddComponent<UnitManager>();
    }
}
