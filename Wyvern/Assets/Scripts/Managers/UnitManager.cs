using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitManager : MonoBehaviour
{
    public Action<Vector3> UnitPositionUpdated;

    public void UpdateUnitPosition(Vector3 newUnitPosition)
    {
        if (UnitPositionUpdated != null)
            UnitPositionUpdated.Invoke(newUnitPosition);
    }
}
