using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 _targetPositionToDestroyOfCamera;
    private Quaternion _initialRotationOfCamera;

    public MeshRenderer GreyPlaneMeshRenderer, RightBottomObjectMeshRenderer;
    private float _greyPlaneMinimumZPosition, _greyPlaneCenterXPosition;

    private void Start()
    {
        _targetPositionToDestroyOfCamera = transform.position;
        _initialRotationOfCamera = transform.rotation;

        _greyPlaneMinimumZPosition = GreyPlaneMeshRenderer.bounds.min.z;
        _greyPlaneCenterXPosition = GreyPlaneMeshRenderer.bounds.center.x;

        gameObject.transform.position = new Vector3(_greyPlaneCenterXPosition, transform.position.y, _greyPlaneMinimumZPosition);
    }

    private void FixedUpdate()
    {
        if (!RightBottomObjectMeshRenderer.isVisible)
        {
            // There is an object right bottom at the grey plane. If that object is invisible, I raise camera higher.
            // With that, Player will always see the whole plane.
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            gameObject.transform.position = new Vector3(_greyPlaneCenterXPosition, transform.position.y, _greyPlaneMinimumZPosition);
        }

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
        transform.position = Vector3.Lerp(transform.position, _targetPositionToDestroyOfCamera, 0.1f);
        transform.rotation = _initialRotationOfCamera;
    }
}
