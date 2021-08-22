using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaviorClass
{
    public bool IsRayHitToUnit, IsSaviorHitToRescuable, IsSaviorHitTo_Not_Rescuable;
    public Vector3 SaviorPosition;
    public SaviorClass(SaviorObject savior)
    {
        IsRayHitToUnit = savior.GetIsRayHitToUnits();
        IsSaviorHitToRescuable = savior.GetIsSaviorHitToRescuable();
        IsSaviorHitTo_Not_Rescuable = savior.GetIsSaviorHitTo_Not_Rescuable();
        SaviorPosition = savior.GetSaviorPosition();
    }
}
