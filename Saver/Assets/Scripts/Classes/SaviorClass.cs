using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaviorClass
{
    public bool IsRayHitToUnit, IsSaviorHitToRescuable, IsSaviorHitTo_Not_Rescuable, IsCameraFollowing;
    public Vector3 SaviorPosition;
    public int Lives;
    public SaviorClass(SaviorObject savior)
    {
        IsRayHitToUnit = savior.GetIsRayHitToUnits();
        IsSaviorHitToRescuable = savior.GetIsSaviorHitToRescuable();
        IsSaviorHitTo_Not_Rescuable = savior.GetIsSaviorHitTo_Not_Rescuable();
        IsCameraFollowing = savior.GetIsCameraFollowing();
        SaviorPosition = savior.GetSaviorPosition();
        Lives = savior.GetLives();
    }
}
