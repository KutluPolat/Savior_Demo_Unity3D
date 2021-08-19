using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private SpawnSaver _spawn; 
    
    void Start()
    {
        _spawn = new SpawnSaver();
    }

    void Update()
    {
        
    }
}
