using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSavior : SpawnUnits
{
    private Vector3 _initialPlayerSpawnPoint;
    public SpawnSavior()
    {
        var screenBottomCenter = new Vector3(Display.main.systemWidth / 2, Display.main.systemHeight / 10, 5);
        _initialPlayerSpawnPoint = Camera.main.ScreenToWorldPoint(screenBottomCenter);
        SpawnSaviorObject();
    }
    public void SpawnSaviorObject()
    {
        var SaviorObject = MonoBehaviour.Instantiate(Resources.Load("Savior"), _initialPlayerSpawnPoint, Quaternion.identity) as GameObject;
        SaviorObject.transform.parent = GameObject.Find("Saviors").transform;
    }
}
