using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : SetSpawnableArea
{
    private int _numberOfRescuableUnits, _numberOfNotRescuableUnits;
    private List<(int, int)> _coordinatesOfUnits = new List<(int, int)>();
    public List<GameObject> RescuableUnits = new List<GameObject>();

    public SpawnUnits()
    {
        _numberOfRescuableUnits = PlayerPrefs.GetInt("Level", 1) * 3; //With these equations, there will be 60% of Rescuable and 40% of NotRescuable units in every level.
        _numberOfNotRescuableUnits = PlayerPrefs.GetInt("Level", 1) * 2;

        for(int i = 0; i < _numberOfRescuableUnits; i++)
        {
        tryagain:

            int xPosition = (int)Random.Range(_xPositionMinimum, _xPositionMaximum); //I used casting for converting Random.Range float value to int.
            int zPosition = (int)Random.Range(_zPositionMinimum, _zPositionMaximum);

            if (CheckForOverlap(xPosition, zPosition))
            {
                goto tryagain;
            }
            else
            {
                _coordinatesOfUnits.Add((xPosition, zPosition)); //Adding used positions to _coordinatesOfUnits list so I can check overlapping later.
                var rescuableObject = MonoBehaviour.Instantiate(Resources.Load("Rescuable"), new Vector3(xPosition, 0.7f, zPosition), Quaternion.Euler(0, 180, 0)) as GameObject;
                rescuableObject.transform.parent = GameObject.Find("Rescuables").transform;
                RescuableUnits.Add(rescuableObject);
            }
        }

        for (int i = 0; i < _numberOfNotRescuableUnits; i++)
        {
        tryagain:

            int xPosition = (int)Random.Range(_xPositionMinimum, _xPositionMaximum);
            int zPosition = (int)Random.Range(_zPositionMinimum, _zPositionMaximum);

            if (CheckForOverlap(xPosition, zPosition))
            {
                goto tryagain;
            }
            else
            {
                _coordinatesOfUnits.Add((xPosition, zPosition));
                var notRescuableObject = MonoBehaviour.Instantiate(Resources.Load("NotRescuable"), new Vector3(xPosition, 0.7f, zPosition), Quaternion.Euler(0, 180, 0)) as GameObject;
                notRescuableObject.transform.parent = GameObject.Find("NotRescuables").transform;
            }
        }
    }

    private bool CheckForOverlap(int x, int z)
    {
        for (int i = 0; i < _coordinatesOfUnits.Count; i++)
        {
            if(_coordinatesOfUnits[i].Item1 == x && _coordinatesOfUnits[i].Item2 == z)
            {
                return true;
            }
        }

        return false;
    }
}
