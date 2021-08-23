using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnableArea
{
    protected float _xPositionMinimum, _xPositionMaximum, _zPositionMinimum, _zPositionMaximum;
    private GameObject _leftTopPoint, _rightBottomPoint;
    public SetSpawnableArea()
    {
        _leftTopPoint = GameObject.Find("LeftTop");
        _rightBottomPoint = GameObject.Find("RightBottom");

        _xPositionMinimum = _leftTopPoint.transform.position.x;
        _xPositionMaximum = _rightBottomPoint.transform.position.x;

        _zPositionMinimum = _rightBottomPoint.transform.position.z;
        _zPositionMaximum = _leftTopPoint.transform.position.z;
    }
}
