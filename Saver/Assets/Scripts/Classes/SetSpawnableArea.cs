using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnableArea
{
    protected float _xPositionMinimum, _xPositionMaximum, _zPositionMinimum, _zPositionMaximum;
    private GameObject _leftTopPoint, _rightBottomPoint;
    public SetSpawnableArea()
    {
        CheckPlaneSize();
    }

    private void SetCurrentPlane()
    {
        _leftTopPoint = GameObject.Find("LeftTop");
        _rightBottomPoint = GameObject.Find("RightBottom");

        _xPositionMinimum = _leftTopPoint.transform.position.x;
        _xPositionMaximum = _rightBottomPoint.transform.position.x;

        _zPositionMinimum = _rightBottomPoint.transform.position.z;
        _zPositionMaximum = _leftTopPoint.transform.position.z;
    }
    
    private void CheckPlaneSize()
    {
        // If xbound*zbound is less than 60% of the unit count, the plane will get bigger.
        for (int i = 0; i < 99; i++) //Checking multiple time
        {
            SetCurrentPlane();

            var xBound = (int)Mathf.Abs(_xPositionMaximum - _xPositionMinimum);
            var zBound = (int)Mathf.Abs(_zPositionMaximum - _zPositionMinimum);

            if (xBound * zBound * 0.6f < PlayerPrefs.GetInt("Level") * 5) // Because there will be 5 times as many units as the level.
            {
                var transformOfGreyPlane = GameObject.Find("Plane(Grey)").transform;

                transformOfGreyPlane.localScale = GameObject.Find("Plane(Grey)").transform.localScale * 1.03f;
                transformOfGreyPlane.position = new Vector3(transformOfGreyPlane.position.x, 0, transformOfGreyPlane.position.z);

                // While plane is getting bigger, camera has to see wider space so I basically raise the camera.
                var cameraYPosition = Camera.main.transform.position.y + 1;
                var cameraZPosition = GameObject.Find("Plane(Grey)").GetComponent<MeshRenderer>().bounds.min.z;
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, cameraYPosition, cameraZPosition);
            }
        }
        
    }
}
