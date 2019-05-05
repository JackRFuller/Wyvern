using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public Action NewPlayerTurn;

    private void Start()
    {
        
    }

    public void StartNewPlayerTurn()
    {
        if (NewPlayerTurn != null)
            NewPlayerTurn.Invoke();
    }
}
