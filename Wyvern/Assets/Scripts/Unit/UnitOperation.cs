using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOperation : UnitComponent
{
    protected bool m_hasPerformedOperation;
    
    private void ResetOperationUponNewTurn()
    {
        m_hasPerformedOperation = false;
    }
}
