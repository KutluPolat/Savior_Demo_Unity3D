using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform _initialTransformOfCamera;
    public static SaviorClass Savior;
    private void Start()
    {
        _initialTransformOfCamera = Camera.main.transform;
        transform.LookAt(GameObject.Find("SaviorClone").transform.position);
    }

    private void Update()
    {
        Savior = new SaviorClass(GameObject.FindObjectOfType<SaviorObject>());
        Debug.Log("Savior hit: " + Savior.IsRayHitToUnit);
        Debug.Log("Savior hit to resc: " + Savior.IsSaviorHitToRescuable);
        Debug.Log("Savior hit to not resc: " + Savior.IsSaviorHitTo_Not_Rescuable);
    }
    private void FixedUpdate()
    {
        FollowTheSavior();
    }
    private void FollowTheSavior()
    {
        if (Savior.IsRayHitToUnit)
        {
            var newFollowPosition = new Vector3(Savior.SaviorPosition.x, Savior.SaviorPosition.y + 5, Savior.SaviorPosition.z - 2);
            transform.position = Vector3.Lerp(gameObject.transform.position, newFollowPosition, 0.1f);
        }
    }
}
