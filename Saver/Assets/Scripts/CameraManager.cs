using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 _initialPositionOfCamera;
    private Quaternion _initialRotationOfCamera;
   
    private void Start()
    {
        _initialPositionOfCamera = transform.position;
        _initialRotationOfCamera = transform.rotation;
    }
    private void FixedUpdate()
    {
        if (GameManager.Savior.IsCameraFollowing)
        {
            FollowTheSavior();
        }
        else
        {
            ReturnToInitialPositionAndRotation();
        }
    }
    private void FollowTheSavior()
    {
        var newFollowPosition = new Vector3(GameManager.Savior.SaviorPosition.x, GameManager.Savior.SaviorPosition.y + 5, GameManager.Savior.SaviorPosition.z - 2);
        transform.position = Vector3.Lerp(gameObject.transform.position, newFollowPosition, 0.1f);
    }
    private void ReturnToInitialPositionAndRotation()
    {
        transform.position = Vector3.Lerp(transform.position, _initialPositionOfCamera, 0.1f);
        transform.rotation = _initialRotationOfCamera;
    }
}
