using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaviorClass
{
    public bool IsRayHitToUnit, IsSaviorHitToRescuable, IsSaviorHitTo_Not_Rescuable, IsCameraFollowing;
    public Vector3 SaviorPosition;
    public GameObject Rescuable;
    public int Lives;
    SaviorObject Savior;
    public SaviorClass(SaviorObject savior)
    {
        Savior = savior;
        IsRayHitToUnit = savior.GetIsRayHitToUnits();
        IsSaviorHitToRescuable = savior.GetIsSaviorHitToRescuable();
        IsSaviorHitTo_Not_Rescuable = savior.GetIsSaviorHitTo_Not_Rescuable();
        IsCameraFollowing = savior.GetIsCameraFollowing();
        SaviorPosition = savior.GetSaviorPosition();
        Lives = savior.GetLives();
        Rescuable = savior.GetRescuable();
    }

    public bool HitToRescuable
    {
        get { return IsSaviorHitToRescuable; }
        set 
        { 
            Savior.SetHitToRescuable(value);
        }
    }

    public bool HitToNotRescuable
    {
        get { return IsSaviorHitTo_Not_Rescuable; }
        set
        {
            Savior.SetHitToNotRescuable(value);
        }
    }
}
