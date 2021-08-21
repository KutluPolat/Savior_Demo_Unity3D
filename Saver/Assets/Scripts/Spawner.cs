using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private SpawnSavior _spawn; 
    
    void Start()
    {
        _spawn = new SpawnSavior();
    }

    void Update()
    {
    }
}
