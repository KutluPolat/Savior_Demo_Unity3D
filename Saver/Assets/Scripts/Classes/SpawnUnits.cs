using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : SetSpawnableArea
{
    private int _numberOfRescuableUnits, _numberOfNotRescuableUnits;
    private List<(int, int)> _coordinatesOfUnits = new List<(int, int)>();

    public SpawnUnits()
    {
        _numberOfRescuableUnits = PlayerPrefs.GetInt("Level", 1) * 3; //With these equations, there will be 60% of Rescuable and 40% of NotRescuable units in every level.
        _numberOfNotRescuableUnits = PlayerPrefs.GetInt("Level", 1) * 2;

        for(int i = 0; i < _numberOfRescuableUnits; i++)
        {
        tryagain:

            int xPosition = (int)Random.Range(_xPositionMinimum, _xPositionMaximum); //I used casting for convert float Random.Range value to int.
            int zPosition = (int)Random.Range(_zPositionMinimum, _zPositionMaximum);

            if (CheckForOverlap(xPosition, zPosition))
            {
                goto tryagain;
            }
            else
            {
                _coordinatesOfUnits.Add((xPosition, zPosition));
                Debug.Log("Spawned xpos: " + xPosition + " Spawned zpos: " + zPosition);

                MonoBehaviour.Instantiate(Resources.Load("Rescuable"), new Vector3(xPosition, 1, zPosition), Quaternion.identity);
            }
        }
        for (int i = 0; i < _numberOfNotRescuableUnits; i++)
        {
        tryagain:

            int xPosition = (int)Random.Range(_xPositionMinimum, _xPositionMaximum); //I used casting for convert float Random.Range value to int.
            int zPosition = (int)Random.Range(_zPositionMinimum, _zPositionMaximum);

            if (CheckForOverlap(xPosition, zPosition))
            {
                goto tryagain;
            }
            else
            {
                _coordinatesOfUnits.Add((xPosition, zPosition));
                Debug.Log("Spawned xpos: " + xPosition + " Spawned zpos: " + zPosition);

                MonoBehaviour.Instantiate(Resources.Load("NotRescuable"), new Vector3(xPosition, 1, zPosition), Quaternion.identity);
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
