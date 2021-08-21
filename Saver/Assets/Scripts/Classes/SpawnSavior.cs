using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSavior : SpawnUnits
{
    public SpawnSavior()
    {
        SpawnSaviorObject();
    }
    public void SpawnSaviorObject()
    {
        var screenBottomCenter = new Vector3(Display.main.systemWidth / 2, Display.main.systemHeight / 10, 5);
        var toWorldPosition = Camera.main.ScreenToWorldPoint(screenBottomCenter);
        var SaviorObject = MonoBehaviour.Instantiate(Resources.Load("Savior"), toWorldPosition, Quaternion.identity) as GameObject;
        SaviorObject.transform.parent = GameObject.Find("Saviors").transform;
    }
}
